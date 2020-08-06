import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';
import Swal from "sweetalert2";

@Component({
  selector: 'app-lounges-popup',
  templateUrl: './lounges-popup.component.html',
  styleUrls: ['./lounges-popup.component.scss']
})
export class LoungesPopupComponent implements OnInit {

  public _elementForm: FormGroup;
  type: string;
  item: any;
  selectedCategory: any;
  categories = [
    'men',
    'women',
    'children',
  ];

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<LoungesPopupComponent>,
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
        lNumber: [this.item.lNumber, [Validators.required]],
        lName: [this.item.lName, [Validators.required]],
        lCapacity: [this.item.lCapacity, [Validators.required]],
        selectedCategory: [this.item.selectedCategory, [Validators.required]],
        lFloor: [this.item.lFloor, [Validators.required]]
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        lNumber: ['', [Validators.required]],
        lName: ['', [Validators.required]],
        lCapacity: ['', [Validators.required]],
        selectedCategory: ['', [Validators.required]],
        lFloor: ['', [Validators.required]]
      });
    }
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
   * Updates changes in server depending on popup type
   */
  submit() {
    const lNumber = (document.getElementById('l1') as HTMLInputElement).value;
    const lName = (document.getElementById('l2') as HTMLInputElement).value;
    const lCapacity = (document.getElementById('l3') as HTMLInputElement).value;
    const lCategory = this.selectedCategory;
    const lFloor = (document.getElementById('l3') as HTMLInputElement).value;
    console.log(this.selectedCategory);
    /*

    if (lNumber !== '' && lName !== ''&& lCapacity !== '' && lFloor !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'Lounges', {
          Number: lNumber,
          Floor: lFloor,
          Name: lName,
          Type: 1,
          HospitalId: -1,
          BedCapacity: lCapacity
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
      } else {
        // Send selected item number to update in database
        axios.put(environment.serverURL + 'Lounges/' + localStorage.getItem('loungesId'), {
          id: -1,
          number: lNumber,
          name: lName,
          capacity: lCapacity,
          category: lCategory
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
    }*/
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
}
