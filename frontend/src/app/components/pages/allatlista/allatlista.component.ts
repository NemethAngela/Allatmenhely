import { Component, OnInit } from '@angular/core';
import { AnimalService } from '../../../services/animal.service';
import { KindService } from '../../../services/kind.service';
import { Animal } from 'src/app/models/animal.model';
import { Kind } from 'src/app/models/kind.model';
import { Admin } from 'src/app/models/admin.model';
import { AuthService } from '../../../services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { LoginDialog } from '../../dialogs/login/logindialog';
import { LoginResponseModel } from 'src/app/models/loginresponsemodel.model';
import { AnimalDetails } from '../../dialogs/animaldetails/animal.details';

@Component({
  selector: 'app-root',
  templateUrl: './allatlista.component.html',
  styleUrls: ['./allatlista.component.css']
})
export class AllatlistaComponent implements OnInit {

  loggedInUser: LoginResponseModel | null | undefined;
  displayedColumns: string[] = ['id', 'name'];
  animals: Animal[] = [];
  kinds: Kind[] = [];
  selectedKindId: number | undefined;

  constructor(
    public dialog: MatDialog, 
    private authService: AuthService,
    private animalService: AnimalService, 
    private kindService: KindService) { }

  ngOnInit(): void {
    this.authService.loggedInUser.subscribe((loginResponse) => {
      this.loggedInUser = loginResponse;
    });
    this.kindService.getAllKinds().subscribe(response => {
      this.kinds = response.kinds;
      console.log(this.kinds);
    });
    console.log('Bejelentkezve: ', this.loggedInUser);
  }

  onKindSelected(selectedKind: string): void {
    const selectedKindId = this.kinds.find(kind => kind.kind1 === selectedKind)?.id;

    this.selectedKindId = selectedKindId ? selectedKindId : -1;

    if (selectedKindId !== -1) {
      this.animalService.getAnimalsByKindId(this.selectedKindId).subscribe(response => {
        this.animals = response.animals;
        console.log(this.animals);
      });
    }
  }

  onLoginClick($event: MouseEvent): void {
    const dialogRef = this.dialog.open(LoginDialog, {
      data: { loggedInUser: this.loggedInUser },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result && result.loginResponse) {
        this.authService.setLoggedInUser(result.loginResponse);
        console.log('#Sikeres bejelentkezés', result.loginResponse.email);
      }
    });
  }

  onLogoutClick(): void {
    this.authService.setLoggedInUser(null);
  }
}
