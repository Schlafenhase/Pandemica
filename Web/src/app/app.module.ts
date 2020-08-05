import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AngularFireModule } from '@angular/fire';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { AngularFirestoreModule } from '@angular/fire/firestore';
import { environment } from '../environments/environment';
import { AuthService } from './services/auth/auth.service';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { NgxAuthFirebaseUIModule } from 'ngx-auth-firebaseui';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { KeysPipe } from './services/keys.pipe';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatPasswordStrengthModule } from '@angular-material-extensions/password-strength';
import { MapComponent } from './components/map/map.component';
import { MapShapeService } from './services/map/map-shape.service';
import { NgxChartsModule } from '@swimlane/ngx-charts';
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
import { HealthCenterComponent } from './components/health-center-dashboard/health-center.component';
import { RegionsComponent } from './components/admin/tables/regions/regions.component';
import { PathologiesComponent } from './components/admin/tables/pathologies/pathologies.component';
import { PatientStatusComponent } from './components/admin/tables/patient-status/patient-status.component';
import { HealthCentersTableComponent } from './components/admin/tables/health-centers-table/health-centers-table.component';
import { SanitaryMeasuresComponent } from './components/admin/tables/sanitary-measures/sanitary-measures.component';
import { CountryMeasuresComponent } from './components/admin/tables/country-measures/country-measures.component';
import { MedicationComponent } from './components/admin/tables/medication/medication.component';
import { UsersComponent } from './components/admin/tables/users/users.component';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatDialogModule} from '@angular/material/dialog';
import {ReactiveFormsModule } from '@angular/forms';
import { CountryMeasuresPopupComponent } from './components/admin/tables/country-measures/country-measures-popup/country-measures-popup.component';
import { HealthCentersTablePopupComponent } from './components/admin/tables/health-centers-table/health-centers-table-popup/health-centers-table-popup.component';
import { MedicationPopupComponent } from './components/admin/tables/medication/medication-popup/medication-popup.component';
import { PathologiesPopupComponent } from './components/admin/tables/pathologies/pathologies-popup/pathologies-popup.component';
import { PatientStatusPopupComponent } from './components/admin/tables/patient-status/patient-status-popup/patient-status-popup.component';
import { RegionsPopupComponent } from './components/admin/tables/regions/regions-popup/regions-popup.component';
import { SanitaryMeasuresPopupComponent } from './components/admin/tables/sanitary-measures/sanitary-measures-popup/sanitary-measures-popup.component';
import { UsersPopupComponent } from './components/admin/tables/users/users-popup/users-popup.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule} from '@angular/material/form-field';

