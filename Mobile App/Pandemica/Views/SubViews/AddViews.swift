//
//  AddViews.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/6/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import SwiftUI
import Combine
import SPAlert

struct AddPatientView: View {
    @Binding var showModal: Bool
    
    // New Patient Attributes
    @State var name: String = ""
    @State var lastName: String = ""
    @State var patientID: String = ""
    @State var age: String = ""
    @State var nationality: String = ""
    @State private var selectedNationalityIndex: Int = 0
    @State var region: String = ""
    @State private var selectedRegionIndex: Int = 0
    @State var pathologies: String = ""
    @State private var selectedStatusIndex: Int = 0
    @State var medication: String = ""
    @State var isHospitalized: Bool = false
    @State var isInUCI: Bool = false
    
    @State private var nationalities = [String]()
    @State private var nationalitiesWithEmoji = [String]()
    var popularNationalities = ["Costa Rica 🇨🇷", "United States 🇺🇸", "United Kingdom 🇬🇧", "Canada 🇨🇦", "Germany 🇩🇪", "Italy 🇮🇹", "Australia 🇦🇺", "Brazil 🇧🇷", "Japan 🇯🇵", "China 🇨🇳", "Mexico 🇲🇽", "France 🇫🇷", "Spain 🇪🇸"]
    var regions = ["The Americas", "Europe", "Asia", "Africa", "Oceania", "Antarctica"]
    var statusList = ["Alive", "Infected", "Recovered", "Dead"]
    
    private var validated: Bool {
        !name.isEmpty && !lastName.isEmpty && !patientID.isEmpty && !age.isEmpty && !pathologies.isEmpty && !medication.isEmpty
    }
    
    var body: some View {
        VStack {
            NavigationView {
                Form {
                    Section(header: Text("General Information 📄")) {
                        HStack {
                            Text("Name")
                            Spacer()
                            TextFieldContent(text: $name, placeholder: "Name")
                        }.padding(.leading)
                        HStack {
                            Text("Last Name")
                            Spacer()
                            TextFieldContent(text: $lastName, placeholder: "Last Name")
                        }.padding(.leading)
                        HStack {
                            Text("Patient ID")
                            Spacer()
                            TextFieldContent(text: $patientID, placeholder: "Patient ID")
                                .keyboardType(.numberPad)
                                .onReceive(Just(patientID)) { newValue in
                                    let filtered = newValue.filter { "0123456789".contains($0) }
                                    if filtered != newValue {
                                        self.patientID = filtered
                                    }
                                }
                        }.padding(.leading)
                        HStack {
                            Text("Age")
                            Spacer()
                            TextFieldContent(text: $age, placeholder: "Age")
                                .keyboardType(.numberPad)
                                .onReceive(Just(age)) { newValue in
                                    let filtered = newValue.filter { "0123456789".contains($0) }
                                    if filtered != newValue {
                                        self.age = filtered
                                    }
                                }
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
                            TextFieldContent(text: $pathologies, placeholder: "Pathologies")
                        }.padding(.leading)
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
                            TextFieldContent(text: $medication, placeholder: "Medication")
                        }.padding(.leading)
                        Toggle(isOn: $isHospitalized) {
                            Text("Is Hospitalized")
                        }.padding(.leading)
                        Toggle(isOn: $isInUCI) {
                            Text("Is in UCI")
                        }.padding(.leading)
                        HStack {
                            Spacer()
                            if validated {
                                Button(action: self.addPatient) {
                                    ButtonContent(text: "Add", icon: "Plus Icon", width: 240.0)
                                }
                            }
                            Spacer()
                        }
                    }
                }.navigationBarTitle("Add New Patient")
            }
        }
        .onAppear() { self.loadCountries() }
    }
    
    /// Adds new patient in database
    func addPatient() {
        let selectedNationality = nationalitiesWithEmoji[selectedNationalityIndex]
        let selectedRegion = regions[selectedRegionIndex]
        let selectedStatus = statusList[selectedStatusIndex]
        
        var patient = Patient(
            name: self.name,
            lastName: self.lastName,
            patientID: Int(self.patientID) ?? Int.random(in: 10000 ... 99999),
            age: Int(self.age) ?? 0,
            nationality: selectedNationality,
            region: selectedRegion,
            pathologies: self.pathologies,
            status: selectedStatus,
            medication: self.medication,
            isHospitalized: self.isHospitalized,
            isInUCI: self.isInUCI)
        
        do {
            try AppDatabase.shared.db.write {db in
                try patient.insert(db)
            }
        } catch {
            print("Error")
        }
        
        // Return to patient view
        showModal = false
        
        // Show alert
        SPAlert.present(title: "Patient Added", preset: .add)
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
}

