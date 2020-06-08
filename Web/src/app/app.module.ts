import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { NgxAuthFirebaseUIModule } from 'ngx-auth-firebaseui';
import { AuthComponent } from './components/auth/auth.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatPasswordStrengthModule } from '@angular-material-extensions/password-strength';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    AuthComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgxAuthFirebaseUIModule.forRoot({
      apiKey: 'AIzaSyCeUVOj8cB5nwhVWa2jxvrc7EgDsGMzGm0',
      authDomain: 'pandemica-259ec.firebaseapp.com',
      databaseURL: 'https://pandemica-259ec.firebaseio.com',
      projectId: 'pandemica-259ec',
      storageBucket: 'pandemica-259ec.appspot.com',
      messagingSenderId: '321004965436',
      appId: '1:321004965436:web:65ed422c723b1096eefd39',
      measurementId: 'G-3RGXR1MLNC'
    }),
    BrowserAnimationsModule,
    MatPasswordStrengthModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
