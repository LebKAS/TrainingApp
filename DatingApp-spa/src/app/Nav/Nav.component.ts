import { AlertifyService } from './../_services/alertify.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-Nav',
  templateUrl: './Nav.component.html',
  styleUrls: ['./Nav.component.css']
})
export class NavComponent implements OnInit {
model:any={};

  constructor(public authService : AuthService, 
        private alertify: AlertifyService,
        private router:Router) { }

  ngOnInit() {
  }
  login() {
    this.authService.login(this.model).subscribe(
        next=>{
          this.alertify.success('Logged in Successfully');
          //console.log('Logged in successfully');
        },
        error=>
        {
          //console.log(error);
          this.alertify.error(error);
        }


    )
  }
  loggedIn():boolean
  {
      // const token= localStorage.getItem('token');
      // return !!token;
      return this.authService.loggedIn();
  }
  logout()
  {
    localStorage.removeItem('token');
    this.router.navigate(['/home']);
    this.alertify.message('Logged Out');
    //console.log('Logged Out');
  }
}
