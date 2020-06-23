import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FilesService {

  constructor(private http: HttpClient) { }

  public SendPatients(files: File[]): Observable<any> {
    const url = environment.serverURL + 'excel';
    let header = new HttpHeaders();
    header = header.set('Accept', 'multipart/form-data');

    const formData = new FormData();
    files.forEach(file => {
      formData.append('file', file);
    });

    console.log('Uploading files to ' + url);
    return this.http.post(url, formData, {headers : header});
  }
}
