//
//  EditingViews.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/11/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import AS_GRDBSwiftUI
import Combine
import GRDB
import GRDBCombine
import SPAlert
import SwiftUI
import Alamofire

struct PatientEditingView: View {
    @GRDBPersistable
    var patient: Patient
    
    @EnvironmentObject var userSession: AppSession
    
    @Binding var showModal: Bool
    
    @State private var selectedNationalityIndex: Int = 0
    @State private var selectedRegionIndex: Int = 0
    @State private var selectedStatusIndex: Int = 0
    @State private var selectedSexIndex: Int = 0
    
    @State private var nationalities = Array(repeating: "", count: 168)
    @State private var regions = Array(repeating: "", count: 4)
    @State private var statusList = Array(repeating: "", count: 3)
    var sexOptions = ["M", "F"]
    
    @State private var birthDate = Date()
    
    var patientIDAsString: Binding<String> {
        Binding(get: {
            "\(self.patient.patientID)"
        }) { newValue in
            if let int = Int(newValue) {
                self.patient.patientID = int
            }
        }
    }
    
    var ageAsString: Binding<String> {
        Binding(get: {
            "\(self.patient.age)"
        }) { newValue in
            if let int = Int(newValue) {
                self.patient.age = int
            }
        }
    }
    
    // Initialize custom Alamofire session without server evaluators for testing
    private let session: Session = {
        let manager = ServerTrustManager(evaluators: [Constants.shortenedIP: DisabledEvaluator()])
        let configuration = URLSessionConfiguration.af.default

        return Session(configuration: configuration, serverTrustManager: manager)
    }()

    var body: some View {
        NavigationView {
            Form {
                Section(header: Text("General Information 📄")) {
                    HStack {
                        Text("SSN")
                        Spacer()
                        TextFieldContent(text: patientIDAsString, placeholder: "Patient ID")
                            .keyboardType(.numberPad)
                    }.padding(.leading)
                    HStack {
                        Text("Name")
                        Spacer()
                        TextFieldContent(text: $patient.name, placeholder: "Name")
                    }.padding(.leading)
                    HStack {
                        Text("Last Name")
                        Spacer()
                        TextFieldContent(text: $patient.lastName, placeholder: "Last Name")
                    }.padding(.leading)
                    DatePicker(selection: $birthDate, in: ...Date(), displayedComponents: .date) {
                        Text("Birth Date")
                    }.padding()
                    Picker(selection: $selectedNationalityIndex, label: Text("Nationality")) {
                        ForEach(0 ..< nationalities.count) {
                            Text(self.nationalities[$0])
                        }
                    }.padding()
                    Picker(selection: $selectedRegionIndex, label: Text("Region")) {
                        ForEach(0 ..< regions.count) {
                            Text(self.regions[$0])
                        }
                    }.padding()
                }
                Section(header: Text("Medical Information 💊")) {
                    Picker(selection: $selectedStatusIndex, label: Text("Status")) {
                        ForEach(0 ..< statusList.count) {
                            Text(self.statusList[$0])
                        }
                    }
                        .padding()
                        .pickerStyle(SegmentedPickerStyle())
                    Toggle(isOn: $patient.isHospitalized) {
                        Text("Is Hospitalized")
                    }.padding(.leading)
                    Toggle(isOn: $patient.isInUCI) {
                        Text("Is in UCI")
                    }.padding(.leading)
                    HStack {
                        Spacer()
                        Button(action: {
                            // Get nationalities from pickers and lists
                            let selectedNationality = self.nationalities[self.selectedNationalityIndex]
                            let selectedRegion = self.regions[self.selectedRegionIndex]
                            let selectedStatus = self.statusList[self.selectedStatusIndex]
                            self.patient.nationality = selectedNationality
                            self.patient.region = selectedRegion
                            self.patient.status = selectedStatus
                            
                            self.updatePatient()
                            self.showModal = false
                        }, label: {
                            ButtonContent(text: "edit", icon: "Edit Icon", width: 150.0)
                        })
                            .padding()
                        Spacer()
                    }
                }
            }.navigationBarTitle("Edit Patient")
        }.onAppear() { self.fetchCountriesRegions() }
    }
    
