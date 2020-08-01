import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';

@Component({
  selector: 'app-regions-popup',
  templateUrl: './regions-popup.component.html',
  styleUrls: ['./regions-popup.component.scss']
})
export class RegionsPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<RegionsPopupComponent>,
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
        Country: [this.item.country, [Validators.required]],
        Region: [this.item.name, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        Country: ['', [Validators.required]],
        Region: ['', [Validators.required]],
      });
    }
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('r1') as HTMLInputElement).value = '';
    (document.getElementById('r2') as HTMLInputElement).value = '';
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const rCountry = (document.getElementById('r1') as HTMLInputElement).value;
    const rName = (document.getElementById('r2') as HTMLInputElement).value;

    if (rCountry !== '' && rName !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'ProvinceStateRegion', {
          name: rName,
          country: rCountry,
          id: -1
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
        axios.put(environment.serverURL + 'ProvinceStateRegion/' + localStorage.getItem('regionId'), {
          name: rName,
          country: rCountry,
          id: -1
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
