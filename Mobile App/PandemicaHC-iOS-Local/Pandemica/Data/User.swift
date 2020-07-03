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
    var password: String
    var id: String
    var bedCount: String
    var UCICount: String
    var director: String
    var contact: String
}