    /// Fetches the list of countries and regions from API
    func fetchCountriesRegions() {
        // GET requests to fetch data
        
        // Fetch country names
        self.session.request("\(Constants.ip)/Country/Names")
            .validate()
            .responseDecodable(of: [String].self) { (response) in
                guard let data = response.value else { self.nationalities = ["Error"]; return}
                self.nationalities = data
            }
        
        // Fetch region names
        self.session.request("\(Constants.ip)/ProvinceStateRegion/Names")
            .validate()
            .responseDecodable(of: [String].self) { (response) in
                guard let data = response.value else { self.regions = ["Error"]; return }
                self.regions = data

            }

        // Fetch status data
        self.session.request("\(Constants.ip)/State/Names")
            .validate()
            .responseDecodable(of: [String].self) { (response) in
                guard let data = response.value else { self.statusList = ["Error"]; return}
                self.statusList = data
            }
    }
    
    // Converts to age from date
    func getAgeFromDOF(date: String) -> (Int,Int,Int) {

        let dateFormater = DateFormatter()
        dateFormater.dateFormat = "yyyy/MM/dd"
        let dateOfBirth = dateFormater.date(from: date)

        let calender = Calendar.current

        let dateComponent = calender.dateComponents([.year, .month, .day], from:
        dateOfBirth!, to: Date())

        return (dateComponent.year!, dateComponent.month!, dateComponent.day!)
    }
    
    /// Updates current patient in database
    func updatePatient() {
        let selectedSex = sexOptions[selectedSexIndex]
        
        // Format date
        let formatter = DateFormatter()
        formatter.dateStyle = .short
        formatter.dateFormat = "yyyy/MM/dd"
        let birthDateStr = formatter.string(from: birthDate)
        patient.age = getAgeFromDOF(date: birthDateStr).0
        
        do {
            try AppDatabase.shared.db.write { db in
                do {
                    try patient.update(db)
                }
                catch {
                    // Catch errors here so that the forEach completes as expected
                    print("Error in ForEach update")
                }
            }
        }
        catch {
            // Handle any errors if considered important
            print("Error in update")
        }
        
        
        // Add in database
        let hcID = Int(self.userSession.session!.id)
        let hcCountry = (self.userSession.session?.location)!
        let pSSN = String(patient.patientID)
        
        let patientToSend = PackagedPatientWHospital(hospital: hcID!, ssn: pSSN, firstName: patient.name,
                                            lastName: patient.lastName, birthDate: birthDateStr, hospitalized: patient.isHospitalized,
                                            icu: patient.isInUCI, country: hcCountry, region: patient.region,
                                            nationality: patient.nationality, sex: selectedSex)
        
        // PUT request new patient
        self.session.request("\(Constants.ip)/Patient/\(patient.patientID)", method: .put,
                                  parameters: patientToSend, encoder: JSONParameterEncoder.prettyPrinted)
            .validate()
            .responseJSON { response in
                print(response)
            }

        
        // Show alert
        SPAlert.present(title: "Patient Edited", preset: .done)
    }
}

struct ContactEditingView: View {
    @GRDBPersistable
    var contact: Contact
    
    @Binding var showModal: Bool
    
    @State private var selectedNationalityIndex: Int = 0
    @State private var selectedRegionIndex: Int = 0
    @State private var selectedStatusIndex: Int = 0
    @State private var selectedSexIndex: Int = 0
    @State private var birthDate = Date()
    
    @ObservedObject var keyBoardResponder = KeyboardResponder()
    
    var sexOptions = ["M", "F"]
    
    var contactIDAsString: Binding<String> {
        Binding(get: {
            "\(self.contact.contactID)"
        }) { newValue in
            if let int = Int(newValue) {
                self.contact.contactID = int
            }
        }
    }
    
