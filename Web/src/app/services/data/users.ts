export interface Admin {
  uid: string;
  email: string;
  displayName: string;
  name: string;
  emailVerified: boolean;
  country: string;
}

export interface HealthCenter {
  uid: string;
  email: string;
  displayName: string;
  name: string;
  emailVerified: boolean
  region: string;
  country: string;
  bed_count: string;
  icu_count: string;
  director: string;
  contact: string;
}

export interface IHomeView {
  confirmedCases: number;
  activeCases: number;
  deadths: number;
  recovered: number;
  nationals: number;
  foreigns: number;
  todayNewCases: number;
  todayRecovered: number;
  todayDeceased: number;
  patientsAtHome: number;
  patientsHospitalized: number;
  patientsInICU: number;
  femaleCases: number;
  maleCases: number;
  '0-12': number;
  '13-20': number;
  '21-39': number;
  '40-59': number;
  '60-79': number;
  '+80': number;

}
