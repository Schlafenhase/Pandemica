import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-lounges-popup',
  templateUrl: './lounges-popup.component.html',
  styleUrls: ['./lounges-popup.component.scss']
})

export class LoungesPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  category: any;
  categories = ['Men',  'Women',  'Children'];

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<LoungesPopupComponent>,
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
        lNumber: [this.item.lNumber, [Validators.required]],
        lName: [this.item.lName, [Validators.required]],
        lCapacity: [this.item.lCapacity, [Validators.required]],
        lCategory: [this.item.lCategory, [Validators.required]],
        lFloor: [this.item.lFloor, [Validators.required]]
      });
      (document.getElementById('l1') as HTMLInputElement).disabled = true;
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        lNumber: ['', [Validators.required]],
        lName: ['', [Validators.required]],
        lCapacity: ['', [Validators.required]],
        lCategory: ['', [Validators.required]],
        lFloor: ['', [Validators.required]]
      });
    }
  }

  /**
   * Closes the dialog on contact upgrade
   */
  closeDialogRefresh() {
    this.dialogRef.close({event: 'refresh'});
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('l1') as HTMLInputElement).value = '';
    (document.getElementById('l2') as HTMLInputElement).value = '';
    (document.getElementById('l3') as HTMLInputElement).value = '';
    (document.getElementById('l4') as HTMLInputElement).value = '';
  }

  /**
   * Displays an error alert
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
   * Displays a success alert
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
   * Manages category selection in HTML
   */
  selectedCategory(event){
    this.category = event.value;
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const lNumber = (document.getElementById('l1') as HTMLInputElement).value;
    const lName = (document.getElementById('l2') as HTMLInputElement).value;
    const lCapacity = (document.getElementById('l3') as HTMLInputElement).value;
    const lCategory = this.category;
    const lFloor = (document.getElementById('l4') as HTMLInputElement).value;

    if (lName !== ''&& lCapacity !== '' && lFloor !== '' && lCategory !== ''){
      if (this.type === 'add' && lNumber !== '') {
        axios.post(environment.secondWaveURL + 'Lounge', {
          Number: lNumber,
          Floor: lFloor,
          Name: lName,
          Type: lCategory,
          HospitalId: localStorage.getItem('hospitalId'),
          BedCapacity: lCapacity
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
        axios.put(environment.secondWaveURL + 'Lounge/' + localStorage.getItem('loungesId'), {
          Number: localStorage.getItem('loungesId'),
          Floor: lFloor,
          Name: lName,
          Type: lCategory,
          HospitalId: localStorage.getItem('hospitalId'),
          BedCapacity: lCapacity
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
