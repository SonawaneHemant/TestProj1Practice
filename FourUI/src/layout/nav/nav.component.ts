import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service.service';
import { UserCreds } from '../../types/user';

@Component({
  selector: 'app-nav',
  imports: [FormsModule,CommonModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
 protected accountService=inject(AccountService);
  protected creds: UserCreds = { email: '', password: '' };
  //protected loginSignal=signal(false);

  login(){
    console.log(this.creds);
    this.accountService.login(this.creds).subscribe({
      next:(response)=>{
        alert('Login successful');
        console.log(response);
        this.creds = { email: '', password: '' };
      },
      error:error=>{
        alert('Login failed');
        console.log(error);
      }
    });
  }
  
  logOut(){
    this.accountService.logOut();
    alert('Logged out');
    //this.loginSignal.set(false);
  }

}
