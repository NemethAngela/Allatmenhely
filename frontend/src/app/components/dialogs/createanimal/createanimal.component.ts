import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Animal } from 'src/app/models/animal.model';
import { AnimalService } from 'src/app/services/animal.service';
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Router } from '@angular/router';
import { Kind } from 'src/app/models/kind.model';

@Component({
    selector: 'app-createanimal',
    templateUrl: './createanimal.component.html'
})
export class CreateanimalComponent {
    kinds: Kind[] = [];

    name = new FormControl('test', [Validators.required]);
    kindId = new FormControl(0, [Validators.required]);
    age = new FormControl(1, [Validators.required]);
    isMale = new FormControl(false);
    isNeutered = new FormControl(false);
    description = new FormControl('test description');

    constructor(
        private animalService: AnimalService,
        private router: Router,
        private dialogRef: MatDialogRef<CreateanimalComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { kinds: Kind[] }
    ) {
        this.kinds = this.data.kinds; 
    }

    onSaveAnimalClick() {
        if (this.name.valid && this.kindId.valid && this.age.valid) {
            const newAnimal: Animal = {
                name: this.name.value !== null ? this.name.value : "",
                kindId: this.kindId.value !== null && this.kindId.value !== undefined ? this.kindId.value : 0,
                age: this.age.value !== null && this.age.value !== undefined ? this.age.value : 0,
                isMale: this.isMale.value !== null ? this.isMale.value : false,
                isNeutered: this.isNeutered.value !== null ? this.isNeutered.value : false,
                description: this.description !== null ? this.description.value : "",
            }

            this.animalService.createAnimal(newAnimal).subscribe(
                result => {
                    console.log('Állatinfó: ', newAnimal);
                    this.dialogRef.close();
                },
                error => {
                    console.log('Mentési hiba!', error);
                    console.log('Állatinfó: ', newAnimal);
                }
            )
        }
    }
}
