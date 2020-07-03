import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';

@Component({
  selector: 'app-contacts-popup',
  templateUrl: './contacts-popup.component.html',
  styleUrls: ['./contacts-popup.component.scss']
})
export class ContactsPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  sexes: string[] = ['M', 'F'];
  sex: '';
  birthDate: string;
  contactDate: string;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<ContactsPopupComponent>,
              private networkService: NetworkService,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;
    this.birthDate = '';
    this.contactDate = '';

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        ID: [this.item.id],
        cName: [this.item.cName, [Validators.required]],
        cLastName: [this.item.cLastName, [Validators.required]],
        cAge: [this.item.cAge, [Validators.required]],
        cNationality: [this.item.cNationality, [Validators.required]],
        cAdress: [this.item.cAdress, [Validators.required]],
        cPathologies: [this.item.cPathology, [Validators.required]],
        cEmail: [this.item.cEmail, [Validators.required]],
      });
      (document.getElementById('cp3') as HTMLInputElement).disabled = true;
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        cName: ['', [Validators.required]],
        cLastName: ['', [Validators.required]],
        cAge: ['', [Validators.required]],
        cNationality: ['', [Validators.required]],
        cAdress: ['', [Validators.required]],
        cPathologies: ['', [Validators.required]],
        cEmail: ['', [Validators.required]],
      });
      (document.getElementById('cp3') as HTMLInputElement).disabled = false;
    }
  }

  /**
   * Set selected sex
   * @param event selected sex
   */
  selectedSex(event) {
    this.sex = event.value;
  }

  /**
   * Set contact date
   * @param dateObject selected date
   */
  updateDOB1(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.contactDate = stringified.substring(1, 11);
  }

  /**
   * Set birth date
   * @param dateObject selected date
   */
  updateDOB2(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.birthDate = stringified.substring(1, 11);
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('cp1') as HTMLInputElement).value = '';
    (document.getElementById('cp2') as HTMLInputElement).value = '';
    (document.getElementById('cp3') as HTMLInputElement).value = '';
    (document.getElementById('cp4') as HTMLInputElement).value = '';
    (document.getElementById('cp5') as HTMLInputElement).value = '';
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const cFirstName = (document.getElementById('cp1') as HTMLInputElement).value;
    const cLastName = (document.getElementById('cp2') as HTMLInputElement).value;
    const cSsn = (document.getElementById('cp3') as HTMLInputElement).value;
    const cEmail = (document.getElementById('cp4') as HTMLInputElement).value;
    const cAddress = (document.getElementById('cp5') as HTMLInputElement).value;

    // tslint:disable-next-line:max-line-length
    if (cFirstName !== '' && cLastName !== '' && cEmail !== '' && cAddress !== '' && this.sex !== '' && this.contactDate !== '' && this.birthDate !== ''){
      if (this.type === 'add') {
        if (cSsn !== ''){
          axios.post(environment.serverURL + 'Person', {
            ssn: cSsn,
            firstName: cFirstName,
            lastName: cLastName,
            birthDate: this.birthDate,
            eMail: cEmail,
            address: cAddress,
            sex: this.sex,
            contactDate: this.contactDate,
            patientSsn: localStorage.getItem('patientSsn')
          }, {
            headers: {
              'Content-Type': 'application/json; charset=UTF-8'
            }
          })
            .then(response => {
              console.log(response);
              window.location.reload();
            })
            .catch(error => {
              console.log(error.response);
            });
        }
      } else {
        axios.put(environment.serverURL + 'Person/' + localStorage.getItem('personSsn'), {
          ssn: -1,
          firstName: cFirstName,
          lastName: cLastName,
          birthDate: this.birthDate,
          eMail: cEmail,
          address: cAddress,
          sex: this.sex,
          contactDate: this.contactDate,
          patientSsn: localStorage.getItem('patientSsn')
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            window.location.reload();
          })
          .catch(error => {
            console.log(error.response);
          });
      }
    }
  }
}

