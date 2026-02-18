import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { NgIf } from "@angular/common";
import { FormsModule } from "@angular/forms";

@Component({
  selector: 'app-nav',
  standalone: true,
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
  imports: [RouterLink, RouterLinkActive, NgIf, FormsModule],
})
export class NavComponent implements OnInit {
  constructor(private authService: AuthService) {}

  loginUser: any = {};

  ngOnInit() {}

  login() {
    this.authService.login(this.loginUser);
  }

  logOut(){
    this.authService.logOut();
  }

  get isAuthenticated(){
    return this.authService.loggedIn();
  }
}
