//
//  SettingsView.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/5/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import SPAlert
import SwiftUI

struct SettingsView: View {
    @EnvironmentObject var session: AppSession
    
    /// Logs out with Firebase authentication
    func logOut() {
        let hasSignedOut = session.signOut()
        if hasSignedOut {
            print("Success signing out")
            // Show alert
            SPAlert.present(title: "Signed Out", preset: .error)
        } else {
            print("Error signing out")
        }
    }
    
    var body: some View {
        ZStack {
            Color("Primary").edgesIgnoringSafeArea(.all)
            VStack {
                AppLogo()
                WelcomeText()
                WelcomeSubText()
                UserInformation(session: self.session)
                
                Text("Schlafenhase. 2020\nMade in Costa Rica")
                    .font(.custom("Avenir Book", size: 15))
                    .padding(.bottom, 25.0)
                
                Button(action: logOut) {
                    ButtonContent(text: "sign out", icon: "X Icon")
                }
            }
        }
    }
}

struct SettingsView_Previews: PreviewProvider {
    static var previews: some View {
        SettingsView().environmentObject(AppSession())
    }
}
