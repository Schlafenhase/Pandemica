import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable, Subject, throwError} from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import {environment} from '../../../environments/environment';
import {IHomeView} from '../data/users';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  private subject = new Subject<string>();

  constructor(private http: HttpClient) { }

  countryPush(country: string) {
    this.subject.next(country);
  }

  country$() {
    return this.subject.asObservable();
  }

  getCountryData(countryName: string): Observable<IHomeView> {
    const url = environment.serverURL + 'StoreProcedure/Home/' + countryName;
    console.log('Downloading from ' + url);
    return this.http.get<IHomeView>(url)
      .pipe(
        retry(1),
        catchError(this.handleError));
  }

  private handleError(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
