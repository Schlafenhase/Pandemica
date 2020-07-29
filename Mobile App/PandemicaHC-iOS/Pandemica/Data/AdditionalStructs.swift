//
//  AdditionalStructs.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 7/2/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import Foundation

struct SpecialPatientState: Codable {
    var name: String
    var date: String
}

struct PatientPathologies: Codable {
    var name: String
    var symptoms: String
    var description: String
    var treatment: String
}

struct PatientMedication: Codable {
    var name: String
    var pharmacy: String
}

struct Person: Codable {
    var patientSsn: String
    var contactDate: String
    var ssn: String
    var firstName: String
    var lastName: String
    var birthDate: String
    var eMail: String
    var address: String
    var sex: String
}

struct PersonWithDateOfContact: Codable {
    var contactDate: String
    var ssn: String
    var firstName: String
    var lastName: String
    var birthDate: String
    var eMail: String
    var address: String
    var sex: String
}
