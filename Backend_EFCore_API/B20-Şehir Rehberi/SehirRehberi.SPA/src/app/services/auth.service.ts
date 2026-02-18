import { Injectable } from '@angular/core';
import { LoginUser } from '../models/loginUser';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { AlertifyService } from './alertify.service';
import { RegisterUser } from '../models/registerUser';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private alertifyService: AlertifyService,
  ) {}
  path = 'https://localhost:7067/api/auth/';
  userToken: any;
  decodedToken: any;
  jwtHelper: JwtHelperService = new JwtHelperService();
  TOKEN_KEY="token"
  login(loginUser: LoginUser) {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    this.httpClient
      .post(this.path + 'login', loginUser, { headers: headers, responseType:'text' })
      .subscribe({
      next:(data: string) => {
        this.saveToken(data);
        this.userToken = data;
        this.decodedToken = this.jwtHelper.decodeToken(data);
        this.alertifyService.success('Sisteme giriş yapildi');
        this.router.navigateByUrl('/city');
      },
      error:(err)=>{
        console.error(err);
        this.alertifyService.error('Kullanici adi veya şifre hatali!');
      }
      });
  }

  register(registerUser: RegisterUser) {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    this.httpClient
      .post(this.path + 'register', registerUser, { headers: headers })
      .subscribe((data) => {});
  }

  saveToken(token: any) {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  logOut(){
    localStorage.removeItem(this.TOKEN_KEY)
    this.alertifyService.error("Sistemden çikiş yapildi")
  }

  loggedIn(){
    const token = this.token;
    return token ? !this.jwtHelper.isTokenExpired(token) : false;
  }
get token(){
  return localStorage.getItem(this.TOKEN_KEY);
}
  getCurrentUserId(){
    const currentToken=this.token;
    if(currentToken){
    return this.jwtHelper.decodeToken(currentToken).nameid
    }
    return null;
  }
}
