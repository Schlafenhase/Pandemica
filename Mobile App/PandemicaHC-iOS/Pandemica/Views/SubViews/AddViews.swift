//
//  AddViews.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/6/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import SwiftUI
import Combine
import SPAlert
import Alamofire

struct AddPatientView: View {
    @Binding var showModal: Bool
    @EnvironmentObject var userSession: AppSession
    
    // New Patient Attributes
    @State var name: String = ""
    @State var lastName: String = ""
    @State var patientID: String = ""
    @State var age: String = ""
    @State var nationality: String = ""
    @State private var selectedNationalityIndex: Int = 0
    @State var region: String = ""
    @State private var selectedRegionIndex: Int = 0
    @State private var selectedStatusIndex: Int = 0
    @State private var selectedSexIndex: Int = 0
    @State var isHospitalized: Bool = false
    @State var isInUCI: Bool = false
    @State private var birthDate = Date()
    
    @State private var nationalities = Array(repeating: "", count: 168)
    @State private var regions = Array(repeating: "", count: 4)
    @State private var statusList = Array(repeating: "", count: 3)
    var sexOptions = ["M", "F"]
    
    // Initialize custom Alamofire session without server evaluators for testing
    private let session: Session = {
        let manager = ServerTrustManager(evaluators: [Constants.shortenedIP: DisabledEvaluator()])
        let configuration = URLSessionConfiguration.af.default

        return Session(configuration: configuration, serverTrustManager: manager)
    }()
    
    private var validated: Bool {
        !name.isEmpty && !lastName.isEmpty && !patientID.isEmpty
    }
    
    var body: some View {
        VStack {
            NavigationView {
                Form {
                    Section(header: Text("General Information 📄")) {
                        HStack {
                            Text("SSN")
                            Spacer()
                            TextFieldContent(text: $patientID, placeholder: "Patient ID")
                                .keyboardType(.numberPad)
                                .onReceive(Just(patientID)) { newValue in
                                    let filtered = newValue.filter { "0123456789".contains($0) }
                                    if filtered != newValue {
                                        self.patientID = filtered
                                    }
                                }
                        }.padding(.leading)
                        HStack {
                            Text("Name")
                            Spacer()
                            TextFieldContent(text: $name, placeholder: "Name")
                        }.padding(.leading)
                        HStack {
                            Text("Last Name")
                            Spacer()
                            TextFieldContent(text: $lastName, placeholder: "Last Name")
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
                        Picker(selection: $selectedSexIndex, label: Text("Sex")) {
                            ForEach(0 ..< sexOptions.count) {
                                Text(self.sexOptions[$0])
                            }
                        }
                            .padding()
                            .pickerStyle(SegmentedPickerStyle())
                        Toggle(isOn: $isHospitalized) {
                            Text("Is Hospitalized")
                        }.padding(.leading)
                        Toggle(isOn: $isInUCI) {
                            Text("Is in UCI")
                        }.padding(.leading)
                        HStack {
                            Spacer()
                            if validated {
                                Button(action: self.addPatient) {
                                    ButtonContent(text: "Add", icon: "Plus Icon", width: 240.0)
                                }
                            }
                            Spacer()
                        }
                    }
                }.navigationBarTitle("Add New Patient")
            }
        }
        .onAppear() { self.fetchCountriesRegions() }
    }
    
    /// Adds new patient in database
    func addPatient() {
        let selectedNationality = nationalities[selectedNationalityIndex]
        let selectedRegion = regions[selectedRegionIndex]
        let selectedStatus = statusList[selectedStatusIndex]
        let selectedSex = sexOptions[selectedSexIndex]
        
        // Format date
        let formatter = DateFormatter()
        formatter.dateStyle = .short
        formatter.dateFormat = "yyyy/MM/dd"
        let birthDateStr = formatter.string(from: birthDate)
        
        var patient = Patient(
            name: self.name,
            lastName: self.lastName,
            patientID: Int(self.patientID) ?? Int.random(in: 10000 ... 99999),
            age: getAgeFromDOF(date: birthDateStr).0,
            nationality: selectedNationality,
            region: selectedRegion,
            pathologies: "",
            status: selectedStatus,
            medication: "",
            isHospitalized: self.isHospitalized,
            isInUCI: self.isInUCI)
        
        do {
            try AppDatabase.shared.db.write {db in
                try patient.insert(db)
            }
        } catch {
            print("Error")
        }
        
        // Add in database
        let hcID = Int(self.userSession.session!.id)
        let hcCountry = (self.userSession.session?.location)!
        let pSSN = String(patient.patientID)
        
        let patientToSend = PackagedPatientWHospital(hospital: hcID!, ssn: pSSN, firstName: patient.name,
                                            lastName: patient.lastName, birthDate: birthDateStr, hospitalized: patient.isHospitalized,
                                            icu: patient.isInUCI, country: hcCountry, region: patient.region,
                                            nationality: patient.nationality, sex: selectedSex)
        
        // POST request new patient
        self.session.request("\(Constants.ip)/Patient", method: .post,
                                  parameters: patientToSend, encoder: JSONParameterEncoder.prettyPrinted)
            .validate()
            .responseJSON { response in
                print(response)
            }
        
        // Return to patient view
        showModal = false
        
        // Show alert
        SPAlert.present(title: "Patient Added", preset: .add)
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
        dateFormater.dateFormat = "yyyy/mm/dd"
        let dateOfBirth = dateFormater.date(from: date)

        let calender = Calendar.current

        let dateComponent = calender.dateComponents([.year, .month, .day], from:
        dateOfBirth!, to: Date())

        return (dateComponent.year!, dateComponent.month!, dateComponent.day!)
    }
    
}

struct AddContactView: View {
    @Binding var showModal: Bool
    
