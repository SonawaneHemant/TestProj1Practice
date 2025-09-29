import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../../types/user';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private http=inject(HttpClient);
  currentUser=signal<User | null>(null);
  
  baseURl='https://localhost:7001/api/';

  login(creds:any){
    return this.http.post(this.baseURl+'account/login',creds).pipe(
      //whenever we get response from the api we will set the currentUser signal
      //tapping into the response and setting the currentUser signal
      tap((response:any)=>{
        const user:User=response;
        this.currentUser.set(user);
        localStorage.setItem('user',JSON.stringify(user));
      })
    );
  }

  logOut(){
    this.currentUser.set(null);
    localStorage.removeItem('user');
  }
  
  
}
