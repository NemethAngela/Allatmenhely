import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './services/auth.interceptor';

import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AllatlistaComponent } from './components/pages/allatlista/allatlista.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatTableModule } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldControl, MatFormFieldModule } from '@angular/material/form-field';
import { MatTabsModule } from '@angular/material/tabs';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

import { AnimalCardComponent } from './components/animal-card/animal-card.component';
import { LoginDialog } from './components/dialogs/login/logindialog';
import { CreateanimalComponent } from './components/dialogs/createanimal/createanimal.component';
import { MatCheckbox, MatCheckboxModule } from '@angular/material/checkbox';
import { UpdateanimalComponent } from './components/dialogs/updateanimal/updateanimal.component';
import { DeleteanimalComponent } from './components/dialogs/deleteanimal/deleteanimal.component';
import { AnimalDetails } from './components/dialogs/animaldetails/animal.details';

@NgModule({
  declarations: [
    AllatlistaComponent,
    AnimalCardComponent,
    LoginDialog,
    AnimalDetails,
    CreateanimalComponent,
    UpdateanimalComponent,
    DeleteanimalComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatSelectModule,
    MatFormFieldModule,
    MatTabsModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    ReactiveFormsModule,
    MatIconModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AllatlistaComponent]
})
export class AppModule { }