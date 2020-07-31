import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';
import {getDecoratorStripTransformerFactory} from "@angular/compiler-cli/src/transformers/r3_strip_decorators";

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
        pName: [this.item.name, [Validators.required]],
        pDescription: [this.item.description, [Validators.required]],
        pSymptoms: [this.item.symptoms, [Validators.required]],
        pTreatments: [this.item.treatment, [Validators.required]],
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
    const pName = (document.getElementById('p1') as HTMLInputElement).value;
    const pDescription = (document.getElementById('p2') as HTMLInputElement).value;
    const pSymptoms = (document.getElementById('p3') as HTMLInputElement).value;
    const pTreatments = (document.getElementById('p4') as HTMLInputElement).value;

    if (pName !== '' && pDescription !== '' && pSymptoms !== '' && pTreatments !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'Pathology', {
          name: pName,
          symptoms: pSymptoms,
          description: pDescription,
          treatment: pTreatments,
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
        axios.put(environment.serverURL + 'Pathology/' + localStorage.getItem('pathologyID'), {
          name: pName,
          symptoms: pSymptoms,
          description: pDescription,
          treatment: pTreatments,
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
