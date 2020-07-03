//
//  ReportView.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/5/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import SwiftUI

struct ReportView: View {
    var body: some View {
        ZStack {
            Color("Primary").edgesIgnoringSafeArea(.all)
            VStack {
                Text("PDF Report Generation")
                    .font(.custom("Avenir Book", size: 24))
                    .padding(.bottom, 25.0)
                Button(action: generatePatientsByStatusReport) {
                    ButtonContent(text: "patients by status", icon: "Heartbeat Icon", width: 250.0)
                }
                Button(action: generateCasesLastWeekReport) {
                    ButtonContent(text: "cases last week", icon: "Plus Icon", width: 250.0)
                }
            }
        }
    }
    
    func generatePatientsByStatusReport() {
        
    }
    
    func generateCasesLastWeekReport() {
        
    }
}

struct ReportView_Previews: PreviewProvider {
    static var previews: some View {
        ReportView()
    }
}
