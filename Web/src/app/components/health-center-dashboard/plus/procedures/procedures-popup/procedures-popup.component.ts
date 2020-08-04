import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';

@Component({
  selector: 'app-procedures-popup',
  templateUrl: './procedures-popup.component.html',
  styleUrls: ['./procedures-popup.component.scss']
})
export class ProceduresPopupComponent implements OnInit {

  public _elementForm: FormGroup;
  type: string;
  item: any;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<ProceduresPopupComponent>,
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
        pName: [this.item.pName, [Validators.required]],
        pDays: [this.item.pDays, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        pName: ['', [Validators.required]],
        pDays: ['', [Validators.required]],
      });
    }
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('p1') as HTMLInputElement).value = '';
    (document.getElementById('p2') as HTMLInputElement).value = '';
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const pName = (document.getElementById('p1') as HTMLInputElement).value;
    const pDays = (document.getElementById('p2') as HTMLInputElement).value;

    if (pName !== '' && pDays !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'Procedure', {
          id: -1,
          name: pName,
          days: pDays
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
        axios.put(environment.serverURL + 'Procedure/' + localStorage.getItem('procedureId'), {
          id: -1,
          name: pName,
          days: pDays
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
