import { Component, Input  } from '@angular/core';
import { Animal } from 'src/app/models/animal.model';
import { AnimalDetails } from '../dialogs/animaldetails/animal.details';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { LoginResponseModel } from 'src/app/models/loginresponsemodel.model';
import { LoginDialog } from '../dialogs/login/logindialog';
import { AuthService } from 'src/app/services/auth.service';
import { AnimalService } from 'src/app/services/animal.service';
import { UpdateanimalComponent } from '../dialogs/updateanimal/updateanimal.component';
import { Kind } from 'src/app/models/kind.model';
import { DeleteanimalComponent } from '../dialogs/deleteanimal/deleteanimal.component';

@Component({
  selector: 'app-animal-card',
  templateUrl: './animal-card.component.html',
  styleUrls: ['./animal-card.component.css']
})
export class AnimalCardComponent {
  
  @Input()
  animal!: Animal;
  kinds: Kind[] = [];
  photo: string = '';
  loggedInUser: LoginResponseModel | null | undefined;
  
  constructor(
    public dialog: MatDialog,
    private authService: AuthService
    ) { }

  ngOnInit(): void {
    const photoData = this.animal.photo;
    
    this.authService.loggedInUser.subscribe((loginResponse) => {
      this.loggedInUser = loginResponse;
    });

    if (photoData !== null && photoData !== undefined && photoData !== "") {
      if (photoData.startsWith("data:image/")) {
        this.photo = photoData;
      } else {
        this.photo = "data:image/jpeg;base64," + photoData;
      }
    } else {
      this.photo = "data:image/jpeg;base64,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";
    }
  }

  showDetails($event: MouseEvent): void {
    const dialogRef = this.dialog.open(AnimalDetails, {
      data: { animal: this.animal },
    });
  }

  reserve() {
    // Foglalás logikája
    console.log("Foglalom!");
  }

  onUpdateAnimalClick() {
    console.log("Módosítom!")
    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      animal: this.animal
    }

    const dialogRef = this.dialog.open(UpdateanimalComponent, dialogConfig);
  }

  deleteAnimal($event: MouseEvent): void {
    console.log("Törlés dialógus megnyitása")
    const dialogRef = this.dialog.open(DeleteanimalComponent, {
      data: { animal: this.animal },
    });
  }

}
