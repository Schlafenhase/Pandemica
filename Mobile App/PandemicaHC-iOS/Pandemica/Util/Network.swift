//
//  Network.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 7/1/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import Foundation
import Alamofire


class Network {
    
    // Initialize custom Alamofire session without server evaluators for testing
    private let session: Session = {
        let manager = ServerTrustManager(evaluators: ["192.168.0.15": DisabledEvaluator()])
        let configuration = URLSessionConfiguration.af.default

        return Session(configuration: configuration, serverTrustManager: manager)
    }()
    
    
}
