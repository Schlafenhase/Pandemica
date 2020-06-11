import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {CountryService} from '../../services/country.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  // Properties
  country = 'Costa Rica';
  lastUpdate = new Date(2020, 4, 7, 10, 25);
  confirmed = 42612;
  active = 35697;
  dead = 696;
  recovered = 6219;
  nationals = 38512;
  foreign = 4100;
  newCases = 23;
  newRecovered = 8;
  newDeceased = 15;

  constructor(private countryService: CountryService) { }

  ngOnInit(): void {
    this.getCountryMap();
  }

  // Updates the country base on the map selection
  private getCountryMap() {
    this.countryService.country$().subscribe(
      data => {
        this.country = data;
      });
  }

  // Gets country data from the API
  private getCountryData() {
    this.countryService.getCountryData()
      .subscribe(
        data => {
        });
  }

  // Scrolls the user to charts section
  public onScrollDown(): void {
    // Scroll certain amounts from current position
    window.scrollBy({
      top: 850, // could be negative value
      left: 0,
      behavior: 'smooth'
    });
  }

  // When the user clicks on the button, scroll to the top of the document
  backToTop() {
    window.scroll({
      top: 0, // could be negative value
      left: 0,
      behavior: 'smooth'
    });
  }
}
