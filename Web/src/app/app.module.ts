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
import {MapComponent} from './components/map/map.component';
import {MapShapeService} from './services/map-shape.service';
import {NgxChartsModule} from '@swimlane/ngx-charts';
import { PatientsChartComponent } from './components/charts/patients-chart/patients-chart.component';
import { GenderChartComponent } from './components/charts/gender-chart/gender-chart.component';
import { AgeChartComponent } from './components/charts/age-chart/age-chart.component';
import { AccumulatedChartComponent } from './components/charts/accumulated-chart/accumulated-chart.component';
import { RegionChartComponent } from './components/charts/region-chart/region-chart.component';
import { FooterComponent } from './components/footer/footer.component';
import { ErrorComponent } from './components/error/error.component';
import { AdminComponent } from './components/admin/admin.component';
import { AboutComponent } from './components/about/about.component';
import { UserAccessComponent } from './components/user-access/user-access.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    AuthComponent,
    MapComponent,
    PatientsChartComponent,
    GenderChartComponent,
    AgeChartComponent,
    AccumulatedChartComponent,
    RegionChartComponent,
    FooterComponent,
    ErrorComponent,
    AdminComponent,
    AboutComponent,
    UserAccessComponent
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
    MatPasswordStrengthModule,
    NgxChartsModule
  ],
  providers: [
    MapShapeService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
