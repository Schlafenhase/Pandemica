//
//  Patient.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/5/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import Foundation
import GRDB
import AS_GRDBSwiftUI

struct Patient {
    var id: Int64?
    
    var name: String
    var lastName: String
    var patientID: Int
    var age: Int
    var nationality: String
    var region: String
    var pathologies: String
    var status: String
    var medication: String
    var isHospitalized: Bool
    var isInUCI: Bool
}

// Support for SwiftUI (List)
extension Patient: Identifiable {}

// MARK: - Persistence

// Turn Patient into a Codable Record.
extension Patient: Codable, FetchableRecord, MutablePersistableRecord {
    // Define database columns from CodingKeys
    fileprivate enum Columns {
        static let id = Column(CodingKeys.id)
        static let name = Column(CodingKeys.name)
        static let lastNmame = Column(CodingKeys.lastName)
        static let patientID = Column(CodingKeys.patientID)
        static let age = Column(CodingKeys.age)
        static let nationality = Column(CodingKeys.nationality)
        static let region = Column(CodingKeys.region)
        static let pathologies = Column(CodingKeys.pathologies)
        static let status = Column(CodingKeys.status)
        static let medication = Column(CodingKeys.medication)
        static let isHospitalized = Column(CodingKeys.isHospitalized)
        static let isInUCI = Column(CodingKeys.isInUCI)
    }
    
    // Update a player id after it has been inserted in the database.
    mutating func didInsert(with rowID: Int64, for _: String?) {
        id = rowID
    }
}

// MARK: - Database access

// Define some useful patient requests.
extension Patient {
    static func orderedByName() -> QueryInterfaceRequest<Patient> {
        Patient.order(Columns.name)
    }
    
    static func orderedByScore() -> QueryInterfaceRequest<Patient> {
        Patient.order(Columns.patientID)
    }
}

// General database management
extension Patient {
    // Deletes specific patient in database
    struct DeleteRequest: GRDBWriteRequest {
        func onWrite(db: Database, value: Patient) throws {
            try value.delete(db)
        }
    }

    // Deletes all patients in database
    struct DeleteAllPatientsRequest: GRDBWriteRequest {
        func onWrite(db: Database, value: Void) throws {
            try Patient.deleteAll(db)
        }
    }
    
    // Fetches all patients in database
    struct FetchAllPatientsRequest: GRDBFetchRequest {
        var defaultResult: [Patient] = []
        
        func onRead(db: Database) throws -> [Patient] {
            let patients = try Patient.fetchAll(db)
            
            return patients
        }
    }
}
