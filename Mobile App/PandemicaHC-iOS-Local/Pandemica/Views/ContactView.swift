//
//  ContactView.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/11/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import Alamofire
import AS_GRDBSwiftUI
import SwiftUI

struct ContactView: View {
    @EnvironmentObject var userSession: AppSession
    
    @Binding var showContactModal: Bool
    
    @State private var showModal = false
    @State private var editMode = EditMode.inactive
    
    // GRDB database fetch for requests
    @GRDBFetch(request: Contact.FetchAllContactsRequest())
    var contacts
    
    @State var filteredContacts: [Contact] = []
    
    var patientID: Int
    
    // Initialize custom Alamofire session without server evaluators for testing
    private let session: Session = {
        let manager = ServerTrustManager(evaluators: ["192.168.0.15": DisabledEvaluator()])
        let configuration = URLSessionConfiguration.af.default

        return Session(configuration: configuration, serverTrustManager: manager)
    }()
    
    var body: some View {
        ZStack {
            NavigationView() {
                List {
                    ForEach(filteredContacts) {
                        ContactRow(contact: $0)
                    }
                    .onDelete(perform: onDelete)
                }
                .navigationBarTitle("Contacts")
                .navigationBarItems(leading: EditButton().foregroundColor(Color.blue), trailing:
                    HStack {
                        Button(action: {
                            self.showModal.toggle()
                        } ) {
                            Image(systemName: "plus")
                                .resizable()
                                .padding(6)
                                .frame(width: 35, height: 35)
                                .background(Color.blue)
                                .clipShape(Circle())
                                .foregroundColor(.white)
                            }.sheet(isPresented: $showModal) {
                                AddContactView(showModal: self.$showModal, patientID: self.patientID)
                            }
                        Button(action: {
                            self.filterContacts()
                        } ) {
                            Image(systemName: "arrow.clockwise")
                                .resizable()
                                .padding(6)
                                .frame(width: 35, height: 35)
                                .background(Color.blue)
                                .clipShape(Circle())
                                .foregroundColor(.white)
                            }
                    }
                )
                .environment(\.editMode, $editMode)
            }
        }
    }
    
    /// Filters only contacts assigned to patient
    func filterContacts() {
        filteredContacts.removeAll()
        print(patientID)
        for element in contacts {
            if element.assignedTo == patientID {
                filteredContacts.append(element)
            }
        }
    }
    
    // Deletes object in database
    func onDelete(at offsets: IndexSet) {
        let toDelete = offsets.map { self.contacts[$0] }

        do {
            try AppDatabase.shared.db.write { db in
                toDelete.forEach { contact in
                    do {
                        try contact.delete(db)
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
}
