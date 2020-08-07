import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-equipment-popup',
  templateUrl: './equipment-popup.component.html',
  styleUrls: ['./equipment-popup.component.scss']
})
export class EquipmentPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  equipment: any;
  equipments = ['Surgical Lights', 'Ultrasound', 'Sterilizers', 'Defibrillators', 'Monitors', 'Art. Breathers', 'Cardiograph'];
  nameSelection = false;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<EquipmentPopupComponent>,
              private networkService: NetworkService,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;
    (document.getElementById('e1') as HTMLInputElement).disabled = true;

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        ID: [this.item.id],
        eNewName: [this.item.eNewName, [Validators.required]],
        ePredName: [this.item.ePredName, [Validators.required]],
        eProvider: [this.item.eProvider, [Validators.required]],
        eAmount: [this.item.eAmount, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        eNewName: ['', [Validators.required]],
        ePredName: ['', [Validators.required]],
        eProvider: ['', [Validators.required]],
        eAmount: ['', [Validators.required]],
      });
    }
  }

  selectedEquipment(event){
    this.equipment = event.value;
  }

  selectedName(){
    (document.getElementById('e1') as HTMLInputElement).disabled = this.nameSelection;
    this.nameSelection = !this.nameSelection;
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('e1') as HTMLInputElement).value = '';
    (document.getElementById('e2') as HTMLInputElement).value = '';
    (document.getElementById('e3') as HTMLInputElement).value = '';
    this.fireSuccessAlert()
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    let eName;
    const eProvider = (document.getElementById('e2') as HTMLInputElement).value;
    const eQuantity = (document.getElementById('e3') as HTMLInputElement).value;

    if (this.nameSelection === true){
      eName = (document.getElementById('e1') as HTMLInputElement).value;
    }
    else {
      eName = this.equipment;
    }

    if (eName !== '' && eProvider !== ''&& eQuantity !== ''){
      if (this.type === 'add') {
        axios.post(environment.secondWaveURL + 'Equipment', {
          Name: eName,
          Provider: eProvider,
          Quantity: eQuantity
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            window.location.reload();
            this.fireSuccessAlert()
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert()
          });
      } else {
        // Send selected item number to update in database
        axios.put(environment.secondWaveURL + 'Equipment/' + localStorage.getItem('equipmentId'), {
          Id: localStorage.getItem('equipmentId'),
          Name: eName,
          Provider: eProvider,
          Quantity: eQuantity
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            window.location.reload();
            this.fireSuccessAlert()
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert()
          });
      }
    }
  }

  /**
   * Fire sweet alert to indicate success
   */
  fireSuccessAlert(){
    Swal.fire({
      position: 'center',
      icon: 'success',
      title: 'Operation done.You are awesome',
      showConfirmButton: false,
      timer: 1000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * Fire sweet alert to indicate error
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
}
