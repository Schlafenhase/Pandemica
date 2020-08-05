import { Injectable, NgZone } from '@angular/core';
import { Admin, HealthCenter, Doctor, User } from '../data/users'
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFirestore, AngularFirestoreDocument } from '@angular/fire/firestore';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  userData: any;

  constructor(
    public afs: AngularFirestore,
    public afAuth: AngularFireAuth,
    public router: Router,
    public ngZone: NgZone
  ) {
    /* Saving user data in localstorage when
    logged in and setting up null when logged out */
    this.afAuth.authState.subscribe(user => {
      if (user) {
        this.userData = user;
        localStorage.setItem('user', JSON.stringify(this.userData));
        JSON.parse(localStorage.getItem('user'));
      } else {
        localStorage.setItem('user', null);
        JSON.parse(localStorage.getItem('user'));
      }
    })
  }

  get isLoggedIn(): boolean {
    const user = JSON.parse(localStorage.getItem('user'));
    return (user !== null) ? true : false;
  }

  /**
   * Signs in with email, password and role
   */
  SignIn(email, password, role) {
    return this.afAuth.signInWithEmailAndPassword(email, password)
      .then((result) => {
        // Report successful login
        switch (role) {
          case 'user':
            // Initalize user
            localStorage.setItem('role', 'user');
            this.ngZone.run(() => {
              this.router.navigate(['user-dashboard']);
              window.location.reload();
            });
            this.SetUserData(result.user);
            break;
          case 'admin':
            // Initialize admin
            localStorage.setItem('role', 'admin');
            this.ngZone.run(() => {
              this.router.navigate(['admin-dashboard']);
              this.SetAdminData(result.user);
              window.location.reload();
            });
            break;
          case 'health-center':
            // Initalize health center
            localStorage.setItem('role', 'health-center');
            this.ngZone.run(() => {
              this.router.navigate(['health-center-dashboard']);
              window.location.reload();
            });
            this.SetHealthCenterData(result.user);
            break;
          case 'doctor':
            // Initalize doctor
            localStorage.setItem('role', 'doctor');
            this.ngZone.run(() => {
              this.router.navigate(['doctor-dashboard']);
              window.location.reload();
            });
            this.SetDoctorData(result.user);
            break;
        }
      }).catch((error) => {
        Swal.fire({
          title: 'error!',
          html: 'can\'t log in: ' + error.message,
          icon: 'error',
          customClass: {
            popup: 'container-alert'
          }
        })
      })
  }

  /**
   * Signs up new user with role
   */
  SignUp(email, password, role) {
    return this.afAuth.createUserWithEmailAndPassword(email, password)
      .then((result) => {
        /* Call the SendVerificationMail() function when new user sign
        up and returns promise */
        this.SendVerificationMail();
        //
        // CONNECT TO API AND CREATE USER
        //
      }).catch((error) => {
        Swal.fire({
          title: 'error!',
          html: 'can\'t create user' + error.message,
          icon: 'error',
          customClass: {
            popup: 'container-alert'
          }
        })
      })
  }

  /**
   * Sends email verification on new user sign up
   */
  SendVerificationMail() {
    return this.afAuth.currentUser.then(u => u.sendEmailVerification())
      .then(() => {
        Swal.fire({
            title: 'success!',
            html: 'email verification sent. please check your inbox',
            icon: 'success',
            customClass: {
              popup: 'container-alert'
            }
        })
      })
  }

  /**
   * Resets password of profile
   */
  ForgotPassword(passwordResetEmail) {
    return this.afAuth.sendPasswordResetEmail(passwordResetEmail)
      .then(() => {
        Swal.fire({
          title: 'success!',
          html: 'password reset sent. please check your inbox',
          icon: 'success',
          customClass: {
            popup: 'container-alert'
          }
        })
      }).catch((error) => {
        Swal.fire({
          title: 'error',
          html: 'error: ' + error,
          icon: 'error',
          customClass: {
            popup: 'container-alert'
          }
        })
      })
  }

  /**
   * Sets user data as admin
   */
  SetAdminData(user) {
    const userRef: AngularFirestoreDocument<any> = this.afs.doc(`users/${user.uid}`);
    const adminData: Admin = {
      uid: user.uid,
      email: user.email,
      emailVerified: user.emailVerified,
      country: 'null',
      continent: 'null'
    };
    localStorage.setItem('userData', JSON.stringify(adminData));
    return userRef.set(adminData, {
      merge: true
    })
  }

  /**
   * Sets user data as doctor
   */
  SetDoctorData(user) {
    const userRef: AngularFirestoreDocument<any> = this.afs.doc(`users/${user.uid}`);
    const doctorData: Doctor = {
      uid: user.uid,
      email: user.email,
      emailVerified: user.emailVerified,
      ssn: '',
      name: '',
      lastName: '',
      phone: '',
      birthdate: '',
      field: '',
    };
    localStorage.setItem('userData', JSON.stringify(doctorData));
    return userRef.set(doctorData, {
      merge: true
    })
  }

  /**
   * Sets user data as health center
   */
  SetHealthCenterData(user) {
    const userRef: AngularFirestoreDocument<any> = this.afs.doc(`users/${user.uid}`);
    const healthCenterData: HealthCenter = {
      uid: user.uid,
      id: 0,
      email: user.email,
      name: user.displayName,
      phone: '',
      emailVerified: user.emailVerified,
      managerName: '',
      capacity: '',
      icuCapacity: '',
      country: '',
      region: ''
    };
    localStorage.setItem('userData', JSON.stringify(healthCenterData));
    return userRef.set(healthCenterData, {
      merge: true
    })
  }

  /**
   * Sets user data as user (patient)
   */
  SetUserData(user) {
    const userRef: AngularFirestoreDocument<any> = this.afs.doc(`users/${user.uid}`);
    const userData: User = {
      uid: user.uid,
      email: user.email,
      emailVerified: user.emailVerified,
      name: '',
      lastName: '',
      ssn: '',
      birthdate: '',
      isHospitalized: false,
      isInICU: false,
      country: '',
      region: '',
      nationality: '',
      sex: ''
    };
    localStorage.setItem('userData', JSON.stringify(userData));
    return userRef.set(userData, {
      merge: true
    })
  }

  /**
   * Signs out of service
   */
  SignOut() {
    return this.afAuth.signOut().then(() => {
      localStorage.removeItem('user');
      localStorage.removeItem('userData');
      localStorage.removeItem('role');
      this.router.navigate(['home']);
      window.location.reload();
    })
  }
}
