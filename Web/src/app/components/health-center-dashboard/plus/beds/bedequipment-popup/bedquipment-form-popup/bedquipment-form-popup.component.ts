import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../../../services/network/network.service';
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
  category: any;
  categories = ['Option1',  'Option2',  'Option3'];

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<BedquipmentFormPopupComponent>,
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
        bequipment1: [this.item.bequipment1, [Validators.required]],
        bequipment2: [this.item.bequipment2, [Validators.required]],
        bequipment3: [this.item.bequipment3, [Validators.required]],
      });
      (document.getElementById('l1') as HTMLInputElement).disabled = true;
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        bequipment1: ['', [Validators.required]],
        bequipment2: ['', [Validators.required]],
        bequipment3: ['', [Validators.required]],
      });
    }
  }

  selectedCategory(event){
    this.category = event.value;
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('id1') as HTMLInputElement).value = '';
    (document.getElementById('id2') as HTMLInputElement).value = '';
    (document.getElementById('id3') as HTMLInputElement).value = '';
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const bequipment1 = (document.getElementById('id1') as HTMLInputElement).value;
    const bequipment2 = (document.getElementById('id2') as HTMLInputElement).value;
    const bequipment3 = (document.getElementById('id3') as HTMLInputElement).value;
    const bequipment4 = this.category;

    if (bequipment1 !== ''&& bequipment2 !== '' && bequipment3 !== ''){
      if (this.type === 'add') {
        axios.post(environment.secondWaveURL + 'Lounge', {
          info1: bequipment1,
          info2: bequipment2,
          info3: bequipment3,
          info4: bequipment4,
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
        axios.put(environment.secondWaveURL + 'Equipment/' + localStorage.getItem('equipmentId'), {
          Number: localStorage.getItem('equipmentId'),
          info1: bequipment1,
          info2: bequipment2,
          info3: bequipment3,
          info4: bequipment4,
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
