//
//  MotherView.swift
//  Pandemica
//
//  Created by Alejandro Ibarra on 6/5/20.
//  Copyright © 2020 Schlafenhase. All rights reserved.
//

import SwiftUI

struct MotherView : View {
    @EnvironmentObject var viewRouter: AppSession
    
    func getUser() {
        viewRouter.listen()
    }

    var body: some View {
        Group {
            if viewRouter.currentPage == "menu" && viewRouter.session != nil {
                MenuView().environmentObject(self.viewRouter)
                    .transition(.scale)
            } else {
                LogInView()
            }
        }.onAppear(perform: getUser)
    }
}

struct MotherView_Previews : PreviewProvider {
    static var previews: some View {
        MotherView().environmentObject(AppSession())
    }
}
