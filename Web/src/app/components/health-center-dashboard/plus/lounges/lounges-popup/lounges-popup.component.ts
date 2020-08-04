import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';

@Component({
  selector: 'app-lounges-popup',
  templateUrl: './lounges-popup.component.html',
  styleUrls: ['./lounges-popup.component.scss']
})
export class LoungesPopupComponent implements OnInit {

  public _elementForm: FormGroup;
  type: string;
  item: any;
  selectedCategory:any;
  categories = [
    'Men',
    'Women',
    'Child',
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
        selectedCCategory: [this.item.selectedCCategory, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        lNumber: ['', [Validators.required]],
        lName: ['', [Validators.required]],
        lCapacity: ['', [Validators.required]],
        selectedCCategory: ['', [Validators.required]],
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
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const lNumber = (document.getElementById('l1') as HTMLInputElement).value;
    const lName = (document.getElementById('l2') as HTMLInputElement).value;
    const lCapacity = (document.getElementById('l3') as HTMLInputElement).value;

    if (lNumber !== '' && lName !== ''&& lCapacity !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'Lounges', {
          id: -1,
          number: lNumber,
          name: lName,
          capacity: lCapacity
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
        // Send selected item number to update in database
        axios.put(environment.serverURL + 'Lounges/' + localStorage.getItem('loungesId'), {
          id: -1,
          number: lNumber,
          name: lName,
          capacity: lCapacity
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
