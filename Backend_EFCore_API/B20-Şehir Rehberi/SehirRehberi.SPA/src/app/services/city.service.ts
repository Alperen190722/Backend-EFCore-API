import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { City } from '../models/city';
import { Photo } from '../models/photo';
import { AlertifyService } from './alertify.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class CityService {

constructor(private httpClient:HttpClient,    
  private alertifyService:AlertifyService,
    private router:Router) { }
path="https://localhost:7067/api/cities";

getCities():Observable<City[]>{
  return this.httpClient.get<City[]>(this.path);
}
getCityById(cityId: number):Observable<City>{
  return this.httpClient.get<City>(this.path+"/detail/?id="+cityId)
}

getPhotosByCity(cityId:number):Observable<Photo[]>{
  return this.httpClient.get<Photo[]>(this.path+"/photos/?id="+cityId)
}
add(city : City){
  this.httpClient.post<City>(this.path + '/add', city).subscribe(data=>{
    this.alertifyService.success("Şehir başariyla eklendi.")
    this.router.navigateByUrl('/cityDetail/'+ data.id)
  });
}
}
