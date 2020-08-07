import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-procedures-popup',
  templateUrl: './procedures-popup.component.html',
  styleUrls: ['./procedures-popup.component.scss']
})

export class ProceduresPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  nameSelection = false;
  procedure: any;
  procedures = ['Appendectomy', 'Breast Biopsy', 'Cataract Surg.', 'Caesarean Sect.', 'Hysterectomy', 'Lowback Surgery', 'Mastectomy', 'Tonsillectomy'];

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<ProceduresPopupComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;
    (document.getElementById('p1') as HTMLInputElement).disabled = true;

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
   * Closes the dialog on contact upgrade
   */
  closeDialogRefresh() {
    this.dialogRef.close({event: 'refresh'});
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
   * Displays error alert
   */
  fireErrorAlert() {
    // Fire alert
    Swal.fire({
      position: 'center',
      icon: 'error',
      title: 'Mmm it seems there was a problem',
      showConfirmButton: false,
      timer: 2000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * Displays success alert
   */
  fireSuccessAlert(){
    Swal.fire({
      position: 'center',
      icon: 'success',
      title: 'We got you, everything ready!',
      showConfirmButton: false,
      timer: 2000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * Manages name selection in HTML
   */
  selectedName(){
    (document.getElementById('p1') as HTMLInputElement).disabled = this.nameSelection;
    this.nameSelection = !this.nameSelection;
  }

  /**
   * Manages procedure selection in HTML
   */
  selectedProcedure(event){
    this.procedure = event.value;
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    let pName;
    const pDuration = (document.getElementById('p2') as HTMLInputElement).value;

    // Select between name and procedure list
    if (this.nameSelection === true){
      pName = (document.getElementById('p1') as HTMLInputElement).value;
    } else {
      pName = this.procedure;
    }

    if (pName !== '' && pDuration !== ''){
      if (this.type === 'add') {
        axios.post(environment.storeProceduresURL + 'Procedure', {
          HospitalId: localStorage.getItem('hospitalId'),
          Name: pName,
          Duration: pDuration
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.closeDialogRefresh();
            this.fireSuccessAlert();
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      } else {
        // Send selected item number to update in database
        axios.put(environment.storeProceduresURL + 'Procedure/' + localStorage.getItem('procedureId'), {
          Id: localStorage.getItem('procedureId'),
          Name: pName,
          Duration: pDuration
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.closeDialogRefresh();
            this.fireSuccessAlert();
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      }
    }
  }

}
