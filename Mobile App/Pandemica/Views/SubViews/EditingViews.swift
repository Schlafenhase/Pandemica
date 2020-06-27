//
//  EditingViews.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/11/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import AS_GRDBSwiftUI
import Combine
import GRDB
import GRDBCombine
import SPAlert
import SwiftUI

struct PatientEditingView: View {
    @GRDBPersistable
    var patient: Patient
    
    @Binding var showModal: Bool
    
    @State private var nationalities = [String]()
    @State private var nationalitiesWithEmoji = [String]()
    @State private var selectedNationalityIndex: Int = 0
    @State private var selectedRegionIndex: Int = 0
    @State private var selectedStatusIndex: Int = 0
    
    var popularNationalities = ["Costa Rica 🇨🇷", "United States 🇺🇸", "United Kingdom 🇬🇧", "Canada 🇨🇦", "Germany 🇩🇪", "Italy 🇮🇹", "Australia 🇦🇺", "Brazil 🇧🇷", "Japan 🇯🇵", "China 🇨🇳", "Mexico 🇲🇽", "France 🇫🇷", "Spain 🇪🇸"]
    var regions = ["The Americas", "Europe", "Asia", "Africa", "Oceania", "Antarctica"]
    var statusList = ["Alive", "Infected", "Recovered", "Dead"]
    var patientIDAsString: Binding<String> {
        Binding(get: {
            "\(self.patient.patientID)"
        }) { newValue in
            if let int = Int(newValue) {
                self.patient.patientID = int
            }
        }
    }
    
    var ageAsString: Binding<String> {
        Binding(get: {
            "\(self.patient.age)"
        }) { newValue in
            if let int = Int(newValue) {
                self.patient.age = int
            }
        }
    }

    var body: some View {
        NavigationView {
            Form {
                Section(header: Text("General Information 📄")) {
                    HStack {
                        Text("Name")
                        Spacer()
                        TextFieldContent(text: $patient.name, placeholder: "Name")
                    }.padding(.leading)
                    HStack {
                        Text("Last Name")
                        Spacer()
                        TextFieldContent(text: $patient.lastName, placeholder: "Last Name")
                    }.padding(.leading)
                    HStack {
                        Text("Patient ID")
                        Spacer()
                        TextFieldContent(text: patientIDAsString, placeholder: "Patient ID")
                            .keyboardType(.numberPad)
                    }.padding(.leading)
                    HStack {
                        Text("Age")
                        Spacer()
                        TextFieldContent(text: ageAsString, placeholder: "Age")
                            .keyboardType(.numberPad)
                    }.padding(.leading)
                    Picker(selection: $selectedNationalityIndex, label: Text("Nationality")) {
                        ForEach(0 ..< nationalitiesWithEmoji.count) {
                            Text(self.nationalitiesWithEmoji[$0])
                        }
                    }.padding()
                    Picker(selection: $selectedRegionIndex, label: Text("Region")) {
                        ForEach(0 ..< regions.count) {
                            Text(self.regions[$0])
                        }
                    }.padding()
                }
                Section(header: Text("Medical Information 💊")) {
                    HStack {
                        Text("Pathologies")
                        Spacer()
                        TextFieldContent(text: $patient.pathologies, placeholder: "Pathologies")
                    }
                    Picker(selection: $selectedStatusIndex, label: Text("Status")) {
                        ForEach(0 ..< statusList.count) {
                            Text(self.statusList[$0])
                        }
                    }
                        .padding()
                        .pickerStyle(SegmentedPickerStyle())
                    HStack {
                        Text("Medication")
                        Spacer()
                        TextFieldContent(text: $patient.medication, placeholder: "Medication")
                    }
                    Toggle(isOn: $patient.isHospitalized) {
                        Text("Is Hospitalized")
                    }.padding(.leading)
                    Toggle(isOn: $patient.isInUCI) {
                        Text("Is in UCI")
                    }.padding(.leading)
                    HStack {
                        Spacer()
                        Button(action: {
                            // Get nationalities from pickers and lists
                            let selectedNationality = self.nationalitiesWithEmoji[self.selectedNationalityIndex]
                            let selectedRegion = self.regions[self.selectedRegionIndex]
                            let selectedStatus = self.statusList[self.selectedStatusIndex]
                            self.patient.nationality = selectedNationality
                            self.patient.region = selectedRegion
                            self.patient.status = selectedStatus
                            
                            //try self._patient.commitChanges()
                            self.updatePatient()
                            self.showModal = false
                            //self.presentation.wrappedValue.dismiss()
                        }, label: {
                            ButtonContent(text: "edit", icon: "Edit Icon", width: 150.0)
                        })
                            .padding()
                        Spacer()
                    }
                }
            }.navigationBarTitle("Edit Patient")
        }
            .onAppear() { self.loadCountries() }
    }
    
    /// Loads a list of countries with emoji flags
    func loadCountries() {
        var countriesData = [String]()
        var countriesDataRaw = [String]()
        
        // Add popular nationalities first
        countriesData.append(contentsOf: popularNationalities)

        for code in NSLocale.isoCountryCodes  {
            let flag = String.emojiFlag(for: code)
            let id = NSLocale.localeIdentifier(fromComponents: [NSLocale.Key.countryCode.rawValue: code])

            if let name = NSLocale(localeIdentifier: "en_UK").displayName(forKey: NSLocale.Key.identifier, value: id) {
                countriesDataRaw.append(name)
                let nameFlag = name + " " + flag!
                
                if !popularNationalities.contains(nameFlag) {
                    countriesData.append(nameFlag)
                }
            }
            
            self.nationalities = countriesDataRaw
            self.nationalitiesWithEmoji = countriesData
        }
    }
    
