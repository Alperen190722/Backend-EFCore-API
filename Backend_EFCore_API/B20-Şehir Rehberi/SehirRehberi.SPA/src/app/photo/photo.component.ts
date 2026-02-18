import { Component, OnInit } from '@angular/core';
import { FileUploader,FileUploadModule} from 'ng2-file-upload';
import { AlertifyService} from '../services/alertify.service';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { Photo } from '../models/photo';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-photo',
  standalone:true,
  imports:[CommonModule,FileUploadModule],
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})
export class PhotoComponent implements OnInit {

  constructor(private authService:AuthService,
    private alertifyService:AlertifyService,
    private activatedRoute:ActivatedRoute
  ) { }

  photos:Photo[]=[];
 uploader!:FileUploader;
 hasBaseDropZoneOver:boolean=false;
 hasAnotherDropZoneOver: boolean = false;
 baseUrl: string = 'https://localhost:7067/api/';
 currentMain!:Photo;
 currentCity:any;
 response: string = '';

 fileOverBase(e: any): void {
        this.hasBaseDropZoneOver = e;
    }

    fileOverAnother(e: any): void {
        this.hasAnotherDropZoneOver = e;
    }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params=>{
      this.currentCity=params["cityId"]
    })
    this.initializeUploader();
  }

  initializeUploader(){
   this.uploader=new FileUploader({
    url:this.baseUrl+'cities/'+this.currentCity+'/photos',
    authToken:'Bearer '+localStorage.getItem('token'),
    isHTML5:true,
    allowedFileType : ['image'],
    autoUpload : false,
    removeAfterUpload : true,
    maxFileSize : 10*1024*1024
   });

   this.uploader.onSuccessItem = (item, response, status, headers)=>{
    if(response){
      const res:Photo = JSON.parse(response);
      const photo ={
        id:res.id,
        url:res.url,
        dateAdded:res.dateAdded,
        description:res.description,
        isMain:res.isMain,
        cityId:res.cityId
      }
      this.photos.push(photo)
    }
   }
   this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };
  }

}
