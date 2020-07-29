//
//  ReportView.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/5/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import SwiftUI
import Alamofire
import UIKit
import SPAlert

struct ReportView: View {
    @State var isShowingReport = false
    @State private var showPreview = false // state activating preview
    @State var reportURL = Constants.dummyPDF

    // Initialize custom Alamofire session without server evaluators for testing
    private let session: Session = {
        let manager = ServerTrustManager(evaluators: [Constants.shortenedIP: DisabledEvaluator()])
        let configuration = URLSessionConfiguration.af.default

        return Session(configuration: configuration, serverTrustManager: manager)
    }()
    
    var body: some View {
        ZStack {
            Color("Primary").edgesIgnoringSafeArea(.all)
            VStack {
                Text("PDF Report Generation")
                    .bold()
                    .font(.custom("Avenir Book", size: 24))
                    .padding(.bottom, 25.0)
                Button(action: { generateReport(reportType: "patientsByStatus") }) {
                    ButtonContent(text: "patients by status", icon: "Heartbeat Icon", width: 250.0)
                }.background(DocumentPreview($showPreview,
                                             url: $reportURL.wrappedValue!))
                Button(action: { generateReport(reportType: "casesDeaths") }) {
                    ButtonContent(text: "cases last week", icon: "Plus Icon", width: 250.0)
                }
            }
        }
    }
    
    func generateReport(reportType: String) -> Void {
        // Tell User that report is being downloaded
        SPAlert.present(title: "Downloading...", preset: .doc)
        
        // Load PDF destination named as report type
        let destination: DownloadRequest.Destination = { _, _ in
            let documentsURL = FileManager.default.urls(for: .documentDirectory, in: .userDomainMask)[0]
            let fileURL = documentsURL.appendingPathComponent("\(reportType).pdf")
            
            return (fileURL, [.removePreviousFile, .createIntermediateDirectories])
        }
        
        // Download report with Alamofire
        self.session.download("\(Constants.ip)/reports/\(reportType)", to: destination).response { response in
            if response.error == nil, let reportPath = response.fileURL?.path {
                // Update URL and show report preview
                self.reportURL = URL(fileURLWithPath: reportPath)
                self.showPreview = true
            } else {
                SPAlert.present(title: "Error", preset: .error)
            }
        }
    }
    
}
