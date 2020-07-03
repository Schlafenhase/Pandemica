//
//  Details.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/5/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import AS_GRDBSwiftUI
import SwiftUI
import Alamofire

struct PatientDetail: View {
    @GRDBPersistable
    var patient: Patient
    
    @EnvironmentObject var userSession: AppSession
    
    // GRDB dabase fetch for requests
    @GRDBFetch(request: Contact.FetchAllContactsRequest())
    var contacts
    
    // Initialize custom Alamofire session without server evaluators for testing
    private let session: Session = {
        let manager = ServerTrustManager(evaluators: ["192.168.0.15": DisabledEvaluator()])
        let configuration = URLSessionConfiguration.af.default

        return Session(configuration: configuration, serverTrustManager: manager)
    }()
    
    @State private var showModal = false
    @State private var showContactModal = false
    
    var body: some View {
        ZStack {
            VStack {
                VStack {
                    Text("Patient ID: \(String(patient.patientID))")
                        .padding(25.0)
                        .font(.headline)
                    Text("Name: \(patient.lastName), \(patient.name)")
                    Text("Age: \(String(patient.age))")
                    Text("Nationality: \(patient.nationality)")
                    Text("Region: \(patient.region)")
                    Text("Pathologies: \(patient.pathologies)")
                    Text("Medication: \(patient.medication)")
                    Text("Status: \(patient.status)")
                        .foregroundColor(Color("Terciary"))
                        .padding(25.0)
                    if patient.isHospitalized {
                        Text("IS HOSPITALIZED")
                            .foregroundColor(Color("Terciary"))
                            .font(.custom("Avenir Black", size: 18))
                    } else if patient.isInUCI {
                        Text("IS IN UCI")
                            .foregroundColor(Color("Terciary"))
                    }
                }
                VStack {
                    Button(action: {
                        self.showModal = true
                    }) {
                        ButtonContent(text: "edit", icon: "Edit Icon", width: 200.0)
                    }.sheet(isPresented: self.$showModal) {
                        PatientEditingView(patient: GRDBPersistable(self.patient, autoSave: false), showModal: self.$showModal).environmentObject(userSession)
                    }
                    Button(action: {
                        self.showContactModal = true
                    }) {
                        ButtonContent(text: "contacts", icon: "User Icon", width: 200.0)
                    }.sheet(isPresented: self.$showContactModal) {
                        ContactView(showContactModal: self.$showContactModal, patientID: self.patient.patientID)
                            .attachDatabase(AppDatabase.shared.db)
                    }
                }
            }
        }
            .onAppear() { self.fetchPatient() }
    }
    
    func fetchPatient() {
        do {
            try AppDatabase.shared.db.write { db in
                if let nPatient = try Patient.fetchOne(db, key: patient.id) {
                    patient = nPatient
                }
            }
        }
        catch {
            // Handle any errors if considered important
            print("Error in fetch")
        }
    }
    
}

struct PatientDetail_Previews: PreviewProvider {
    static var previews: some View {
        Text("Test")
        
    }
}

struct ContactDetail: View {
    @GRDBPersistable
    var contact: Contact
    
    // Initialize custom Alamofire session without server evaluators for testing
    private let session: Session = {
        let manager = ServerTrustManager(evaluators: ["192.168.0.15": DisabledEvaluator()])
        let configuration = URLSessionConfiguration.af.default

        return Session(configuration: configuration, serverTrustManager: manager)
    }()
    
    @State private var showModal = false
    @Environment(\.presentationMode) var presentation
    
    func fetchContact() {
        do {
            try AppDatabase.shared.db.write { db in
                if let nContact = try Contact.fetchOne(db, key: contact.id) {
                    contact = nContact
                }
            }
        }
        catch {
            // Handle any errors if considered important
            print("Error in fetch")
        }
    }
    
    var body: some View {
        ZStack {
            VStack() {
                Text("Contact ID: \(String(contact.contactID))")
                    .padding(25.0)
                    .font(.headline)
                Text("Name: \(contact.lastName), \(contact.name)")
                Text("Age: \(String(contact.age))")
                Text("Address: \(contact.address)")
                Text("Email: \(contact.email)")
                    .foregroundColor(Color("Terciary"))
                    .padding(25.0)
                
                Button(action: {
                    self.showModal = true
                    self.presentation.wrappedValue.dismiss()
                }) {
                    ButtonContent(text: "edit", icon: "Edit Icon", width: 150.0)
                }.sheet(isPresented: self.$showModal) {
                    ContactEditingView(contact: GRDBPersistable(self.contact, autoSave: false), showModal: self.$showModal)
                }
            }
        }.onAppear() { self.fetchContact() }
    }
}

struct ContactDetail_Previews: PreviewProvider {
    static var previews: some View {
        Text("Test")
        
    }
}

