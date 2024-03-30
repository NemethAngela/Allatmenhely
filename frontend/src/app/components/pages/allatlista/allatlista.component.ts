import { Component, OnInit } from '@angular/core';
import { AnimalService } from '../../../services/animal.service';
import { KindService } from '../../../services/kind.service';
import { Animal } from 'src/app/models/animal.model';
import { Kind } from 'src/app/models/kind.model';
import { Admin } from 'src/app/models/admin.model';
import { AuthService } from '../../../services/auth.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { LoginDialog } from '../../dialogs/login/logindialog';
import { LoginResponseModel } from 'src/app/models/loginresponsemodel.model';
import { AnimalDetails } from '../../dialogs/animaldetails/animal.details';
import { CreateanimalComponent } from '../../dialogs/createanimal/createanimal.component';

@Component({
  selector: 'app-root',
  templateUrl: './allatlista.component.html',
  styleUrls: ['./allatlista.component.css']
})
export class AllatlistaComponent implements OnInit {

  loggedInUser: LoginResponseModel | null | undefined;
  displayedColumns: string[] = ['id', 'name'];
  animalDogs: Animal[] = [];
  animalCats: Animal[] = [];
  animalOthers: Animal[] = [];
  dogKindId: number | undefined;
  catKindId: number | undefined;
  otherKindId: number | undefined;
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
      this.dogKindId = this.kinds.find(kind => kind.kind1 === "Kutya")?.id;
      this.catKindId = this.kinds.find(kind => kind.kind1 === "Macska")?.id;
      this.otherKindId = this.kinds.find(kind => kind.kind1 === "Egyéb")?.id;
      this.refreshAnimalList();
    });
    console.log('Bejelentkezve: ', this.loggedInUser);
  }

  refreshAnimalList(): void {
    this.animalService.getAnimalsByKindId(this.dogKindId ?? -1).subscribe(response => {
      this.animalDogs = response.animals;
    });
    this.animalService.getAnimalsByKindId(this.catKindId ?? -1).subscribe(response => {
      this.animalCats = response.animals;
    });
    this.animalService.getAnimalsByKindId(this.otherKindId ?? -1).subscribe(response => {
      this.animalOthers = response.animals;
    });
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

  onCreateAnimalClick(): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
        kinds: this.kinds,
        animalListComponent: this
    };

    const dialogRef = this.dialog.open(CreateanimalComponent, dialogConfig);
  }
}
