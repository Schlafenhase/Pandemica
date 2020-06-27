import { Component, AfterViewInit } from '@angular/core';
import * as L from 'leaflet';
import {MapShapeService} from '../../services/map/map-shape.service';
import {CountryService} from '../../services/map/country.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements AfterViewInit {

  countryHover = '';
  private map;
  private countries;
  fill = '#43C59E';
  outline = '#ACFCD9';
  outline2 = '#E4BE25';
  highlight = '#F0D980';

  constructor(private shapeService: MapShapeService,
              private countryService: CountryService) { }

  ngAfterViewInit(): void {
    this.initMap();
    this.shapeService.getCountriesShapes().subscribe(countries => {
      this.countries = countries;
      this.initCountriesLayer();
    });
  }

  // Renders the map
  private initMap(): void {
    // Start the map
    this.map = L.map('map', {
      center: [20, 0],
      zoom: 2
    });
    // Populate the map
    const tiles = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 8,
      attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    });
    tiles.addTo(this.map);
  }

  // Paints the countries outline
  private initCountriesLayer() {
    const countryLayer = L.geoJSON(this.countries, {
      style: (feature) => ({
        weight: 3,
        opacity: 0.5,
        color: this.outline,
        fillOpacity: 0.8,
        fillColor: this.fill
      }),
      onEachFeature: (feature, layer) => (
        layer.on({
          mouseover: (e) => (this.highlightFeature(e)),
          mouseout: (e) => (this.resetFeature(e)),
          click: (e) => (this.clickFeature(e))
        })
      )
    });

    this.map.addLayer(countryLayer);
  }

  private highlightFeature(e)  {
    const layer = e.target;
    layer.setStyle({
      weight: 10,
      opacity: 1.0,
      color: this.outline2,
      fillOpacity: 1.0,
      fillColor: this.highlight,
    });
    // Writes country name on component
    const country = layer.feature.properties;
    this.countryHover = country.name;
  }

  private resetFeature(e)  {
    const layer = e.target;
    layer.setStyle({
      weight: 3,
      opacity: 0.5,
      color: this.outline,
      fillOpacity: 0.8,
      fillColor: this.fill
    });
  }

  private clickFeature(e) {
    const layer = e.target;
    const country = layer.feature.properties;
    this.countryService.countryPush(country.name);
  }
}
