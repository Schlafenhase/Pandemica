import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';

@Component({
  selector: 'app-states-popup',
  templateUrl: './states-popup.component.html',
  styleUrls: ['./states-popup.component.scss']
})
export class StatesPopupComponent implements OnInit {

  public _elementForm: FormGroup;
  type: string;
  item: any;
  states: string[];
  state: '';
  date: string;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<StatesPopupComponent>,
              private networkService: NetworkService,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;
    this.date = '';

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
      this.getStates();
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
      this.getStates();
    }
  }

  getStates() {
    axios.get(environment.serverURL + 'State/Names', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.states = response.data;
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  selectedState(event) {
    this.state = event.value;
  }

  updateDOB(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.date = stringified.substring(1, 11);
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    if (this.state !== '' && this.date !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'PatientState', {
          name: this.state,
          date: this.date,
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
      } else {
        axios.put(environment.serverURL + 'PatientState/' + localStorage.getItem('patientSsn') + '/' + localStorage.getItem('stateName') + '/' + localStorage.getItem('stateDate'), {
          name: this.state,
          date: this.date
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
