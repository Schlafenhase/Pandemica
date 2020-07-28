import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-medical-history-popup',
  templateUrl: './medical-history-popup.component.html',
  styleUrls: ['./medical-history-popup.component.scss']
})
export class MedicalHistoryPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;

  constructor(
    private _formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<MedicalHistoryPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        ID: [this.item.id],
        cName: [this.item.cName, [Validators.required]],
        cLastName: [this.item.cLastName, [Validators.required]],
        cAge: [this.item.cAge, [Validators.required]],
        cNationality: [this.item.cNationality, [Validators.required]],
        cAdress: [this.item.cAdress, [Validators.required]],
        cPathologies: [this.item.cPathology, [Validators.required]],
        cEmail: [this.item.cEmail, [Validators.required]],
      });
      (document.getElementById('cp3') as HTMLInputElement).disabled = true;
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        cName: ['', [Validators.required]],
        cLastName: ['', [Validators.required]],
        cAge: ['', [Validators.required]],
        cNationality: ['', [Validators.required]],
        cAdress: ['', [Validators.required]],
        cPathologies: ['', [Validators.required]],
        cEmail: ['', [Validators.required]],
      });
      (document.getElementById('cp3') as HTMLInputElement).disabled = false;
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('cp1') as HTMLInputElement).value = '';
    (document.getElementById('cp2') as HTMLInputElement).value = '';
    (document.getElementById('cp3') as HTMLInputElement).value = '';
    (document.getElementById('cp4') as HTMLInputElement).value = '';
    (document.getElementById('cp5') as HTMLInputElement).value = '';
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {

  }

}
