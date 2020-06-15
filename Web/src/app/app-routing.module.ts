import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AuthComponent } from './components/auth/auth.component';
import { ErrorComponent } from './components/error/error.component';
import { AdminComponent } from './components/admin/admin.component';
import { RegionsComponent } from './components/admin/tables/regions/regions.component';
import { PathologiesComponent } from './components/admin/tables/pathologies/pathologies.component';
import { PatientStatusComponent } from './components/admin/tables/patient-status/patient-status.component';
import { HealthCentersTableComponent } from './components/admin/tables/health-centers-table/health-centers-table.component';
import { SanitaryMeasuresComponent } from './components/admin/tables/sanitary-measures/sanitary-measures.component';
import { CountryMeasuresComponent } from './components/admin/tables/country-measures/country-measures.component';
import { MedicationComponent } from './components/admin/tables/medication/medication.component';
import { AboutComponent } from './components/about/about.component';
import { UserAccessComponent } from './components/user-access/user-access.component';
import { HealthCenterComponent } from './components/health-center/health-center.component';
import { PatientContactsComponent } from './components/health-center/patient-contacts/patient-contacts.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'auth', component: AuthComponent },
  { path: 'admin', component: AdminComponent },
  { path: 'admin/regions', component: RegionsComponent },
  { path: 'admin/pathologies', component: PathologiesComponent },
  { path: 'admin/patient-status', component: PatientStatusComponent },
  { path: 'admin/health-centers-table', component: HealthCentersTableComponent },
  { path: 'admin/sanitary-measures', component: SanitaryMeasuresComponent },
  { path: 'admin/country-measures', component: CountryMeasuresComponent },
  { path: 'admin/medication', component: MedicationComponent },
  { path: 'about', component: AboutComponent },
  { path: 'user-access', component: UserAccessComponent },
  { path: 'health-center', component: HealthCenterComponent },
  { path: 'health-center/contacts', component: PatientContactsComponent },

  { path: '**', component: ErrorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