    var ageAsString: Binding<String> {
        Binding(get: {
            "\(self.contact.age)"
        }) { newValue in
            if let int = Int(newValue) {
                self.contact.age = int
            }
        }
    }
    
    // Initialize custom Alamofire session without server evaluators for testing
    private let session: Session = {
        let manager = ServerTrustManager(evaluators: [Constants.shortenedIP: DisabledEvaluator()])
        let configuration = URLSessionConfiguration.af.default

        return Session(configuration: configuration, serverTrustManager: manager)
    }()


    var body: some View {
        NavigationView {
            Form {
                Section(header: Text("General Information 📄")) {
                    HStack {
                        Text("SSN")
                        Spacer()
                        TextFieldContent(text: contactIDAsString, placeholder: "Patient ID")
                            .keyboardType(.numberPad)
                    }.padding(.leading)
                    HStack {
                        Text("Name")
                        Spacer()
                        TextFieldContent(text: $contact.name, placeholder: "Name")
                    }.padding(.leading)
                    HStack {
                        Text("Last Name")
                        Spacer()
                        TextFieldContent(text: $contact.lastName, placeholder: "Last Name")
                    }.padding(.leading)
                    DatePicker(selection: $birthDate, in: ...Date(), displayedComponents: .date) {
                        Text("Birth Date")
                    }.padding()
                }
                Section(header: Text("Additional Information 📎")) {
                    HStack {
                        Text("Address")
                        Spacer()
                        TextFieldContent(text: $contact.address, placeholder: "Address")
                    }
                    HStack {
                        Text("Email")
                        Spacer()
                        TextFieldContent(text: $contact.email, placeholder: "Email")
                    }
                    HStack {
                        Spacer()
                        Button(action: {
                            
                            self.updateContact()
                            self.showModal = false
                        }, label: {
                            ButtonContent(text: "edit", icon: "Edit Icon", width: 150.0)
                        })
                            .padding()
                        Spacer()
                    }
                }
            }.navigationBarTitle("Edit Contact")
        }.offset(y: -keyBoardResponder.currentHeight * 0.1)
    }
    
    // Converts to age from date
    func getAgeFromDOF(date: String) -> (Int,Int,Int) {

        let dateFormater = DateFormatter()
        dateFormater.dateFormat = "yyyy/MM/dd"
        let dateOfBirth = dateFormater.date(from: date)

        let calender = Calendar.current

        let dateComponent = calender.dateComponents([.year, .month, .day], from:
        dateOfBirth!, to: Date())

        return (dateComponent.year!, dateComponent.month!, dateComponent.day!)
    }
    
    /// Updates current contact in database
    func updateContact() {
        let selectedSex = sexOptions[selectedSexIndex]
        
        // Format date
        let formatter = DateFormatter()
        formatter.dateStyle = .short
        formatter.dateFormat = "yyyy/MM/dd"
        let birthDateStr = formatter.string(from: birthDate)
        contact.age = getAgeFromDOF(date: birthDateStr).0
        
        do {
            try AppDatabase.shared.db.write { db in
                do {
                    try contact.update(db)
                }
                catch {
                    // Catch errors here so that the forEach completes as expected
                    print("Error in ForEach update")
                }
            }
        }
        catch {
            // Handle any errors if considered important
            print("Error in update")
        }
        
        // Get current date
        let cDate = Date()
        let dateResult = formatter.string(from: cDate)
        
        let contactToSend = Person(patientSsn: String(self.contact.assignedTo), contactDate: dateResult, ssn: String(self.contact.contactID),
                                   firstName: self.contact.name, lastName: self.contact.lastName, birthDate: birthDateStr, eMail: self.contact.email,
                                   address: self.contact.address, sex: selectedSex)
        
        // PUT request contact
        self.session.request("\(Constants.ip)/Person/\(contact.contactID)", method: .put,
                                  parameters: contactToSend, encoder: JSONParameterEncoder.prettyPrinted)
            .validate()
            .responseJSON { response in
                print(response)
            }
        
        // Show alert
        SPAlert.present(title: "Contact Edited", preset: .done)
    }
}

