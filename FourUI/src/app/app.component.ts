import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { NavComponent } from "../layout/nav/nav.component";
import { AccountService } from '../core/services/account-service.service';
import { HomeComponent } from "../features/home/home.component";
import { User } from '../types/user';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [NavComponent, RouterOutlet, NgClass],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  private accountService=inject(AccountService);
  protected router=inject(Router);
  private http =inject(HttpClient);//if you want to use httpClinet then you need to first provide that in app.config.ts then only you can use it here
  title = 'FourUI';
  protected members:any=[];
  protected signaltest=signal<User[]>([]);

  async ngOnInit() {
    
    //Observable needs to Subscribe
    this.http.get('https://localhost:7001/api/members').subscribe({
      next: (response) => {
        console.log(response);
        this.members=response;
        //you can also use signal here
        //this.signaltest.set(response);
      },
      error: (error) => {
        console.error('Error fetching title:', error);
      },
      complete: () => {
        console.log('Request completed');
      }
    });

    //Promise needs to await and also its a demo of how to use signal
    this.signaltest.set(await this.getMembers());
    this.setCurrentUser();
  }

  setCurrentUser(){
    const userString=localStorage.getItem('user');
    if(!userString) return;
    else{
      const user=JSON.parse(userString);
      this.accountService.currentUser.set(user);
    }
  }

  async getMembers(){
      try {
        return lastValueFrom(this.http.get<User[]>('https://localhost:7001/api/members'));
    }
    catch (error) {
        console.error('Error fetching title:', error);
        throw error;
    }
  } 

}
