import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HealthCenterAuthGuard } from './services/auth/health-center.auth.guard';
import { AdminAuthGuard } from './services/auth/admin.auth.guard';
import { SecureInnerPagesGuard } from './services/auth/secure-inner-pages.guard';
import { HomeComponent } from './components/home/home.component';
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
import { HealthCenterComponent } from './components/health-center-dashboard/health-center.component';
import { WorkerAccessComponent } from './components/worker-access/worker-access.component';
import { DoctorDashboardComponent } from './components/doctor-dashboard/doctor-dashboard.component';
import { DoctorAuthGuard } from './services/auth/doctor.auth.guard';
import { UserDashboardComponent } from './components/user-dashboard/user-dashboard.component';
import { UserAuthGuard } from './services/auth/user.auth.guard';
import {LoungesComponent} from './components/health-center-dashboard/plus/lounges/lounges.component';
import {EquipmentComponent} from './components/health-center-dashboard/plus/equipment/equipment.component';
import {BedsComponent} from './components/health-center-dashboard/plus/beds/beds.component';
import {ProceduresComponent} from './components/health-center-dashboard/plus/procedures/procedures.component';
import {HealthWorkersComponent} from './components/health-center-dashboard/plus/health-workers/health-workers.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'admin-dashboard', component: AdminComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin-dashboard/regions', component: RegionsComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin-dashboard/pathologies', component: PathologiesComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin-dashboard/patient-status', component: PatientStatusComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin-dashboard/health-centers-table', component: HealthCentersTableComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin-dashboard/sanitary-measures', component: SanitaryMeasuresComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin-dashboard/country-measures', component: CountryMeasuresComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin-dashboard/medication', component: MedicationComponent, canActivate: [AdminAuthGuard] },
  { path: 'admin-dashboard/users', component: UsersComponent, canActivate: [AdminAuthGuard] },
  { path: 'about', component: AboutComponent },
  { path: 'user-access', component: UserAccessComponent, canActivate: [SecureInnerPagesGuard] },
  { path: 'worker-access', component: WorkerAccessComponent, canActivate: [SecureInnerPagesGuard] },
  { path: 'health-center-dashboard', component: HealthCenterComponent, canActivate: [HealthCenterAuthGuard] },
  { path: 'health-center-dashboard/lounges', component: LoungesComponent, canActivate: [HealthCenterAuthGuard] },
  { path: 'health-center-dashboard/equipment', component: EquipmentComponent, canActivate: [HealthCenterAuthGuard] },
  { path: 'health-center-dashboard/beds', component: BedsComponent, canActivate: [HealthCenterAuthGuard] },
  { path: 'health-center-dashboard/procedures', component: ProceduresComponent, canActivate: [HealthCenterAuthGuard] },
  { path: 'health-center-dashboard/health-workers', component: HealthWorkersComponent, canActivate: [HealthCenterAuthGuard] },
  { path: 'doctor-dashboard', component: DoctorDashboardComponent, canActivate: [DoctorAuthGuard] },
  { path: 'user-dashboard', component: UserDashboardComponent, canActivate: [UserAuthGuard] },

  { path: '**', component: ErrorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
