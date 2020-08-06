import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';
import Swal from "sweetalert2";

@Component({
  selector: 'app-beds-popup',
  templateUrl: './beds-popup.component.html',
  styleUrls: ['./beds-popup.component.scss']
})
export class BedsPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  selectedCategory:any;
  categories = [
    'Yes',
    'No',
  ];

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<BedsPopupComponent>,
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
        ID: [this.item.id],
        bNumber: [this.item.bNumber, [Validators.required]],
        bEquipment: [this.item.bEquipment, [Validators.required]],
        bLounge: [this.item.bLounge, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        bNumber: ['', [Validators.required]],
        bEquipment: ['', [Validators.required]],
        bLounge: ['', [Validators.required]],
      });
    }
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('b1') as HTMLInputElement).value = '';
    (document.getElementById('b2') as HTMLInputElement).value = '';
    (document.getElementById('b3') as HTMLInputElement).value = '';
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const bNumber = (document.getElementById('b1') as HTMLInputElement).value;
    const bEquipment = (document.getElementById('b2') as HTMLInputElement).value;
    const bLounge = (document.getElementById('b3') as HTMLInputElement).value;

    if (bNumber !== '' && bEquipment !== ''&& bLounge !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'Beds', {
          id: -1,
          number: bNumber,
          equipment: bEquipment,
          lounge: bLounge
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            window.location.reload();
            this.fireSuccesAlert()
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      } else {
        // Send selected item number to update in database
        axios.put(environment.serverURL + 'Beds/' + localStorage.getItem('bedsId'), {
          id: -1,
          number: bNumber,
          equipment: bEquipment,
          lounge: bLounge
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            window.location.reload();
            this.fireSuccesAlert()
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
      title: 'Everything went smoothly',
      showConfirmButton: false,
      timer: 1000,
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
      title: 'error',
      showConfirmButton: false,
      timer: 1000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }
}
