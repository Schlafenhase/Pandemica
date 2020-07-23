//
//  PatientView.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/5/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import SwiftUI
import Alamofire
import AS_GRDBSwiftUI
import SPAlert

struct PatientView: View {
    @EnvironmentObject var userSession: AppSession

    @State private var showModal = false
    @State private var editMode = EditMode.inactive
    
    // GRDB dabase fetch for requests
    @GRDBFetch(request: Patient.FetchAllPatientsRequest())
    var patients
    
    // Initialize custom Alamofire session without server evaluators for testing
    private let session: Session = {
        let manager = ServerTrustManager(evaluators: [Constants.shortenedIP: DisabledEvaluator()])
        let configuration = URLSessionConfiguration.af.default

        return Session(configuration: configuration, serverTrustManager: manager)
    }()

    init() {
        UITableView.appearance().backgroundColor = UIColor(named: "Default")
        UITableViewCell.appearance().backgroundColor = UIColor(named: "Default")
        UITableView.appearance().tableFooterView = UIView()
    }
    
    var body: some View {
        ZStack {
            NavigationView() {
                List {
                    ForEach(patients) {
                        PatientRow(patient: $0).environmentObject(userSession)
                    }
                    .onDelete(perform: onDelete)
                }
                .navigationBarTitle("Patients")
                .navigationBarItems(leading: EditButton().foregroundColor(Color.blue),
                                    trailing: HStack{ Button(action: { self.showModal.toggle() }) {
                    Image(systemName: "plus")
                        .resizable()
                        .padding(6)
                        .frame(width: 35, height: 35)
                        .background(Color.blue)
                        .clipShape(Circle())
                        .foregroundColor(.white)
                    }.sheet(isPresented: $showModal) {
                        AddPatientView(showModal: self.$showModal).environmentObject(userSession)
                    }; Button(action: { self.updateDatabase() }) {
                        Image(systemName: "arrow.clockwise")
                            .resizable()
                            .padding(6)
                            .frame(width: 35, height: 35)
                            .background(Color.blue)
                            .clipShape(Circle())
                            .foregroundColor(.white)
                    }
                    } )
                .environment(\.editMode, $editMode)
            }
        }
    }
    
    // Converts to age from date
    func getAgeFromDOF(date: String) -> (Int,Int,Int) {

        let dateFormater = DateFormatter()
        dateFormater.dateFormat = "dd/mm/yyyy"
        let dateOfBirth = dateFormater.date(from: date)

        let calender = Calendar.current

        let dateComponent = calender.dateComponents([.year, .month, .day], from:
        dateOfBirth!, to: Date())

        return (dateComponent.year!, dateComponent.month!, dateComponent.day!)
    }
    
    // Deletes object in database
    func onDelete(at offsets: IndexSet) {
        // Get patient to delete
        let toDelete = offsets.map { self.patients[$0] }
        
        // Delete patient in server
        self.session.request("\(Constants.ip)/Patient/Hospital/\(toDelete[0].patientID)", method: .delete)
            .validate()
                
        // Delete locally
        do {
            try AppDatabase.shared.db.write { db in
                toDelete.forEach { patient in
                    do {
                        try patient.delete(db)
                    }
                    catch {
                        // Catch errors here so that the forEach completes as expected
                        print("Error in ForEach deletion")
                    }
                }
            }
        }
        catch {
            // Handle any errors if considered important
            print("Error in deletion")
        }
    }
    
    /// Updates database
    func updateDatabase() {
        SPAlert.present(title: "Synchronizing...", preset: .repeat)
        
        // Delete database
        self.deleteDatabase()
        
        // Update information
        self.updatePatients()
        self.updateContacts()
    }
    
    /// Updates contacts in database
    func updateContacts() {
        
        let requestQueue = DispatchQueue(label: "alamofire.queue")
        
        var delay = 5.0
        for p in patients {
            requestQueue.asyncAfter(deadline: DispatchTime.now() + delay) {
                self.session.request("\(Constants.ip)/Contact/Patient/\(p.patientID)")
                    .validate()
                    .responseDecodable(of: [PersonWithDateOfContact].self) { (response) in
                        guard let data = response.value else { print("contact not found"); return }

                        // Decode contacts
                        for person in data {
                            // Create new contact
                            var nContact = Contact(id: nil, name: person.firstName, lastName: person.lastName, contactID: Int(person.ssn)!,
                                                   age: 0, nationality: "", address: person.address, pathologies: "",
                                                   email: person.eMail, assignedTo: p.patientID)
                            insertContact(contact: &nContact)
                        }
                    }
            }
            delay += 1.0
        }
    }
    
