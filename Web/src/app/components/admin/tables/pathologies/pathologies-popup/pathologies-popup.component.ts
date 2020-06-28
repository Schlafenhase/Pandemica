import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {NetworkService} from '../../../../../services/network/network.service';

@Component({
  selector: 'app-pathologies-popup',
  templateUrl: './pathologies-popup.component.html',
  styleUrls: ['./pathologies-popup.component.scss']
})
export class PathologiesPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<PathologiesPopupComponent>,
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
        pDescription: [this.item.pDescription, [Validators.required]],
        pSymptoms: [this.item.pSymptoms, [Validators.required]],
        pTreatments: [this.item.pTreatments, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        pName: ['', [Validators.required]],
        pDescription: ['', [Validators.required]],
        pSymptoms: ['', [Validators.required]],
        pTreatments: ['', [Validators.required]],
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
    (document.getElementById('p3') as HTMLInputElement).value = '';
    (document.getElementById('p4') as HTMLInputElement).value = '';
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    let url: string;
    let dataToSend: any;

    if (this.type === 'add') {
      // ID number is empty, it isn't assigned yet by database
      dataToSend = {
        idNumber: '',
        pName: this.data.pName,
        pDescription: this.data.id.pDescription,
        pSymptoms: this.data.pSymptoms,
        pTreatments: this.data.pTreatments
      }

      url = '' // INSERT ADD URL
    } else {
      // Send selected item number to update in database
      dataToSend = {
        idNumber: this.item.id,
        pName: this.data.pName,
        pDescription: this.data.id.pDescription,
        pSymptoms: this.data.pSymptoms,
        pTreatments: this.data.pTreatments
      }

      url = '' // INSERT EDIT URL
    }

    // Send data to server
    // this.networkService.post(url, dataToSend)

    // Close popup window
    window.location.reload();
  }
}
