import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MapShapeService {

  constructor(private http: HttpClient) { }

  getCountriesShapes(): Observable<any> {
    const url = 'assets/data/worldmap.json';
    return this.http.get(url);
  }
}
