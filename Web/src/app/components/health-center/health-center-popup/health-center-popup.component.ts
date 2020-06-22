import {Component, Inject, OnInit} from '@angular/core';
import {NetworkService} from '../../../services/network/network.service';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-health-center-popup',
  templateUrl: './health-center-popup.component.html',
  styleUrls: ['./health-center-popup.component.scss']
})
export class HealthCenterPopupComponent implements OnInit {
  yesOrNo: string[] = ['Yes', 'No',]
  public _elementForm: FormGroup;
  type: string;
  item: any;
  optionSelected: any;
  optionSelected2: any;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<HealthCenterPopupComponent>,
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
        pLast: [this.item.pLast, [Validators.required]],
        pAge: [this.item.pAge, [Validators.required]],
        pNationality: [this.item.pNationality, [Validators.required]],
        pRegion: [this.item.pRegion, [Validators.required]],
        pPathology: [this.item.pPathology, [Validators.required]],
        pState: [this.item.pState, [Validators.required]],
        pMedication: [this.item.pMedication, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        pName: ['', [Validators.required]],
        pLast: ['', [Validators.required]],
        pAge: ['', [Validators.required]],
        pNationality: ['', [Validators.required]],
        pRegion: ['', [Validators.required]],
        pPathology: ['', [Validators.required]],
        pState: ['', [Validators.required]],
        pMedication: ['', [Validators.required]],
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
    (document.getElementById('p5') as HTMLInputElement).value = '';
    (document.getElementById('p6') as HTMLInputElement).value = '';
    (document.getElementById('p7') as HTMLInputElement).value = '';
    (document.getElementById('p8') as HTMLInputElement).value = '';
  }
  selected(event) {
    console.log(event.value);
    this.optionSelected = event.value;
  }
  selected2(event) {
    console.log(event.value);
    this.optionSelected2 = event.value;
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
        pLast: this.data.pLast,
        pAge: this.data.pAge,
        pNationality:this.data.pNationality,
        pRegion: this.data.pRegion,
        pPathology:this.data.pPathology,
        pState: this.data.pState,
        pMedication: this.data.pMedication,
        optionSelected: this.data.optionSelected,
        optionSelected2: this.data.optionSelected2,
      }

      url = '' // INSERT ADD URL
    } else {
      // Send selected item number to update in database
      dataToSend = {
        idNumber: this.item.id,
        pName: this.data.pName,
        pLast: this.data.pLast,
        pAge: this.data.pAge,
        pNationality:this.data.pNationality,
        pRegion: this.data.pRegion,
        pPathology:this.data.pPathology,
        pState: this.data.pState,
        pMedication: this.data.pMedication,
        optionSelected: this.data.optionSelected,
        optionSelected2: this.data.optionSelected2,
      }

      url = '' // INSERT EDIT URL
    }

    // Send data to server
    // this.networkService.post(url, dataToSend)

    // Close popup window
    window.location.reload();
  }
}


