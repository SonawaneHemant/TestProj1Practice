import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { RegisterUser, User, UserCreds } from '../../types/user';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private http=inject(HttpClient);
  currentUser=signal<User | null>(null);
  
  baseURl='https://localhost:7001/api/';

  login(creds:UserCreds){
    return this.http.post(this.baseURl+'account/login',creds).pipe(
      //whenever we get response from the api we will set the currentUser signal
      //tapping into the response and setting the currentUser signal
      tap((response:any)=>{
        const user:User=response;
         this.setCurrentUser(user);
      })
    );
  }

  resister(registercreds:RegisterUser){
    return this.http.post(this.baseURl+'account/registerdto',registercreds).pipe(
      //whenever we get response from the api we will set the currentUser signal
      //tapping into the response and setting the currentUser signal
      tap((response:any)=>{
        const user:User=response;
       this.setCurrentUser(user);
      })
    );
  }

  logOut(){
    this.currentUser.set(null);
    localStorage.removeItem('user');
  }

  setCurrentUser(user:User){
    if(user)
    {
    this.currentUser.set(user);
    localStorage.setItem('user',JSON.stringify(user));
    }
  }
  
  
}
