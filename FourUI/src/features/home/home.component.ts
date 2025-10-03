import { Component, Input, input, signal, Signal } from '@angular/core';
import { RegisterComponent } from "../account/register/register.component";
import { User } from '../../types/user';

@Component({
  selector: 'app-home',
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  // @Input({required:true}) membersFromAppComp : User[] = [];

  protected resistermode = signal(false);

  showResisterMode(value : boolean) {
    this.resistermode.set(value);
  }
}
