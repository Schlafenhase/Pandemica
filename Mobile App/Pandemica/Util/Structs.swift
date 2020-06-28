//
//  Structs.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/11/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import Foundation
import SwiftUI

// Helps to wrap UIKitTabView for use in SwiftUI
struct UIKitTabView: View {
    var viewControllers: [UIHostingController<AnyView>]

    init(_ tabs: [Tab]) {
        self.viewControllers = tabs.map {
            let host = UIHostingController(rootView: $0.view)
            host.tabBarItem = $0.barItem
            return host
        }
    }

    var body: some View {
        TabBarController(controllers: viewControllers)
            .edgesIgnoringSafeArea(.all)
    }

    struct Tab {
        var view: AnyView
        var barItem: UITabBarItem

        init<V: View>(view: V, barItem: UITabBarItem) {
            self.view = AnyView(view)
            self.barItem = barItem
        }
    }
}
struct TabBarController: UIViewControllerRepresentable {
    var controllers: [UIViewController]

    func makeUIViewController(context: Context) -> UITabBarController {
        let tabBarController = UITabBarController()
        tabBarController.viewControllers = controllers
        return tabBarController
    }

    func updateUIViewController(_ uiViewController: UITabBarController, context: Context) {

    }
}
