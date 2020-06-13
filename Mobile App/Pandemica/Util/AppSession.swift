//
//  AppSession.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/5/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import Foundation
import Combine
import SwiftUI
import Firebase
import Alamofire

class AppSession: ObservableObject {
    let objectWillChange = PassthroughSubject<AppSession,Never>()
    var session: User? { didSet { self.objectWillChange.send(self) }}
    var handle: AuthStateDidChangeListenerHandle?
    
    var currentPage: String = "menu" {
        didSet {
            withAnimation() {
                objectWillChange.send(self)
            }
        }
    }
    
    // Initialize custom Alamofire session without server evaluators for testing
    private let AlamoSession: Session = {
        let manager = ServerTrustManager(evaluators: ["192.168.0.15": DisabledEvaluator()])
        let configuration = URLSessionConfiguration.af.default

        return Session(configuration: configuration, serverTrustManager: manager)
    }()
    
    // User data
    var name = ""
    var password = ""
    var id = ""
    
    func listen () {
        // Monitor authentication changes using Firebase
        handle = Auth.auth().addStateDidChangeListener { (auth, user) in
            if let user = user {
                // If we have a user, create a new user model
                print("Got user: \(user)")
                self.session = User(name: "MP", location: "Grecia", eMail: user.email ?? "mp@mp.com", password: "Mp123", id: "42", bedCount: "23", UCICount: "23", director: "Quebin", contact: "22212222")
                
                // Check own REST-API database
                print("Connecting to own REST-API...")
                //let userToSend = User(name: "null", department: "Delivery", eMail: user.email!, password: "null", id: "null")
//                self.AlamoSession.request("https://192.168.0.15:5001/administrator/employees/check", method: .post, parameters: userToSend, encoder: JSONParameterEncoder.prettyPrinted)
//                    .validate()
//                    .responseDecodable(of: User.self) { (response) in
//                        guard let data = response.value else { return self.session = nil }
//                        self.session = User(name: data.name, department: "Delivery", eMail: user.email ?? "mp@mp.com", password: data.password, id: data.id)
//                    }
            } else {
                // If we don't have a user, set our session to nil
                self.session = nil
            }
        }
    }

    func signUp(
        email: String,
        password: String,
        handler: @escaping AuthDataResultCallback
        ) {
        Auth.auth().createUser(withEmail: email, password: password, completion: handler)
    }

    func signIn(
        email: String,
        password: String,
        handler: @escaping AuthDataResultCallback
        ) {
        // Check Firebase databse
        Auth.auth().signIn(withEmail: email, password: password, completion: handler)
    }

    func signOut () -> Bool {
        do {
            try Auth.auth().signOut()
            self.session = nil
            return true
        } catch {
            return false
        }
    }
    
    func unbind () {
        if let handle = handle {
            Auth.auth().removeStateDidChangeListener(handle)
        }
    }
    
}
