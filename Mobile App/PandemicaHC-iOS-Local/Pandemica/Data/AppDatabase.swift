//
//  AppDatabase.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/8/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import Foundation
import GRDB

/// A type responsible for initializing the application database.
///
/// See AppDelegate.setupDatabase()
struct AppDatabase {
    
    static var shared = AppDatabase.loadSharedDatabase()

    static func loadSharedDatabase() -> AppDatabase {
        do {
            let databaseURL = try FileManager.default
                .url(for: .applicationSupportDirectory, in: .userDomainMask, appropriateFor: nil, create: true)
                .appendingPathComponent("db.sqlite")

            return try self.init(withDatabasePath: databaseURL.path)
        }
        catch {
            fatalError("Couldn't load main application database")
        }
    }
    
    let db: DatabasePool

    init(withDatabasePath path: String) throws {
        // Connect to the database
        // See https://github.com/groue/GRDB.swift/blob/master/README.md#database-connections
        db = try DatabasePool(path: path)
        
        // Define the database schema
        try migrator.migrate(db)
    }
    
    /// The DatabaseMigrator that defines the database schema.
    ///
    /// See https://github.com/groue/GRDB.swift/blob/master/Documentation/Migrations.md
    var migrator: DatabaseMigrator {
        var migrator = DatabaseMigrator()
        
        migrator.registerMigration("v1") { db in
            // Initial tables
            try db.create(table: "patient") { t in
                t.autoIncrementedPrimaryKey("id")
                
                t.column("name", .text).notNull()
                t.column("lastName", .text).notNull()
                t.column("patientID", .integer).notNull()
                t.column("age", .integer).notNull()
                t.column("nationality", .text).notNull()
                t.column("region", .text).notNull()
                t.column("pathologies", .text).notNull()
                t.column("status", .text).notNull()
                t.column("medication", .text).notNull()
                t.column("isHospitalized", .boolean).notNull()
                t.column("isInUCI", .boolean).notNull()
            }
            
            try db.create(table: "contact") { t in
                t.autoIncrementedPrimaryKey("id")
                
                t.column("name", .text).notNull()
                t.column("lastName", .text).notNull()
                t.column("contactID", .integer).notNull()
                t.column("age", .integer).notNull()
                t.column("nationality", .text).notNull()
                t.column("address", .text).notNull()
                t.column("pathologies", .text).notNull()
                t.column("email", .text).notNull()
                t.column("assignedTo", .integer).notNull()
            }
        }
        
        migrator.registerMigration("v2") { db in
            // Initial patients and contacts
            var c1 = Contact(id: nil, name: "Quebin", lastName: "Cordero", contactID: 1, age: 20, nationality: "Costa Rica", address: "San Ramon", pathologies: "nothing", email: "kscorzu@gmail.com", assignedTo: 118920182)
            var c2 = Contact(id: nil, name: "Jesus", lastName: "Sandoval", contactID: 2, age: 20, nationality: "Costa Rica", address: "Orotina", pathologies: "nothing", email: "jose.ibarra@hotmail.com", assignedTo: 118920182)
            var c3 = Contact(id: nil, name: "Hannia", lastName: "Salas", contactID: 3, age: 64, nationality: "Costa Rica", address: "Grecia", pathologies: "nothing", email: "hsalas@hotmail.com", assignedTo: 118920182)
            var c4 = Contact(id: nil, name: "Carlos", lastName: "Campos", contactID: 4, age: 54, nationality: "Costa Rica", address: "Sabanilla", pathologies: "nothing", email: "ccampos@hotmail.com", assignedTo: 118920182)
            var c5 = Contact(id: nil, name: "Raquel", lastName: "Quesada", contactID: 5, age: 20, nationality: "Costa Rica", address: "Santa Gertrudis", pathologies: "nothing", email: "raquesol@hotmail.com", assignedTo: 118920182)
            
            var c6 = Contact(id: nil, name: "Quebin", lastName: "Cordero", contactID: 1, age: 20, nationality: "Costa Rica", address: "San Ramon", pathologies: "nothing", email: "kscorzu@gmail.com", assignedTo: 117890851)
            var c7 = Contact(id: nil, name: "Jesus", lastName: "Sandoval", contactID: 2, age: 20, nationality: "Costa Rica", address: "Orotina", pathologies: "nothing", email: "jose.ibarra@hotmail.com", assignedTo: 117890851)
            var c8 = Contact(id: nil, name: "Hannia", lastName: "Salas", contactID: 3, age: 64, nationality: "Costa Rica", address: "Grecia", pathologies: "nothing", email: "hsalas@hotmail.com", assignedTo: 117890851)
            var c9 = Contact(id: nil, name: "Carlos", lastName: "Campos", contactID: 4, age: 54, nationality: "Costa Rica", address: "Sabanilla", pathologies: "nothing", email: "ccampos@hotmail.com", assignedTo: 117890851)
            var c10 = Contact(id: nil, name: "Raquel", lastName: "Quesada", contactID: 5, age: 20, nationality: "Costa Rica", address: "Santa Gertrudis", pathologies: "nothing", email: "raquesol@hotmail.com", assignedTo: 117890851)
            
            var p1 = Patient(id: nil, name: "Alejandro", lastName: "Ibarra", patientID: 117890851, age: 19, nationality: "Costa Rica", region: "The Americas", pathologies: "sick burns, tooth pain", status: "Alive", medication: "Aderall", isHospitalized: true, isInUCI: false)
            var p2 = Patient(id: nil, name: "Jose Daniel", lastName: "Acuña", patientID: 118920182, age: 20, nationality: "Costa Rica", region: "The Americas", pathologies: "real sick", status: "Dead", medication: "The Blue Pill", isHospitalized: false, isInUCI: false)
            
            try p1.insert(db)
            try p2.insert(db)
            
            try c1.insert(db)
            try c2.insert(db)
            try c3.insert(db)
            try c4.insert(db)
            try c5.insert(db)
            try c6.insert(db)
            try c7.insert(db)
            try c8.insert(db)
            try c9.insert(db)
            try c10.insert(db)
        }
        
//        // Migrations for future application versions will be inserted here:
//        migrator.registerMigration(...) { db in
//            ...
//        }
        
        return migrator
    }
}
