import { Component, OnInit } from '@angular/core';

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

  constructor() { }

  ngOnInit(): void {
  }

}
