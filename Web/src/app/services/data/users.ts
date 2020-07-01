export interface Admin {
  uid: string;
  email: string;
  emailVerified: boolean;
  country: string;
  continent: string;
}

export interface HealthCenter {
  uid: string;
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
