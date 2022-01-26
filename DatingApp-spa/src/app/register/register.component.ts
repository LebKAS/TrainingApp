import { AuthService } from './../_services/auth.service';
import { Component, EventEmitter, Input, OnInit, Output, NgModule } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
//  @Input() valueFromHome:any;
  @Output() cancelRegister=new EventEmitter();
  model:any={};  

  constructor(private authservice :AuthService) { }

  ngOnInit() {

  }
  register() {
    this.authservice.register(this.model).subscribe(
      () => {
        console.log("User is registered");
      }, 
      error=>{
        console.log(error);
      }

    );
  }
 cancel(){
   this.cancelRegister.emit(false);
   console.log('Cancelled');
 }

}
