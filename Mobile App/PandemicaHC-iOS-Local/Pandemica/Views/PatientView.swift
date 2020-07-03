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

struct PatientView: View {
    @EnvironmentObject var userSession: AppSession

    @State private var showModal = false
    @State private var editMode = EditMode.inactive
    
    // GRDB dabase fetch for requests
    @GRDBFetch(request: Patient.FetchAllPatientsRequest())
    var patients
    
    // Initialize custom Alamofire session without server evaluators for testing
    private let session: Session = {
        let manager = ServerTrustManager(evaluators: ["192.168.0.15": DisabledEvaluator()])
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
                        PatientRow(patient: $0)
                    }
                    .onDelete(perform: onDelete)
                }
                .navigationBarTitle("Patients")
                .navigationBarItems(leading: EditButton().foregroundColor(Color.blue), trailing: Button(action: {
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
                        AddPatientView(showModal: self.$showModal)
                    } )
                .environment(\.editMode, $editMode)
            }
        }
    }
    
    // Deletes object in database
    func onDelete(at offsets: IndexSet) {
        let toDelete = offsets.map { self.patients[$0] }

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
    
    /// Updates packages in list view
    func updatePatients() {
        // Request new data depending on logged delivery man
//        self.session.request("https://192.168.0.15:5001/warehouse/packages/employeePackages", method: .post, parameters: userSession.session, encoder: JSONParameterEncoder.prettyPrinted)
//            .validate()
//            .responseDecodable(of: [Package].self) { (response) in
//                guard let data = response.value else { return }
//                self.packages = self.wrapPackages(packageList: data)
//            }
        //self.patients = [p1, p2]
    }
    
}

struct PatientView_Previews: PreviewProvider {
    static var previews: some View {
        PatientView()
    }
}

