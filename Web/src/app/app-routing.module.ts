import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HealthCenterAuthGuard } from './services/auth/health-center.auth.guard';
import { AdminAuthGuard } from './services/auth/admin.auth.guard';
import { SecureInnerPagesGuard } from './services/auth/secure-inner-pages.guard';
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
import { UsersComponent } from './components/admin/tables/users/users.component';
import { AboutComponent } from './components/about/about.component';
import { UserAccessComponent } from './components/user-access/user-access.component';
import { HealthCenterComponent } from './components/health-center/health-center.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'auth', component: AuthComponent },
  { path: 'admin', component: AdminComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin/regions', component: RegionsComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin/pathologies', component: PathologiesComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin/patient-status', component: PatientStatusComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin/health-centers-table', component: HealthCentersTableComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin/sanitary-measures', component: SanitaryMeasuresComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin/country-measures', component: CountryMeasuresComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin/medication', component: MedicationComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin/users', component: UsersComponent, canActivate: [AdminAuthGuard] },
  { path: 'about', component: AboutComponent },
  { path: 'user-access', component: UserAccessComponent, canActivate: [SecureInnerPagesGuard] },
  { path: 'health-center', component: HealthCenterComponent, canActivate: [HealthCenterAuthGuard] },

  { path: '**', component: ErrorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
