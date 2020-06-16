import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

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
import { HealthCenterComponent } from './components/health-center/health-center.component';
import { RegionsComponent } from './components/admin/tables/regions/regions.component';
import { PathologiesComponent } from './components/admin/tables/pathologies/pathologies.component';
import { PatientStatusComponent } from './components/admin/tables/patient-status/patient-status.component';
import { HealthCentersTableComponent } from './components/admin/tables/health-centers-table/health-centers-table.component';
import { SanitaryMeasuresComponent } from './components/admin/tables/sanitary-measures/sanitary-measures.component';
import { CountryMeasuresComponent } from './components/admin/tables/country-measures/country-measures.component';
import { MedicationComponent } from './components/admin/tables/medication/medication.component';
import { PatientContactsComponent } from './components/health-center/patient-contacts/patient-contacts.component';
import { UsersComponent } from './components/admin/tables/users/users.component';

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
    UserAccessComponent,
    HealthCenterComponent,
    RegionsComponent,
    PathologiesComponent,
    PatientStatusComponent,
    HealthCentersTableComponent,
    SanitaryMeasuresComponent,
    CountryMeasuresComponent,
    MedicationComponent,
    PatientContactsComponent,
    UsersComponent
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
    NgxChartsModule,
    FormsModule
  ],
  providers: [
    MapShapeService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
