import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { NavComponent } from "../layout/nav/nav.component";

@Component({
  selector: 'app-root',
  imports: [NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  private http =inject(HttpClient);//if you want to use httpClinet then you need to first provide that in app.config.ts then only you can use it here
  title = 'FourUI';
  protected members:any=[];
  protected signaltest=signal<any>([]);

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
  }

  async getMembers(){
      try {
        return lastValueFrom(this.http.get('https://localhost:7001/api/members'));
    }
    catch (error) {
        console.error('Error fetching title:', error);
        throw error;
    }
  } 

}
