import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../../../environments/environment';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-bedquipment-form-popup',
  templateUrl: './bedquipment-form-popup.component.html',
  styleUrls: ['./bedquipment-form-popup.component.scss']
})

export class BedquipmentFormPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  equipment: any;
  equipmentList = [];

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<BedquipmentFormPopupComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;

    this.getEquipmentList();
    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        ID: [this.item.id],
        lEquipment: [this.item.lEquipment, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        lEquipment: ['', [Validators.required]],
      });
    }
  }

  /**
   * Tells parent to refresh data
   */
  closeDialogRefresh() {
    this.dialogRef.close({event: 'refresh'});
  }

  /**
   * This method display a warning alert for any error in the project
   */
  fireErrorAlert() {
    // Fire alert
    Swal.fire({
      position: 'center',
      icon: 'error',
      title: 'something went wrong with that',
      showConfirmButton: false,
      timer: 2000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * This method display a success alert for any error in the project
   */
  fireSuccessAlert(){
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

  /**
   * Fetches data from server
   */
  getEquipmentList() {
    axios.get(environment.secondWaveURL + 'Equipment/Name', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.equipmentList = response.data;
      })
      .catch(error => {
        console.log(error.response);
        this.fireErrorAlert();
      });
  }

  /**
   * Maages selected equipment from HTML
   */
  selectedEquipment(event){
    this.equipment = event.value;
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    if (this.equipment !== ''){
      if (this.type === 'add') {
        axios.post(environment.storeProceduresURL + 'BedEquipment', {
          BedNumber: localStorage.getItem('bedNumber'),
          EquipmentName: this.equipment
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.closeDialogRefresh();
            this.fireSuccessAlert();
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      } else {
        // Send selected item number to update in database
        axios.put(environment.storeProceduresURL + 'BedEquipment/' + localStorage.getItem('bedNumber'), {
          OldEquipmentName: localStorage.getItem('equipmentName'),
          NewEquipmentName: this.equipment
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.closeDialogRefresh();
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