    /// Updates current patient in database
    func updatePatient() {
        do {
            try AppDatabase.shared.db.write { db in
                do {
                    try patient.update(db)
                }
                catch {
                    // Catch errors here so that the forEach completes as expected
                    print("Error in ForEach update")
                }
            }
        }
        catch {
            // Handle any errors if considered important
            print("Error in update")
        }
        
        // Show alert
        SPAlert.present(title: "Patient Edited", preset: .done)
    }
}

struct ContactEditingView: View {
    @GRDBPersistable
    var contact: Contact
    
    @Binding var showModal: Bool
    
    @State private var nationalities = [String]()
    @State private var nationalitiesWithEmoji = [String]()
    @State private var selectedNationalityIndex: Int = 0
    @State private var selectedRegionIndex: Int = 0
    @State private var selectedStatusIndex: Int = 0
    
    var popularNationalities = ["Costa Rica 🇨🇷", "United States 🇺🇸", "United Kingdom 🇬🇧", "Canada 🇨🇦", "Germany 🇩🇪", "Italy 🇮🇹", "Australia 🇦🇺", "Brazil 🇧🇷", "Japan 🇯🇵", "China 🇨🇳", "Mexico 🇲🇽", "France 🇫🇷", "Spain 🇪🇸"]
    var contactIDAsString: Binding<String> {
        Binding(get: {
            "\(self.contact.contactID)"
        }) { newValue in
            if let int = Int(newValue) {
                self.contact.contactID = int
            }
        }
    }
    
    var ageAsString: Binding<String> {
        Binding(get: {
            "\(self.contact.age)"
        }) { newValue in
            if let int = Int(newValue) {
                self.contact.age = int
            }
        }
    }

    var body: some View {
        NavigationView {
            Form {
                Section(header: Text("General Information 📄")) {
                    HStack {
                        Text("Name")
                        Spacer()
                        TextFieldContent(text: $contact.name, placeholder: "Name")
                    }.padding(.leading)
                    HStack {
                        Text("Last Name")
                        Spacer()
                        TextFieldContent(text: $contact.lastName, placeholder: "Last Name")
                    }.padding(.leading)
                    HStack {
                        Text("Patient ID")
                        Spacer()
                        TextFieldContent(text: contactIDAsString, placeholder: "Patient ID")
                            .keyboardType(.numberPad)
                    }.padding(.leading)
                    HStack {
                        Text("Age")
                        Spacer()
                        TextFieldContent(text: ageAsString, placeholder: "Age")
                            .keyboardType(.numberPad)
                    }.padding(.leading)
                    Picker(selection: $selectedNationalityIndex, label: Text("Nationality")) {
                        ForEach(0 ..< nationalitiesWithEmoji.count) {
                            Text(self.nationalitiesWithEmoji[$0])
                        }
                    }.padding()
                }
                Section(header: Text("Additional Information 📎")) {
                    HStack {
                        Text("Address")
                        Spacer()
                        TextFieldContent(text: $contact.address, placeholder: "Address")
                    }
                    HStack {
                        Text("Pathologies")
                        Spacer()
                        TextFieldContent(text: $contact.pathologies, placeholder: "Pathologies")
                    }
                    HStack {
                        Text("Email")
                        Spacer()
                        TextFieldContent(text: $contact.email, placeholder: "Email")
                    }
                    HStack {
                        Spacer()
                        Button(action: {
                            // Get nationalities from pickers and lists
                            let selectedNationality = self.nationalitiesWithEmoji[self.selectedNationalityIndex]
                            self.contact.nationality = selectedNationality
                            
                            self.updateContact()
                            self.showModal = false
                        }, label: {
                            ButtonContent(text: "edit", icon: "Edit Icon", width: 150.0)
                        })
                            .padding()
                        Spacer()
                    }
                }
            }.navigationBarTitle("Edit Contact")
        }
            .onAppear() { self.loadCountries() }
    }
    
    /// Loads a list of countries with emoji flags
    func loadCountries() {
        var countriesData = [String]()
        var countriesDataRaw = [String]()
        
        // Add popular nationalities first
        countriesData.append(contentsOf: popularNationalities)

        for code in NSLocale.isoCountryCodes  {
            let flag = String.emojiFlag(for: code)
            let id = NSLocale.localeIdentifier(fromComponents: [NSLocale.Key.countryCode.rawValue: code])

            if let name = NSLocale(localeIdentifier: "en_UK").displayName(forKey: NSLocale.Key.identifier, value: id) {
                countriesDataRaw.append(name)
                let nameFlag = name + " " + flag!
                
                if !popularNationalities.contains(nameFlag) {
                    countriesData.append(nameFlag)
                }
            }
            
            self.nationalities = countriesDataRaw
            self.nationalitiesWithEmoji = countriesData
        }
    }
    
    /// Updates current contact in database
    func updateContact() {
        do {
            try AppDatabase.shared.db.write { db in
                do {
                    try contact.update(db)
                }
                catch {
                    // Catch errors here so that the forEach completes as expected
                    print("Error in ForEach update")
                }
            }
        }
        catch {
            // Handle any errors if considered important
            print("Error in update")
        }
        
        // Show alert
        SPAlert.present(title: "Contact Edited", preset: .done)
    }
}

