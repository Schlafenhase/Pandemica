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
