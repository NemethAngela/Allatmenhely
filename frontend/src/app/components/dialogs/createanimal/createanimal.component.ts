import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Animal } from 'src/app/models/animal.model';
import { AnimalService } from 'src/app/services/animal.service';
import { MatFormFieldControl, MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { Animalregistration } from 'src/app/models/animalregistration';
import { AuthInterceptor } from 'src/app/services/auth.interceptor';

// export interface Animal {
//     id: number;
//     name: string;
//     kindId: number;
//     age: number;
//     isMale?: boolean | null;
//     isNeutered: boolean;
//     description?: string | null;
//     photo?: string | null;
//     isActive: boolean;
//     timeStamp: Date;

// `Id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
//   `Name` varchar(50) NOT NULL,
//   `KindId` integer NOT NULL,
//   `Age` integer NOT NULL,
//   `IsMale` bit NULL DEFAULT null,
//   `IsNeutered` bit NOT NULL DEFAULT 0,
//   `Description` text DEFAULT null,
//   `Photo` BLOB DEFAULT null,
//   `IsActive` bit NOT NULL DEFAULT 1,
//   `TimeStamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP

@Component({
    selector: 'app-createanimal',
    templateUrl: './createanimal.component.html',
    //standalone: true,
    // imports: [CommonModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatCheckboxModule, MatDatepickerModule, MatNativeDateModule, MatIconModule],
    // imports: [
    //     CommonModule,
    //  ],    
    // styleUrls: ['./createanimal.component.css'],
    // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreateanimalComponent {

    name = new FormControl('', [Validators.required]);
    kindId = new FormControl('', [Validators.required]);
    age = new FormControl('', [Validators.required]);
    isMale = new FormControl('', [Validators.required]);
    description = new FormControl('', [Validators.required]);

    constructor(
        private animalService: AnimalService,
        private router: Router,
        private dialogRef: MatDialogRef<CreateanimalComponent>

    ) { }

    onSaveAnimalClick() {
        if (this.name.valid && this.kindId.valid && this.age.valid) {
            const animalInfo: Animalregistration = {
                name: this.name.value !== null ? this.name.value : "",
                kindId: this.kindId.value !== null ? this.kindId.value : "",
                age: this.age.value !== null ? this.age.value : "",
                isMale: this.isMale.value !== null ? this.age.value === "true": false,
                description: this.description !== null ? this.description.value : ""

            }

            this.animalService.createAnimal(animalInfo).subscribe(
                result => {
                    console.log('Állatinfó: ', animalInfo);
                    this.dialogRef.close();
                },
                error => {
                    console.log('Mentési hiba!', error);
                    console.log('Állatinfó: ', animalInfo);
                }
            )
        }

    }
}