struct AddContactView: View {
    @Binding var showModal: Bool
    
    // New Patient Attributes
    @State var name: String = ""
    @State var lastName: String = ""
    @State var contactID: String = ""
    @State var age: String = ""
    @State var nationality: String = ""
    @State private var selectedNationalityIndex: Int = 0
    @State var address: String = ""
    @State var pathologies: String = ""
    @State var email: String = ""
    
    @State private var nationalities = [String]()
    @State private var nationalitiesWithEmoji = [String]()
    var popularNationalities = ["Costa Rica 🇨🇷", "United States 🇺🇸", "United Kingdom 🇬🇧", "Canada 🇨🇦", "Germany 🇩🇪", "Italy 🇮🇹", "Australia 🇦🇺", "Brazil 🇧🇷", "Japan 🇯🇵", "China 🇨🇳", "Mexico 🇲🇽", "France 🇫🇷", "Spain 🇪🇸"]
    var regions = ["The Americas", "Europe", "Asia", "Africa", "Oceania", "Antarctica"]
    var statusList = ["Alive", "Infected", "Recovered", "Dead"]
    
    var patientID: Int
    
    private var validated: Bool {
        !name.isEmpty && !lastName.isEmpty && !contactID.isEmpty && !age.isEmpty && !address.isEmpty && !pathologies.isEmpty && !email.isEmpty
    }
    
    var body: some View {
        VStack {
            NavigationView {
                Form {
                    Section(header: Text("General Information 📄")) {
                        HStack {
                            Text("Name")
                            Spacer()
                            TextFieldContent(text: $name, placeholder: "Name")
                        }.padding(.leading)
                        HStack {
                            Text("Last Name")
                            Spacer()
                            TextFieldContent(text: $lastName, placeholder: "Last Name")
                        }.padding(.leading)
                        HStack {
                            Text("Contact ID")
                            Spacer()
                            TextFieldContent(text: $contactID, placeholder: "Contact ID")
                                .keyboardType(.numberPad)
                                .onReceive(Just(contactID)) { newValue in
                                    let filtered = newValue.filter { "0123456789".contains($0) }
                                    if filtered != newValue {
                                        self.contactID = filtered
                                    }
                                }
                        }.padding(.leading)
                        HStack {
                            Text("Age")
                            Spacer()
                            TextFieldContent(text: $age, placeholder: "Age")
                                .keyboardType(.numberPad)
                                .onReceive(Just(age)) { newValue in
                                    let filtered = newValue.filter { "0123456789".contains($0) }
                                    if filtered != newValue {
                                        self.age = filtered
                                    }
                                }
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
                            TextFieldContent(text: $address, placeholder: "Address")
                        }.padding(.leading)
                        HStack {
                            Text("Pathologies")
                            Spacer()
                            TextFieldContent(text: $pathologies, placeholder: "Pathologies")
                        }.padding(.leading)
                        HStack {
                            Text("Email")
                            Spacer()
                            TextFieldContent(text: $email, placeholder: "Email")
                        }.padding(.leading)
                        HStack {
                            Spacer()
                            if validated {
                                Button(action: self.addContact) {
                                    ButtonContent(text: "Add", icon: "Plus Icon", width: 240.0)
                                }
                            }
                            Spacer()
                        }
                    }
                }.navigationBarTitle("Add New Contact")
            }
        }
        .onAppear() { self.loadCountries() }
    }
    
    /// Adds new patient in database
    func addContact() {
        let selectedNationality = nationalitiesWithEmoji[selectedNationalityIndex]
        
        var contact = Contact(
            name: self.name,
            lastName: self.lastName,
            contactID: Int(self.contactID) ?? Int.random(in: 10000 ... 99999),
            age: Int(self.age) ?? 0,
            nationality: selectedNationality,
            address: self.address,
            pathologies: self.pathologies,
            email: self.email,
            assignedTo: patientID)
        
        do {
            try AppDatabase.shared.db.write {db in
                try contact.insert(db)
            }
        } catch {
            print("Error")
        }
        
        // Return to patient view
        showModal = false
        
        // Show alert
        SPAlert.present(title: "Contact Added", preset: .add)
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
}
