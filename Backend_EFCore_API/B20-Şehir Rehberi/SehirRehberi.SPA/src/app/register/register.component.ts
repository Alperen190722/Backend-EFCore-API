import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { CommonModule, JsonPipe } from '@angular/common';
import { FormBuilder,
  FormGroup,
  Validators,
  FormControl,ReactiveFormsModule } from '@angular/forms';


@Component({
  selector: 'app-register',
  standalone : true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports:[
    CommonModule,
    ReactiveFormsModule,
    JsonPipe
  ]
})
export class RegisterComponent implements OnInit {

  constructor(private authService:AuthService,
    private formBuilder:FormBuilder) { }
   registerForm! : FormGroup;
   registerUser:any={}
  ngOnInit() {
    this.createRegisterForm()
  }
createRegisterForm(){
  this.registerForm=this.formBuilder.group(
    {
      userName:["",Validators.required],
      password:["",[Validators.required,
      Validators.minLength(4),
      Validators.maxLength(8)]],
      confirmPassword:["",Validators.required]
    },
    {validator:this.passwordMatchValidator}
  )
}

passwordMatchValidator(g:FormGroup){
  return g.get('password')?.value === 
  g.get('confirmPassword')?.value?null:{mismatch:true}
}

register(){
  if(this.registerForm.valid){
    this.registerUser=Object.assign({},this.registerForm.value)
    this.authService.register(this.registerUser)
  }
}
}