    // New Contact Attributes
    @State var name: String = ""
    @State var lastName: String = ""
    @State var contactID: String = ""
    @State var age: String = ""
    @State var address: String = ""
    @State var email: String = ""
    @State private var selectedSexIndex: Int = 0
    @State private var birthDate = Date()
    var patientID: Int
    
    @ObservedObject var keyBoardResponder = KeyboardResponder()
    
    var sexOptions = ["M", "F"]
    
    private var validated: Bool {
        !name.isEmpty && !lastName.isEmpty && !contactID.isEmpty && !address.isEmpty
    }
    
    // Initialize custom Alamofire session without server evaluators for testing
    private let session: Session = {
        let manager = ServerTrustManager(evaluators: [Constants.shortenedIP: DisabledEvaluator()])
        let configuration = URLSessionConfiguration.af.default

        return Session(configuration: configuration, serverTrustManager: manager)
    }()
    
    var body: some View {
        VStack {
            NavigationView {
                Form {
                    Section(header: Text("General Information 📄")) {
                        HStack {
                            Text("Contact ID")
                            Spacer()
                            TextFieldContent(text: $contactID, placeholder: "Contact ID")
                                .keyboardType(.numberPad)
                                .onReceive(Just(contactID)) { newValue in
                                    let filtered = newValue.filter { "0123456789".contains($0) }
                                    if filtered != newValue {
                                        self.contactID = filtered
                                    }
                                }
                        }.padding(.leading)
                        HStack {
                            Text("Name")
                            Spacer()
                            TextFieldContent(text: $name, placeholder: "Name")
                        }.padding(.leading)
                        HStack {
                            Text("Last Name")
                            Spacer()
                            TextFieldContent(text: $lastName, placeholder: "Last Name")
                        }.padding(.leading)
                        DatePicker(selection: $birthDate, in: ...Date(), displayedComponents: .date) {
                            Text("Birth Date")
                        }.padding()
                        Picker(selection: $selectedSexIndex, label: Text("Sex")) {
                            ForEach(0 ..< sexOptions.count) {
                                Text(self.sexOptions[$0])
                            }
                        }
                            .padding()
                            .pickerStyle(SegmentedPickerStyle())
                    }
                    Section(header: Text("Additional Information 📎")) {
                        HStack {
                            Text("Address")
                            Spacer()
                            TextFieldContent(text: $address, placeholder: "Address")
                        }.padding(.leading)
                        HStack {
                            Text("Email")
                            Spacer()
                            TextFieldContent(text: $email, placeholder: "Email")
                        }.padding(.leading)
                        HStack {
                            Spacer()
                            if validated {
                                Button(action: self.addContact) {
                                    ButtonContent(text: "Add", icon: "Plus Icon", width: 240.0)
                                }
                            }
                            Spacer()
                        }
                    }
                }.navigationBarTitle("Add New Contact")
            }
        }.offset(y: -keyBoardResponder.currentHeight * 0.1)
    }
    
    /// Adds new patient in database
    func addContact() {
        let selectedSex = sexOptions[selectedSexIndex]
        
        // Format date
        let formatter = DateFormatter()
        formatter.dateStyle = .short
        formatter.dateFormat = "yyyy/MM/dd"
        let birthDateStr = formatter.string(from: birthDate)
        print(birthDateStr)
        
        // Add in database
        var contact = Contact(
            name: self.name,
            lastName: self.lastName,
            contactID: Int(self.contactID) ?? Int.random(in: 10000 ... 99999),
            age: self.getAgeFromDOF(date: birthDateStr).0,
            nationality: "",
            address: self.address,
            pathologies: "",
            email: self.email,
            assignedTo: patientID)
        do {
            try AppDatabase.shared.db.write {db in
                try contact.insert(db)
            }
        } catch {
            print("Error")
        }
        
        // Get current date
        let contactDate = Date()
        let dateResult = formatter.string(from: contactDate)
        
        let contactToSend = Person(patientSsn: String(self.patientID), contactDate: dateResult, ssn: self.contactID,
                                   firstName: self.name, lastName: self.lastName, birthDate: birthDateStr, eMail: self.email,
                                   address: self.address, sex: selectedSex)
        
        // POST request new patient
        self.session.request("\(Constants.ip)/Person", method: .post,
                                  parameters: contactToSend, encoder: JSONParameterEncoder.prettyPrinted)
            .validate()
            .responseJSON { response in
                print(response)
            }
        
        // Return to patient view
        showModal = false
        
        // Show alert
        SPAlert.present(title: "Contact Added", preset: .add)
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

}

struct AddViews_Previews: PreviewProvider {
    static var previews: some View {
        /*@START_MENU_TOKEN@*/Text("Hello, World!")/*@END_MENU_TOKEN@*/
    }
}
