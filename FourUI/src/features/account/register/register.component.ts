import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterUser, User } from '../../../types/user';
import { AccountService } from '../../../core/services/account-service.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  private accountService=inject(AccountService);
  // membersFromHome = input.required<User[]>(); 
  cancelResister=output<boolean>();

  protected creds={} as RegisterUser;

  resister(){
    console.log(this.creds);
    this.accountService.resister(this.creds).subscribe({
      next:(response)=>{
        console.log(response);
        this.creds={} as RegisterUser;
        alert('Registration successful');
        this.cancel();
      },
      error:error=>{
        alert('Registration failed');
        console.log(error);
      }
    });
   
  } 

  cancel(){
   console.log("cancelled");
   this.cancelResister.emit(false);
  }
}
