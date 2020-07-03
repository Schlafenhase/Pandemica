//
//  LogInView.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/5/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import SPAlert
import SwiftUI

struct LogInView: View {
    @State private var selection = 0
    @State var username: String = ""
    @State var password: String = ""
    @State var error = false
    
    @State var authenticationDidFail: Bool = false
    @State var authenticationDidSucceed: Bool = false
    
    @ObservedObject var keyBoardResponder = KeyboardResponder()
    @EnvironmentObject var session: AppSession
    
    /// Communicates log in form with session authentication server
    func logIn() {
        error = false
        session.signIn(email: username, password: password) { (result, error) in
            if error != nil {
                self.error = true
                self.authenticationDidFail = true
                self.authenticationDidSucceed = false
            } else {
                self.username = ""
                self.password = ""
                self.authenticationDidSucceed = true
                // Show alert
                SPAlert.present(title: "Signed In", preset: .done)
            }
        }
    }
 
    var body: some View {
        ZStack {
            Color("Primary").edgesIgnoringSafeArea(.all)
            
            VStack {
                AppLogo()
                WelcomeText()
                WelcomeSubText()
                LowercaseTextFieldContent(text: $username, placeholder: "Username")
                PasswordSecureField(password: $password)
                
                if authenticationDidFail {
                    Text("Information not correct. Try again.")
                        .foregroundColor(Color.red)
                        .offset(y: -10)
                }

                Button(action: logIn) {
                    ButtonContent(text: "sign in", icon: "Right Arrow Icon")
                }
                
            }
            .padding()
        }
        .offset(y: -keyBoardResponder.currentHeight * 0.82)
    }
}

struct LogInView_Previews: PreviewProvider {
    static var previews: some View {
        LogInView().environmentObject(AppSession())
    }
}

struct PasswordSecureField: View {
    @Binding var password: String
    var body: some View {
        SecureField("Password", text: $password)
            .padding()
            .background(Color("EntryBackgroundColor"))
            .cornerRadius(10.0)
            .padding(/*@START_MENU_TOKEN@*/.bottom, 20.0/*@END_MENU_TOKEN@*/)
    }
}
