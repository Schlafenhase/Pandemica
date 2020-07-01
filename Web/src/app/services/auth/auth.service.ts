import { Injectable, NgZone } from '@angular/core';
import { Admin } from '../data/users'
import { HealthCenter } from '../data/users';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFirestore, AngularFirestoreDocument } from '@angular/fire/firestore';
import { Router } from '@angular/router';
import { NetworkService } from '../network/network.service'

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
        if (role === 'admin') {
          // Initialize admin
          localStorage.setItem('role', 'admin');
          this.ngZone.run(() => {
            this.router.navigate(['admin']);
            this.SetAdminData(result.user);
            window.location.reload();
          });
        } else {
          // Initalize health center
          localStorage.setItem('role', 'health-center');
          this.ngZone.run(() => {
            this.router.navigate(['health-center']);
            window.location.reload();
          });
          this.SetHealthCenterData(result.user);
        }
      }).catch((error) => {
        window.alert(error.message)
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
        window.alert(role + ' was created successfully');
      }).catch((error) => {
        window.alert(error.message)
      })
  }

  /**
   * Sends email verification on new user sign up
   */
  SendVerificationMail() {
    return this.afAuth.currentUser.then(u => u.sendEmailVerification())
      .then(() => {
        window.alert('Verification email sent, check your inbox.');
      })
  }

  /**
   * Resets password of profile
   */
  ForgotPassword(passwordResetEmail) {
    return this.afAuth.sendPasswordResetEmail(passwordResetEmail)
      .then(() => {
        window.alert('Password reset email sent, check your inbox.');
      }).catch((error) => {
        window.alert(error)
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
   * Sets user data as health center
   */
  SetHealthCenterData(user) {
    const userRef: AngularFirestoreDocument<any> = this.afs.doc(`users/${user.uid}`);
    const healthCenterData: HealthCenter = {
      uid: user.uid,
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
   * Signs out of service
   */
  SignOut() {
    return this.afAuth.signOut().then(() => {
      localStorage.removeItem('user');
      localStorage.removeItem('role');
      this.router.navigate(['user-access']);
      window.location.reload();
    })
  }
}