    /// Updates packages in list view
    func updatePatients() {
        // Requests patient data with health center ID in user session
        let hcID = self.userSession.session?.id
        
        let requestQueue = DispatchQueue(label: "alamofire.queue")
        
        self.session.request("\(Constants.ip)/Patient/Hospital/\(hcID ?? String(2))")
            .validate()
            .responseDecodable(of: [PackagedPatient].self) { (response) in
                guard let data = response.value else { print("not found"); return }
                // Decode patients

                // Get all patients ssn
                var allSSN: [Int] = []
                for i in patients {
                    allSSN.append(i.patientID)
                }
                
                var delay = 2.0

                // Iterate over each patient to get its last status, medication and pathologies
                for patient in data {
                    // Add patient only if his ssn is not already in database
                    if !allSSN.contains(Int(patient.ssn) ?? 1) {
                        
                        requestQueue.asyncAfter(deadline: DispatchTime.now() + delay) {
                            // Request patient status
                            self.session.request("\(Constants.ip)/PatientState/\(patient.ssn)")
                                .validate()
                                .responseDecodable(of: [SpecialPatientState].self) { (response) in
                                    guard let data = response.value else { print("not found status"); return }
                                    // Decode status list and get the last status
                                    let pStatus = data.last?.name

                                    // Request patient pathologies
                                    self.session.request("\(Constants.ip)/PatientPathologies/\(patient.ssn)")
                                        .validate()
                                        .responseDecodable(of: [PatientPathologies].self) { (response) in
                                            guard let data = response.value else { print("not found pathologies"); return }

                                            // Store all pathology names in string
                                            var pStr = String()
                                            if !data.isEmpty {
                                                for i in data {
                                                    if i.name == data.last!.name {
                                                        pStr.append("\(i.name)")
                                                    } else {
                                                        pStr.append("\(i.name), ")
                                                    }
                                                }
                                            } else {
                                                pStr = "None"
                                            }

                                            // Request patient medication
                                            self.session.request("\(Constants.ip)/PatientMedication/\(patient.ssn)")
                                                .validate()
                                                .responseDecodable(of: [PatientMedication].self) { (response) in
                                                    guard let data = response.value else { print("not found medication \(patient.ssn)"); return }
                                                    // Store all pathology names in string
                                                    var mStr = String()
                                                    if !data.isEmpty {
                                                        for i in data {
                                                            if i.name == data.last!.name {
                                                                mStr.append("\(i.name)")
                                                            } else {
                                                                mStr.append("\(i.name), ")
                                                            }
                                                        }
                                                    } else {
                                                        mStr = "None"
                                                    }

                                                    // Insert new patient
                                                    var nPatient = Patient(id: nil, name: patient.firstName, lastName: patient.lastName,
                                                                           patientID: Int(patient.ssn)!, age: 20, nationality: patient.nationality,
                                                                           region: patient.region, pathologies: pStr, status: pStatus ?? "None",
                                                                           medication: mStr, isHospitalized: patient.hospitalized, isInUCI: patient.icu)
                                                    insertPatient(patient: &nPatient)
                                                }
                                        }
                                }
                        }
                        delay += 1.0
                    }
                }
            }
    }
    
    /// Inserts a specific patient in database
    func insertContact(contact: inout Contact) {
        do {
            try AppDatabase.shared.db.write { db in
                do {
                    try contact.insert(db)
                }
                catch {
                    print("Error in addition")
                }
            }
        }
        catch {
            print("error")
        }
        print("inserted \(contact)")
    }
    
    /// Inserts a specific patient in database
    func insertPatient(patient: inout Patient) {
        do {
            try AppDatabase.shared.db.write { db in
                do {
                    try patient.insert(db)
                }
                catch {
                    print("Error in addition")
                }
            }
        }
        catch {
            print("error")
        }
    }
    
    /// Deletes the whole database
    func deleteDatabase() {
        // Delete patients and contacts
        do {
            try AppDatabase.shared.db.write { db in
                do {
                    try Patient.deleteAll(db)
                    try Contact.deleteAll(db)
                }
                catch {
                    print("Error in addition")
                }
            }
        }
        catch {
            print("error")
        }
    }
    
    // Updates patient
    func updatePatient(patient: Patient) {
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
    }
    
}

struct PatientView_Previews: PreviewProvider {
    static var previews: some View {
        PatientView()
    }
}

