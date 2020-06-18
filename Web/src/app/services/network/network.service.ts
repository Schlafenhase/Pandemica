import { Injectable } from '@angular/core';
import axios from 'axios'
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NetworkService {

  constructor() { }

  /**
   * Posts array like object formatted in JSON to API url
   * @param url string
   * @param data array like object
   */
  post(url: string, data) {
    axios.post(environment.serverURL + url, data, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        return response.data.json();
      })
      .catch(error => {
        console.log(error.response);
      });
  }
}
