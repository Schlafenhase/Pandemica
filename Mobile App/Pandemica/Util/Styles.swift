//
//  Styles.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 5/6/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import Foundation
import SwiftUI

struct AppLogo: View {
    var body: some View {
        Image("Pandemica Logo")
            .resizable()
            .frame(width: /*@START_MENU_TOKEN@*/150.0/*@END_MENU_TOKEN@*/, height: /*@START_MENU_TOKEN@*/150.0/*@END_MENU_TOKEN@*/)
            .padding(.bottom, 13.0)
    }
}

struct ButtonContent: View {
    var text: String
    var icon: String
    var width = 220.0
    var height = 60.0
    
    var body: some View {
        HStack {
            Image(icon)
                .resizable()
                .frame(width: 30.0, height: 30.0)
            Text(text)
                .font(.custom("Avenir Black", size: 20))
                .fontWeight(.bold)
        }
            .foregroundColor(Color.black)
            .padding()
        .frame(width: CGFloat(self.width), height: CGFloat(self.height))
            .background(Color.white)
            .overlay(
                Rectangle()
                    .stroke(Color("Terciary"), lineWidth: 5)
            )
    }
}

struct TextFieldContent: View {
    @Binding var text: String
    var placeholder: String
    
    var body: some View {
        TextField(placeholder, text: $text)
            .padding()
            .background(Color("EntryBackgroundColor"))
            .cornerRadius(/*@START_MENU_TOKEN@*/10.0/*@END_MENU_TOKEN@*/)
            .frame(width: UIScreen.main.bounds.width * 0.6)
    }
}

struct LowercaseTextFieldContent: View {
    @Binding var text: String
    var placeholder: String
    
    var body: some View {
        TextField(placeholder, text: $text)
            .padding()
            .background(Color("EntryBackgroundColor"))
            .cornerRadius(/*@START_MENU_TOKEN@*/10.0/*@END_MENU_TOKEN@*/)
            .autocapitalization(.none)
    }
}

struct WelcomeText: View {
    var body: some View {
        Text("pandemica")
            .font(.custom("Avenir Book", size: 44))
            .fontWeight(.bold)
    }
}

struct WelcomeSubText: View {
    var body: some View {
        HStack {
            Text("health center")
                .font(.custom("Avenir Light", size: 25))
            Image("Health Center Icon")
                .resizable()
                .frame(width: 50.0, height: 50.0)
        }
    }
}

struct UserInformation: View {
    var session: AppSession
    
    init(session: AppSession) {
        self.session = session
    }
    
    var body: some View {
        VStack {
            Text("Health Center Information:")
                .padding(.bottom, 10.0)
                .padding(.top, 25.0)
                .font(.custom("Avenir Black", size: 18))
            Text("Name: \(session.session!.name) at \(session.session!.location)")
                .font(.custom("Avenir Book", size: 18))
            Text("Email: \(session.session!.eMail)")
                .font(.custom("Avenir Book", size: 18))
            Text("Health Center ID: \(session.session!.id)")
                .font(.custom("Avenir Book", size: 18))
            HStack {
                Text("Bed Count: \(session.session!.bedCount)")
                    .font(.custom("Avenir Book", size: 18))
                Text("(UCI: \(session.session!.UCICount))")
                    .font(.custom("Avenir Book Bold", size: 18))
            }
            Text("Director: \(session.session!.bedCount) (Contact: \(session.session!.UCICount))")
                .font(.custom("Avenir Book", size: 18))
                .padding(.bottom, 25.0)
        }
    }
}
