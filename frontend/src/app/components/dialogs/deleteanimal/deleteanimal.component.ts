import { CommonModule, NgFor } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { FormControl, FormsModule, Validators } from '@angular/forms';
import { Animal } from 'src/app/models/animal.model';
import { AnimalService } from 'src/app/services/animal.service';
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Router } from '@angular/router';
import { Kind } from 'src/app/models/kind.model';
import { KindService } from 'src/app/services/kind.service';

@Component({
    selector: 'app-deleteanimal',
    templateUrl: './deleteanimal.component.html'
    
})
export class DeleteanimalComponent { 
    animal: Animal;
    photo: string = '';
    maleIcon: string = 'data:image/png;base64,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';
    femaleIcon: string = 'data:image/png;base64,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';
    

    constructor(
        private animalService: AnimalService,
        private kindService: KindService,
        private router: Router,
        private dialogRef: MatDialogRef<DeleteanimalComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { animal: Animal }
    ) {
        this.animal = this.data.animal;
        
    }

    ngOnInit(): void {
        console.log(this.animal);
        const photoData = this.animal.photo;

        if (photoData !== null && photoData !== undefined) {
          if (photoData.startsWith("data:image/")) {
            this.photo = photoData;
          } else {
            this.photo = "data:image/jpeg;base64," + photoData;
          }
        } else {
          this.photo = "data:image/jpeg;base64,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";
        }
    }

    onDeleteAnimalClick($event: MouseEvent): void{
        console.log("Ezt fogom törölni: ", this.animal.id);
        this.animalService.deleteAnimal(this.animal.id ?? -1).subscribe(
            result => {
                console.log('Állat ID: ', this.animal.id);
                this.dialogRef.close();
            },
            error => {
                console.log('Mentési hiba!', error);
                console.log('Állat ID: ', this.animal.id);
            }
        );
        
    }

    onCancelClick($event: MouseEvent){
        this.dialogRef.close();
    }
}
