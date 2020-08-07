import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-beds-popup',
  templateUrl: './beds-popup.component.html',
  styleUrls: ['./beds-popup.component.scss']
})
export class BedsPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  lounge:any;
  lounges = [];

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<BedsPopupComponent>,
              private networkService: NetworkService,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;
    this.getLounges();

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      (document.getElementById('b1') as HTMLInputElement).disabled = true;
      this._elementForm = this._formBuilder.group({
        ID: [this.item.id],
        bNumber: [this.item.bNumber, [Validators.required]],
        bIcu: [this.item.bIcu, [Validators.required]],
        bLounges: [this.item.bLounges, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        bNumber: ['', [Validators.required]],
        bIcu: ['', [Validators.required]],
        bLounges: ['', [Validators.required]],
      });
    }
  }

  getLounges() {
    axios.get(environment.secondWaveURL + 'Lounge/Number/' + localStorage.getItem('hospitalId'), {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.lounges = response.data;
      })
      .catch(error => {
        console.log(error.response);
        this.fireErrorAlert();
      });
  }

  selectedLounge(event){
    this.lounge = event.value;
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('b1') as HTMLInputElement).value = '';
    (document.getElementById('b2') as HTMLInputElement).value = '';
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const bNumber = (document.getElementById('b1') as HTMLInputElement).value;
    const bIcu = (document.getElementById('b2') as HTMLInputElement).checked;

    if (this.lounge !== ''){
      if (this.type === 'add' && bNumber !== '') {
        axios.post(environment.secondWaveURL + 'Bed', {
          Number: bNumber,
          Icu: bIcu,
          LoungeNumber: this.lounge
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            window.location.reload();
            this.fireSuccesAlert()
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      } else {
        // Send selected item number to update in database
        axios.put(environment.secondWaveURL + 'Bed/' + localStorage.getItem('bedId'), {
          Number: localStorage.getItem('bedId'),
          Icu: bIcu,
          LoungeNumber: this.lounge
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            window.location.reload();
            this.fireSuccesAlert()
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
      title: 'Everything went smoothly',
      showConfirmButton: false,
      timer: 1000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }
  fireErrorAlert() {
    // Fire alert
    Swal.fire({
      position: 'center',
      icon: 'error',
      title: 'error',
      showConfirmButton: false,
      timer: 1000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }
}
