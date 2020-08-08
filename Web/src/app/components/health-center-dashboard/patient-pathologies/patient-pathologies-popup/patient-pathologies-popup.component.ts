import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';
import Swal from "sweetalert2";

@Component({
  selector: 'app-patient-pathologies-popup',
  templateUrl: './patient-pathologies-popup.component.html',
  styleUrls: ['./patient-pathologies-popup.component.scss']
})
export class PatientPathologiesPopupComponent implements OnInit {

  public _elementForm: FormGroup;
  type: string;
  item: any;
  pathologies: string[];
  pathology: '';

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<PatientPathologiesPopupComponent>,
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
        f_pathology: [this.item.name]
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        f_pathology: ['']
      });
    }
    this.getPathologies();
  }

  /**
   * Closes dialog and forces refresh on parent table data
   */
  closeDialogRefresh() {
    this.dialogRef.close({event: 'refresh'});
  }

  /**
   * Fire error alert
   */
  fireErrorAlert() {
    // Fire alert
    Swal.fire({
      position: 'center',
      icon: 'error',
      title: 'error',
      showConfirmButton: false,
      timer: 1000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * Fire success alert
   */
  fireSuccessAlert(){
    Swal.fire({
      position: 'center',
      icon: 'success',
      title: 'Everything went smoothly',
      showConfirmButton: false,
      timer: 1000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * Get pathologies from the dataabase
   */
  getPathologies() {
    axios.get(environment.serverURL + 'Pathology/Names', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.pathologies = response.data;
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  /**
   * Set pathology
   * @param event selected pathology
   */
  selectedPathology(event) {
    this.pathology = event.value;
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    if (this.pathology !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'PatientPathologies', {
          name: this.pathology,
          symptoms: '',
          description: '',
          treatment: '',
          patientSsn: localStorage.getItem('patientSsn')
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.closeDialogRefresh()
            this.fireSuccessAlert();
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      } else {
        axios.put(environment.serverURL + 'PatientPathologies/' + localStorage.getItem('patientSsn') + '/' + localStorage.getItem('pathologyName'), {
          name: this.pathology,
          symptoms: '',
          description: '',
          treatment: '',
          patientSsn: -1
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.closeDialogRefresh()
            this.fireSuccessAlert();
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      }
    }
  }

}
