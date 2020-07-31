import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';

@Component({
  selector: 'app-medications-popup',
  templateUrl: './medications-popup.component.html',
  styleUrls: ['./medications-popup.component.scss']
})
export class MedicationsPopupComponent implements OnInit {

  public _elementForm: FormGroup;
  type: string;
  item: any;
  medications: string[];
  medication: '';

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<MedicationsPopupComponent>,
              private networkService: NetworkService,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        f_medication: [this.item.name]
      });
    } else {
      this._elementForm = this._formBuilder.group({
        f_medication: ['']
      });
    }

    this.getMedications();
  }

  /**
   * Closes dialog and forces refresh on parent table data
   */
  closeDialogRefresh() {
    this.dialogRef.close({event: 'refresh'});
  }

  /**
   * Get medications from the database
   */
  getMedications() {
    axios.get(environment.serverURL + 'Medication/Names', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.medications = response.data;
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  /**
   * Set medication
   * @param event selected medication
   */
  selectedMedication(event) {
    this.medication = event.value;
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    if (this.medication !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'PatientMedication', {
          name: this.medication,
          pharmacy: '',
          patientSsn: localStorage.getItem('patientSsn')
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.closeDialogRefresh();
          })
          .catch(error => {
            console.log(error.response);
          });
      } else {
        axios.put(environment.serverURL + 'PatientMedication/' + localStorage.getItem('patientSsn') + '/' + localStorage.getItem('medicationName'), {
          name: this.medication,
          pharmacy: '',
          patientSsn: -1
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.closeDialogRefresh();
          })
          .catch(error => {
            console.log(error.response);
          });
      }
    }
  }

}
