import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  // Properties
  country = 'Costa Rica';
  lastUpdate = '20-5-2020 10:00 a.m.';
  confirmed = 42612;
  active = 35697;
  dead = 696;
  recovered = 6219;
  nationals = 38512;
  foreign = 4100;

  constructor() { }

  ngOnInit(): void {
  }

}
