import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllatlistaComponent } from './components/pages/allatlista/allatlista.component';

const routes: Routes = [
  { path: '', redirectTo: '/allatlista', pathMatch: 'full' },
  { path: 'allatlista', component: AllatlistaComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