import { DatePipe } from '@angular/common';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import {MatPaginatorModule} from '@angular/material/paginator';
import { ContactsComponent } from './components/health-center-dashboard/contacts/contacts.component';
import { ContactsPopupComponent } from './components/health-center-dashboard/contacts/contacts-popup/contacts-popup.component';
import { HealthCenterPopupComponent } from './components/health-center-dashboard/health-center-popup/health-center-popup.component';
import {MatSelectModule} from '@angular/material/select';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { StatesComponent } from './components/health-center-dashboard/states/states.component';
import { MedicationsComponent } from './components/health-center-dashboard/medications/medications.component';
import { PatientPathologiesComponent } from './components/health-center-dashboard/patient-pathologies/patient-pathologies.component';
import { StatesPopupComponent } from './components/health-center-dashboard/states/states-popup/states-popup.component';
import { MedicationsPopupComponent } from './components/health-center-dashboard/medications/medications-popup/medications-popup.component';
import { PatientPathologiesPopupComponent } from './components/health-center-dashboard/patient-pathologies/patient-pathologies-popup/patient-pathologies-popup.component';
import {ExtendedModule} from '@angular/flex-layout';
import { WorkerAccessComponent } from './components/worker-access/worker-access.component';
import { UserDashboardComponent } from './components/user-dashboard/user-dashboard.component';
import { DoctorDashboardComponent } from './components/doctor-dashboard/doctor-dashboard.component';
import { MedicalHistoryComponent } from './components/health-center-dashboard/medical-history/medical-history.component';
import { MedicalHistoryPopupComponent } from './components/health-center-dashboard/medical-history/medical-history-popup/medical-history-popup.component';
import { LoungesComponent } from './components/health-center-dashboard/plus/lounges/lounges.component';
import { EquipmentComponent } from './components/health-center-dashboard/plus/equipment/equipment.component';
import { BedsComponent} from './components/health-center-dashboard/plus/beds/beds.component';
import { ProceduresComponent } from './components/health-center-dashboard/plus/procedures/procedures.component';
import { HealthWorkersComponent } from './components/health-center-dashboard/plus/health-workers/health-workers.component';
import { ReservationsComponent } from './components/health-center-dashboard/reservations/reservations.component';
import { ContactsUpgradeComponent } from './components/health-center-dashboard/contacts/contacts-upgrade/contacts-upgrade.component';
import { ReservationsPopupComponent } from './components/health-center-dashboard/reservations/reservations-popup/reservations-popup.component';
import {MatRadioModule} from '@angular/material/radio';
import { BedsPopupComponent} from './components/health-center-dashboard/plus/beds/beds-popup/beds-popup.component';
import { LoungesPopupComponent } from './components/health-center-dashboard/plus/lounges/lounges-popup/lounges-popup.component';
import { EquipmentPopupComponent } from './components/health-center-dashboard/plus/equipment/equipment-popup/equipment-popup.component';
import { HealthWorkersPopupComponent } from './components/health-center-dashboard/plus/health-workers/health-workers-popup/health-workers-popup.component';
import { ProceduresPopupComponent } from './components/health-center-dashboard/plus/procedures/procedures-popup/procedures-popup.component';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { ReservationsPopupProceduresComponent } from './components/health-center-dashboard/reservations/reservations-popup-procedures/reservations-popup-procedures.component';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
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
    UsersComponent,
    KeysPipe,
    CountryMeasuresPopupComponent,
    HealthCentersTablePopupComponent,
    MedicationPopupComponent,
    PathologiesPopupComponent,
    PatientStatusPopupComponent,
    RegionsPopupComponent,
    SanitaryMeasuresPopupComponent,
    UsersPopupComponent,
    ContactsComponent,
    ContactsPopupComponent,
    HealthCenterPopupComponent,
    StatesComponent,
    MedicationsComponent,
    PatientPathologiesComponent,
    StatesPopupComponent,
    MedicationsPopupComponent,
    PatientPathologiesPopupComponent,
    WorkerAccessComponent,
    UserDashboardComponent,
    DoctorDashboardComponent,
    MedicalHistoryComponent,
    MedicalHistoryPopupComponent,
    LoungesComponent,
    EquipmentComponent,
    BedsComponent,
    ProceduresComponent,
    HealthWorkersComponent,
    ReservationsComponent,
    ContactsUpgradeComponent,
    ReservationsPopupComponent,
    BedsPopupComponent,
    LoungesPopupComponent,
    EquipmentPopupComponent,
    HealthWorkersPopupComponent,
    ProceduresPopupComponent,
    ReservationsPopupProceduresComponent,
  ],
  imports: [
    AngularFireModule.initializeApp(environment.firebaseConfig),
    AngularFireAuthModule,
    AngularFirestoreModule,
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
    FormsModule,
    NgxDropzoneModule,
    MatCardModule,
    MatIconModule,
    MatToolbarModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatSelectModule,
    MatCheckboxModule,
    ExtendedModule,
    MatRadioModule,
    [SweetAlert2Module.forRoot({
    })],
    [SweetAlert2Module],
    [SweetAlert2Module.forChild({ /* options */ })]
  ],
  providers: [
    MapShapeService,
    AuthService,
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
