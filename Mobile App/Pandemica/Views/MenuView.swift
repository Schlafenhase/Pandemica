//
//  MenuView.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/5/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import SwiftUI

struct MenuView: View {

    var body: some View {
        UIKitTabView([
            UIKitTabView.Tab(
                view: PatientView().attachDatabase(AppDatabase.shared.db),
                barItem: UITabBarItem(title: "Patients", image: UIImage(systemName: "person.fill"), selectedImage: nil)
            ),
            UIKitTabView.Tab(
                view: ReportView(),
                barItem: UITabBarItem(title: "Reports", image: UIImage(systemName: "square.and.arrow.down"), selectedImage: nil)
            ),
            UIKitTabView.Tab(
                view: SettingsView(),
                barItem: UITabBarItem(title: "Settings", image: UIImage(systemName: "gear"), selectedImage: nil)
            )
        ]).accentColor(Color("Tabs Color"))
    }
}

struct MenuView_Previews: PreviewProvider {
    static var previews: some View {
        MenuView().environmentObject(AppSession())
    }
}
