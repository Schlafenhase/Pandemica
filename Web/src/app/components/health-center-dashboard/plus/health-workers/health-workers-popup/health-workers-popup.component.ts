import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';
import Swal from 'sweetalert2';
import {AuthService} from '../../../../../services/auth/auth.service';

@Component({
  selector: 'app-health-workers-popup',
  templateUrl: './health-workers-popup.component.html',
  styleUrls: ['./health-workers-popup.component.scss']
})
export class HealthWorkersPopupComponent implements OnInit {

  public _elementForm: FormGroup;
  type: string;
  item: any;
  birthDate: any;
  startDate: any;
  role: any;
  roles = ['Doctor', 'Nurse', 'Administrative'];
  sexes = ['M', 'F'];
  sex: any;
  isDoctor = false;
  title = 'health worker';

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<HealthWorkersPopupComponent>,
              public authService: AuthService,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;
    this.startDate = '';
    this.birthDate = '';

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        ID: [this.item.id],
        wName: [this.item.wName, [Validators.required]],
        wLast: [this.item.wLast, [Validators.required]],
        wId: [this.item.wId, [Validators.required]],
        wPhone: [this.item.wPhone, [Validators.required]],
        wAddress: [this.item.wAddress, [Validators.required]],
        wRole: [this.item.wRole, [Validators.required]],
        wSex: [this.item.wSex, [Validators.required]],
        wEmail: [this.item.wEmail, [Validators.required]],
        wPassword: [this.item.wPassword, [Validators.required]],
        startDate: [this.item.selectedDate2, [Validators.required]],
        birthDate: [this.item.selectedDate, [Validators.required]],
      });
      (document.getElementById('w3') as HTMLInputElement).disabled = true;
      (document.getElementById('w7') as HTMLInputElement).disabled = true;
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        wName: ['', [Validators.required]],
        wLast: ['', [Validators.required]],
        wId: ['', [Validators.required]],
        wPhone: ['', [Validators.required]],
        wAddress: ['', [Validators.required]],
        wRole: ['', [Validators.required]],
        wSex: ['', [Validators.required]],
        wEmail: ['', [Validators.required]],
        wPassword: ['', [Validators.required]],
        startDate: ['', [Validators.required]],
        birthDate: ['', [Validators.required]],
      });
    }

    // If doctor is watching, disable stuff that he/she can't change
    if (this.data.isDoctor) {
      this.isDoctor = true;
      this.title = 'doctor profile';
    }
  }

  selectedRole(event){
    this.role = event.value;
  }

  selectedSex(event){
    this.sex = event.value;
  }

  /**
   * Select the start date
   * @param dateObject selected date
   */
  updateDOB(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.birthDate = stringified.substring(1, 11);
  }

  /**
   * Select the final date
   * @param dateObject selected date
   */
  updateDOB2(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.startDate = stringified.substring(1, 11);
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('w1') as HTMLInputElement).value = '';
    (document.getElementById('w2') as HTMLInputElement).value = '';
    (document.getElementById('w3') as HTMLInputElement).value = '';
    (document.getElementById('w4') as HTMLInputElement).value = '';
    (document.getElementById('w5') as HTMLInputElement).value = '';
    (document.getElementById('w6') as HTMLInputElement).value = '';

  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const wName = (document.getElementById('w1') as HTMLInputElement).value;
    const wLast = (document.getElementById('w2') as HTMLInputElement).value;
    const wSsn = (document.getElementById('w3') as HTMLInputElement).value;
    const wPhone = (document.getElementById('w4') as HTMLInputElement).value;
    const wAddress = (document.getElementById('w5') as HTMLInputElement).value;
    const wEmail = (document.getElementById('w6') as HTMLInputElement).value;
    const wPassword = (document.getElementById('w7') as HTMLInputElement).value;

    // tslint:disable-next-line:max-line-length
    if (wName !== '' && wLast !== '' && wPhone !== '' && wAddress !== '' && wEmail !== '' && this.birthDate !== '' && this.startDate !== '' && this.role !== '' && this.sex !== ''){
      if (this.type === 'add' && wSsn !== '' && wPassword !== '') {
        axios.post(environment.secondWaveURL + 'HealthWorker', {
          Ssn: wSsn,
          Fname: wName,
          Lname: wLast,
          Phone: wPhone,
          Birthdate: this.birthDate,
          Role: this.role,
          HospitalId: localStorage.getItem('hospitalId'),
          Sex: this.sex,
          Email: wEmail,
          Address: wAddress,
          Startdate: this.startDate
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            window.location.reload();
            if (this.role === 'Doctor'){
              this.authService.SignUp(wEmail, wPassword, 'Doctor');
            }
            this.fireSuccesAlert();
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      } else {
        // Send selected item number to update in database
        axios.put(environment.secondWaveURL + 'HealthWorker/' + localStorage.getItem('healthWorkerId'), {
          Ssn: localStorage.getItem('healthWorkerId'),
          Fname: wName,
          Lname: wLast,
          Phone: wPhone,
          Birthdate: this.birthDate,
          Role: this.role,
          HospitalId: localStorage.getItem('hospitalId'),
          Sex: this.sex,
          Email: wEmail,
          Address: wAddress,
          Startdate: this.startDate
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            window.location.reload();
            this.fireSuccesAlert();
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      }
    }
  }
  fireSuccesAlert(){
    Swal.fire({
      position: 'center',
      icon: 'success',
      title: 'Everything done in here!',
      showConfirmButton: false,
      timer: 2000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }
  fireErrorAlert() {
    // Fire alert
    Swal.fire({
      position: 'center',
      icon: 'error',
      title: 'Having problems right now, try again please',
      showConfirmButton: false,
      timer: 2000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }
}






