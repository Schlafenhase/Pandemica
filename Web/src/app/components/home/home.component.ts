import {Component, ElementRef, HostListener, OnInit, ViewChild} from '@angular/core';
import {CountryService} from '../../services/map/country.service';
import {ChartsService} from '../../services/charts/charts.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public currentWindowWidth: number;

  // Properties
  country = 'Costa Rica';
  lastUpdate = new Date(2020, 4, 7, 10, 25);
  confirmed = 0;
  active = 0;
  dead = 0;
  recovered = 0;
  nationals = 0;
  foreign = 0;
  newCases = 0;
  newRecovered = 0;
  newDeceased = 0;
  measurements = [];
  regions = [];

  constructor(private countryService: CountryService,
              private chartsService: ChartsService) { }

  ngOnInit(): void {
    this.getCountryMap();
    this.getCountryData();
    this.currentWindowWidth = window.innerWidth;
  }

  // Updates the country base on the map selection
  private getCountryMap() {
    this.countryService.country$().subscribe(
      data => {
        this.country = data;
        this.getCountryData();
      });
  }

  // Gets country data from the API
  private getCountryData() {
    this.countryService.getCountryData(this.country)
      .subscribe(
        data => {
          console.log(data);
          this.confirmed = data.confirmedCases;
          this.active = data.activeCases;
          this.dead = data.deadths;
          this.recovered = data.recovered;
          this.nationals = data.nationals;
          this.foreign = data.foreigns;
          this.newCases = data.todayNewCases;
          this.newRecovered = data.todayRecovered;
          this.newDeceased = data.todayDeceased;
          this.measurements = data.measurements;
          this.regions = data.regions;
          this.chartsService.pushChartsData(data);
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

  parseRegion(region) {
    if (!region) return;
    return [
      {
        name: 'confirmed',
        value: region.confirmed
      },
      {
        name: 'active',
        value: region.active
      },
      {
        name: 'recovered',
        value: region.recovered
      },
      {
        name: 'dead',
        value: region.dead
      }
    ];
  }

  trackBy(index, item) {
    if (!item) return null;
    return index;
  }

  /**
   * Listen for real time window resizing
   */
  @HostListener('window:resize')
  onResize() {
    this.currentWindowWidth = window.innerWidth
  }

}
