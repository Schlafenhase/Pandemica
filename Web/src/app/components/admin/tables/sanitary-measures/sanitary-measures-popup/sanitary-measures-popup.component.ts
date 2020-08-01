import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from "axios";
import {environment} from "../../../../../../environments/environment";

@Component({
  selector: 'app-sanitary-measures-popup',
  templateUrl: './sanitary-measures-popup.component.html',
  styleUrls: ['./sanitary-measures-popup.component.scss']
})
export class SanitaryMeasuresPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<SanitaryMeasuresPopupComponent>,
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
        sName: [this.item.name, [Validators.required]],
        sDescription: [this.item.description, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        sName: ['', [Validators.required]],
        sDescription: ['', [Validators.required]],
      });
    }
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('sm1') as HTMLInputElement).value = '';
    (document.getElementById('sm2') as HTMLInputElement).value = '';
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const smName = (document.getElementById('sm1') as HTMLInputElement).value;
    const smDescription = (document.getElementById('sm2') as HTMLInputElement).value;

    if (smName !== '' && smDescription !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'SanitaryMeasurements', {
          id: -1,
          name: smName,
          description: smDescription
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
        axios.put(environment.serverURL + 'SanitaryMeasurements/' + localStorage.getItem('sanitaryMeasurementId'), {
          id: -1,
          name: smName,
          description: smDescription
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
