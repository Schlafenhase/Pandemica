export interface Admin {
  uid: string;
  email: string;
  emailVerified: boolean;
  country: string;
  continent: string;
}

export interface HealthCenter {
  uid: string;
  id: number;
  email: string;
  name: string;
  phone: string;
  emailVerified: string;
  managerName: string;
  capacity: string;
  icuCapacity: string;
  country: string;
  region: string;
}

export interface Doctor {
  uid: string;
  email: string;
  emailVerified: boolean;
  ssn: string;
  name: string;
  lastName: string;
  phone: string;
  birthdate: string;
  role: string;
  hospitalId: string;
  address: string;
  startdate: string;
  sex: string;
}

export interface User {
  uid: string;
  email: string;
  emailVerified: boolean;
  name: string;
  lastName: string;
  ssn: string;
  birthdate: string;
  isHospitalized: boolean;
  isInICU: boolean;
  country: string;
  region: string;
  nationality: string;
  sex: string;
  hospital: string;
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
  measurements: Measurement[];
  accumulated: any[];
  regions: Region[];
}

export interface Measurement {
  name: string;
  state: string;
  until: string;
}

export interface Region {
  confirmed: number;
  active: number;
  recovered: number;
  dead: number;
  region: string;
}
