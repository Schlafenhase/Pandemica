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
        let manager = ServerTrustManager(evaluators: [Constants.shortenedIP: DisabledEvaluator()])
        let configuration = URLSessionConfiguration.af.default

        return Session(configuration: configuration, serverTrustManager: manager)
    }()
    
    func listen () {
        // Monitor authentication changes using Firebase
        handle = Auth.auth().addStateDidChangeListener { (auth, user) in
            if let user = user {
                // If we have a user, create a new user model
                print("Got user: \(user)")
                
                // Check own REST-API database
                print("Connecting to own REST-API...")
                let userToSend = HealthCenter(id: -1,
                                              name: "",
                                              phone: -1,
                                              managerName: "",
                                              capacity: -1,
                                              icuCapacity: -1,
                                              country: "",
                                              region: "",
                                              eMail: user.email ?? "apple@apple.com")
                
                // POST request to fetch data
                self.AlamoSession.request("\(Constants.ip)/Hospital/Email", method: .post,
                                          parameters: userToSend, encoder: JSONParameterEncoder.prettyPrinted)
                    .validate()
                    .responseDecodable(of: [HealthCenter].self) { (response) in
                        guard let data = response.value else { return self.session = nil }
                        // Assign data to logged in health center
                        let hc = data[0]
                        let user = User(name: hc.name, location: hc.country, eMail: hc.eMail, id: String(hc.id),
                                        bedCount: String(hc.capacity), ICUCount: String(hc.icuCapacity), director: hc.managerName,
                                        contact: String(hc.phone), fUID: user.uid)
                        self.session = user
                    }
            } else {
                // If we don't have a user, set our session to nil
                self.session = nil
            }
        }
    }

    /// Signs up new user with Firebase authentication
    func signUp(
        email: String,
        password: String,
        handler: @escaping AuthDataResultCallback
        ) {
        Auth.auth().createUser(withEmail: email, password: password, completion: handler)
    }

    /// Signs in with Firebase authentication
    func signIn(
        email: String,
        password: String,
        handler: @escaping AuthDataResultCallback
        ) {
        // Check Firebase databse
        Auth.auth().signIn(withEmail: email, password: password, completion: handler)
    }

    /// Signs out with Firebase authentication
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
