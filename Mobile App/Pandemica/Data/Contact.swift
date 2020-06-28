//
//  Contact.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/10/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import AS_GRDBSwiftUI
import Foundation
import GRDB

struct Contact {
    var id: Int64?
    
    var name: String
    var lastName: String
    var contactID: Int
    var age: Int
    var nationality: String
    var address: String
    var pathologies: String
    var email: String
    var assignedTo: Int
}

// Support for UIViewController (difference(from:).inferringMoves())
extension Contact: Hashable {}

// Support for SwiftUI (List)
extension Contact: Identifiable {}

// MARK: - Persistence

// Turn Contact into a Codable Record.
extension Contact: Codable, FetchableRecord, MutablePersistableRecord {
    // Define database columns from CodingKeys
    fileprivate enum Columns {
        static let id = Column(CodingKeys.id)
        static let name = Column(CodingKeys.name)
        static let lastNmame = Column(CodingKeys.lastName)
        static let contactID = Column(CodingKeys.contactID)
        static let age = Column(CodingKeys.age)
        static let nationality = Column(CodingKeys.nationality)
        static let address = Column(CodingKeys.address)
        static let pathologies = Column(CodingKeys.pathologies)
        static let email = Column(CodingKeys.email)
        static let assignedTo = Column(CodingKeys.assignedTo)
    }
    
    // Update a player id after it has been inserted in the database.
    mutating func didInsert(with rowID: Int64, for _: String?) {
        id = rowID
    }
}

// MARK: - Database access

// Define some useful contact requests.
extension Contact {
    static func orderedByName() -> QueryInterfaceRequest<Contact> {
        Contact.order(Columns.name)
    }
    
    static func orderedByScore() -> QueryInterfaceRequest<Contact> {
        Contact.order(Columns.contactID)
    }
}

// General database management
extension Contact {
    // Deletes specific contact in database
    struct DeleteRequest: GRDBWriteRequest {
        func onWrite(db: Database, value: Contact) throws {
            try value.delete(db)
        }
    }

    // Deletes all contacts in database
    struct DeleteAllContactsRequest: GRDBWriteRequest {
        func onWrite(db: Database, value: Void) throws {
            try Contact.deleteAll(db)
        }
    }
    
    // Fetches all contacts in database
    struct FetchAllContactsRequest: GRDBFetchRequest {
        var defaultResult: [Contact] = []
        
        func onRead(db: Database) throws -> [Contact] {
            let contacts = try Contact.fetchAll(db)
            
            return contacts
        }
    }
}
