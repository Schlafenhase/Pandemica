import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  constructor(private http: HttpClient) { }

  public GetReport(report: string) {
    const responseType = 'arraybuffer';
    const url = environment.serverURL + 'reports/' +  report;
    return this.http.get(url, { responseType } )
  }
}
