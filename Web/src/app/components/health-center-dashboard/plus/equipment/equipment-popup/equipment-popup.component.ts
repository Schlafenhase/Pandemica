import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';

@Component({
  selector: 'app-equipment-popup',
  templateUrl: './equipment-popup.component.html',
  styleUrls: ['./equipment-popup.component.scss']
})
export class EquipmentPopupComponent implements OnInit {

  public _elementForm: FormGroup;
  type: string;
  item: any;

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

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        ID: [this.item.id],
        eName: [this.item.eName, [Validators.required]],
        eProvider: [this.item.eProvider, [Validators.required]],
        eAmount: [this.item.eAmount, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        eName: ['', [Validators.required]],
        eProvider: ['', [Validators.required]],
        eAmount: ['', [Validators.required]],
      });
    }
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('e1') as HTMLInputElement).value = '';
    (document.getElementById('e2') as HTMLInputElement).value = '';
    (document.getElementById('e3') as HTMLInputElement).value = '';
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const eName = (document.getElementById('e1') as HTMLInputElement).value;
    const eProvider = (document.getElementById('e2') as HTMLInputElement).value;
    const eAmount = (document.getElementById('e3') as HTMLInputElement).value;

    if (eName !== '' && eProvider !== ''&& eAmount !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'Equipment', {
          id: -1,
          name: eName,
          provider: eProvider,
          amount: eAmount
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
        axios.put(environment.serverURL + 'Equipments/' + localStorage.getItem('equipmentId'), {
          id: -1,
          name: eName,
          provider: eProvider,
          amount: eAmount
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
