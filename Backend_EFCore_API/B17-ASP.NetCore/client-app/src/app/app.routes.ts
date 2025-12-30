import { Routes } from '@angular/router';
import { CustomerComponent } from './customer/customer'; 
export const routes: Routes = [{path : 'customer', component: CustomerComponent},
{ path: '', redirectTo: '/customers', pathMatch: 'full' }
];
