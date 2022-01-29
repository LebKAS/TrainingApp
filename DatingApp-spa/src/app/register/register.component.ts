import { AuthService } from './../_services/auth.service';
import { Component, EventEmitter, Input, OnInit, Output, NgModule } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
//  @Input() valueFromHome:any;
  @Output() cancelRegister=new EventEmitter();
  model:any={};  

  constructor(private authservice :AuthService, private alertfy: AlertifyService) { }

  ngOnInit() {

  }
  register() {
    this.authservice.register(this.model).subscribe(
      () => {
        this.alertfy.success("User is registered");
      }, 
      error=>{
        this.alertfy.error(error);
      }

    );
  }
 cancel(){
   this.cancelRegister.emit(false);
   this.alertfy.message('Cancelled');
 }

}
