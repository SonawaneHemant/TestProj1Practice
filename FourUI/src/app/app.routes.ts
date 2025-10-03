import { Routes } from '@angular/router';
import { HomeComponent } from '../features/home/home.component';
import { MemberListComponent } from '../features/member/member-list/member-list.component';
import { MemberDetailedComponent } from '../features/member/member-detailed/member-detailed.component';
import { ListsComponent } from '../features/lists/lists.component';
import { MessagesComponent } from '../features/messages/messages.component';

export const routes: Routes = [
    {path:'',component:HomeComponent},
    {path:'members',component:MemberListComponent},
    {path:'members/:id',component:MemberDetailedComponent},
    {path:'lists',component:ListsComponent},
    {path:'messages',component:MessagesComponent},
    {path:'**',component:HomeComponent},
];
