import { ChangeDetectorRef } from '@angular/core'; // Bunu ekle
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common'; // *ngFor için şart
import {Value} from '../models/value';
@Component({
  selector: 'app-value',
  standalone: true, // Yeni nesil Angular kuralı
  imports: [CommonModule], // Template'de döngü kullanabilmek için
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css'],
})
export class ValueComponent implements OnInit {
  values:Value[]=[];

  constructor(private http:HttpClient, private cdr: ChangeDetectorRef) { }

ngOnInit() {
  console.log("1. ngOnInit çalıştı");
  this.getValues().subscribe({
    next: (data) => {
    this.values = data;
  this.cdr.detectChanges(); // "Hey Angular, veri değişti, ekranı hemen güncelle!" diyorsun
  console.log("Ekran zorla güncellendi!");

    },
    error: (err) => {
      console.error("API Hatası:", err);
    }
  });
}
  getValues()
  {
    return this.http.get<Value[]>("https://localhost:7067/api/values")
  }

}
