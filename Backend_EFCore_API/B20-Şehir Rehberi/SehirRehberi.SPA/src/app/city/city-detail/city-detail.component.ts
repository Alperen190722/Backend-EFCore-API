import { ChangeDetectorRef } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CityService } from '../../services/city.service';
import { City } from '../../models/city';
import { CommonModule } from '@angular/common';
import {
  NgxGalleryModule,
  NgxGalleryOptions,
  NgxGalleryImage,
  NgxGalleryAnimation,
} from '@kolkov/ngx-gallery';
import { Photo } from '../../models/photo';
import { PhotoComponent } from '../../photo/photo.component';
@Component({
  selector: 'app-city-detail',
  standalone: true,
  imports: [CommonModule, NgxGalleryModule, PhotoComponent],
  providers: [CityService],
  templateUrl: './city-detail.component.html',
  styleUrls: ['./city-detail.component.css'],
})
export class CityDetailComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private cityService: CityService,
    private cdr: ChangeDetectorRef,
  ) {}

  city!: City;
  photos: Photo[] = [];
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];

  ngOnInit() {
    this.activatedRoute.params.subscribe((params) => {
      const cityId = params['cityId'];
      this.getCityById(cityId);
      this.getPhotosByCity(cityId);
    });
  }

  getCityById(cityId: number) {
    this.cityService.getCityById(cityId).subscribe({
      next: (data) => {
        console.log('Servisten Gelen Veri:', data);
        this.city = data;
        this.photos=data.photos;
        this.setGallery();
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Detay Getirme Hatasi:', err);
      },
    });
  }
  getPhotosByCity(cityId:number){
    this.cityService.getPhotosByCity(cityId).subscribe(data=>{
    this.photos = data;
    this.setGallery();
    this.cdr.detectChanges();
    })
  }

getImages(){
  const imageUrls=[]
  for(let i = 0; i<this.photos.length;i++){
imageUrls.push({
  small:this.city.photos[i].url,
  medium:this.city.photos[i].url,
  big:this.city.photos[i].url
})
  }
  return imageUrls;
}

  setGallery() {
    this.galleryOptions = [
      {
        width: '100%',
        height: '400px',
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
      },
      // max-width 800
      {
        breakpoint: 800,
        width: '100%',
        height: '600px',
        imagePercent: 80,
        thumbnailsPercent: 20,
        thumbnailsMargin: 20,
        thumbnailMargin: 20,
      },
      // max-width 400
      {
        breakpoint: 400,
        preview: false,
      },
    ];

    this.galleryImages = this.getImages()
  }
}
