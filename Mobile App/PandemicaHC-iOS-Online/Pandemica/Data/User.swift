//
//  User.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/5/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import Foundation

struct User: Codable {
    var name: String
    var location: String
    var eMail: String
    var id: String
    var bedCount: String
    var ICUCount: String
    var director: String
    var contact: String
    var fUID: String
}

struct HealthCenter: Codable {
    var id: Int
    var name: String
    var phone: Int
    var managerName: String
    var capacity: Int
    var icuCapacity: Int
    var country: String
    var region: String
    var eMail: String
}
