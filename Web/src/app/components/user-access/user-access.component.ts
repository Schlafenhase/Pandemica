import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-user-access',
  templateUrl: './user-access.component.html',
  styleUrls: ['./user-access.component.scss']
})
export class UserAccessComponent implements OnInit {
  signInForm;
  signUpForm;

  constructor(
    public authService: AuthService,
    private formBuilder: FormBuilder,
  ) {
    // Initialize Sign In Form
    this.signInForm = this.formBuilder.group({
      userEmail: '',
      userPassword: ''
    });

    // Initialize Sign Up Form
    this.signUpForm = this.formBuilder.group({
      newEmail: '',
      newPassword: '',
      newName: '',
      newLastName: '',
      newSSN: '',
      newPhone: '',
      newAddress: '',
      newBirthdate: ''
    });
  }

  ngOnInit(): void {
  }

  /*
  Register user using auth service
   */
  signUp(email, password, name, lastName, id, phone, address, birthdate) {
    this.authService.SignUp(email, password, 'user')
  }

  /*
  Signs in user using auth service
    */
  signIn(email, password) {
    const role = 'user'
    this.authService.SignIn(email, password, role);
    this.authService.SignIn(email, password, role);
  }
}



