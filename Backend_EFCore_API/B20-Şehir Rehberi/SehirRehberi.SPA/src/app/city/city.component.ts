import { Component, OnInit, signal } from '@angular/core';
import { City } from '../models/city';
import { CityService } from '../services/city.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-city',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css'],
  providers:[CityService]
})
export class CityComponent implements OnInit {
 cities = signal<City[]>([]);
  constructor(private cityService:CityService) { }
  ngOnInit() {
this.cityService.getCities().subscribe({
      next: (data) => {
        console.log("Gelen Veri:", data); // Verinin gelip gelmediğini konsolda gör
        this.cities.set(data); // Signal'i güncelle
    },
    error:(err)=>console.error("Hata oluştu:",err)
    
    });
  }

}
