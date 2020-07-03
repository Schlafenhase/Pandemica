//
//  Rows.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/7/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import AS_GRDBSwiftUI
import SwiftUI

struct PatientRow: View {
    var patient: Patient
    @EnvironmentObject var userSession: AppSession
    
    var body: some View {
        NavigationLink(destination: PatientDetail(patient: GRDBPersistable(patient, autoSave: false)).environmentObject(userSession)) {
            HStack {
                VStack(alignment: .leading) {
                    Text("\(String(patient.patientID)) - \(patient.lastName), \(patient.name)")
                        .font(.headline)
                    Text("Status: \(patient.status)")
                        .foregroundColor(Color("Terciary"))
                }
            }
        }
    }
}

struct PatientRow_Previews: PreviewProvider {
    static var previews: some View {
        NavigationView {
            Text("Test")
        }
    }
}

struct ContactRow: View {
    var contact: Contact
    
    var body: some View {
        NavigationLink(destination: ContactDetail(contact: GRDBPersistable(contact, autoSave: false))) {
            HStack {
                VStack(alignment: .leading) {
                    Text("\(String(contact.contactID)) - \(contact.lastName), \(contact.name)")
                        .font(.headline)
                }
            }
        }
    }
}

struct ContactRow_Previews: PreviewProvider {
    static var previews: some View {
        NavigationView {
            Text("Test")
        }
    }
}
