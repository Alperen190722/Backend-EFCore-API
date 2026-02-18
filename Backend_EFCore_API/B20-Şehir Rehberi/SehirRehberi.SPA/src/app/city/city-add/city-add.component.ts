import { Component, OnInit, OnDestroy } from '@angular/core';
import { CityService } from '../../services/city.service';
import {
  FormsModule,
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
  ReactiveFormsModule
} from '@angular/forms';
import { City } from '../../models/city';
import { CommonModule } from '@angular/common';
import { Editor, NgxEditorModule, Toolbar } from 'ngx-editor';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-city-add',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule,NgxEditorModule],
  templateUrl: './city-add.component.html',
  styleUrls: ['./city-add.component.css'],
  providers: [CityService]
})
export class CityAddComponent implements OnInit, OnDestroy {
  editor: Editor = new Editor(); // Editör motorunu burada çalıştırıyoruz
  
  // Üstteki buton takımı (Bold, Italic vs.)
  toolbar: Toolbar = [
    ['bold', 'italic'],
    ['underline', 'strike'],
    ['ordered_list', 'bullet_list'],
    [{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
    ['link', 'image'],
  ];
  ngOnDestroy(): void {
    this.editor.destroy(); // Sayfadan çıkınca motoru durduruyoruz
  }
  constructor(
    private cityService: CityService,
    private formBuilder: FormBuilder,
    private authService:AuthService
  ) {}

  city!: City;
  cityAddForm!: FormGroup;

  createCityForm() {
    this.cityAddForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.createCityForm();
  }

  add() {
    if (this.cityAddForm.valid) {
      this.city = Object.assign({}, this.cityAddForm.value);
      //Todo
      this.city.userId = this.authService.getCurrentUserId();
      this.cityService.add(this.city);
      
    }
  }
}
