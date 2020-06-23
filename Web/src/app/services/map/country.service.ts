import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, Subject, throwError} from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  private subject = new Subject<string>();
  private url = 'localhost/country';

  constructor(private http: HttpClient) { }

  countryPush(country: string) {
    this.subject.next(country);
  }

  country$() {
    return this.subject.asObservable();
  }

  getCountryData(): Observable<any> {
    return this.http.get<any>(this.url)
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
