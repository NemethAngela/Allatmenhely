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
    selector: 'app-updateanimal',
    templateUrl: './updateanimal.component.html'      
})
export class UpdateanimalComponent {
    animal: Animal;
    kinds: Kind[] = [];
    selectedFile: File | null = null;

    name = new FormControl('', [Validators.required]);
    kindId = new FormControl(0, [Validators.required]);
    age = new FormControl(1, [Validators.required]);
    isMale = new FormControl(false);
    isNeutered = new FormControl(false);
    description = new FormControl('');

    constructor(
        private animalService: AnimalService,
        private kindService: KindService,
        private router: Router,
        private dialogRef: MatDialogRef<UpdateanimalComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { animal: Animal },
        
    ) {
        this.animal = this.data.animal; 
        this.kindService.getAllKinds().subscribe(response => {
            this.kinds = response.kinds;
            console.log(this.kinds);
        });
    }

    ngOnInit(): void{
        this.name.setValue(this.animal.name);
        this.kindId.setValue(this.animal.kindId);
        this.age.setValue(this.animal.age);
        this.isMale.setValue(this.animal.isMale ?? false);
        this.isNeutered.setValue(this.animal.isNeutered);
        this.description.setValue(this.animal.description ?? '');
    }

    onSaveAnimalClick() {
        if (this.name.valid && this.kindId.valid && this.age.valid) {
            this.convertImageToBase64().then(base64String => {
                const updatedAnimal: Animal = {
                    id: this.animal.id,
                    name: this.name.value !== null ? this.name.value : "",
                    kindId: this.kindId.value !== null && this.kindId.value !== undefined ? this.kindId.value : 0,
                    age: this.age.value !== null && this.age.value !== undefined ? this.age.value : 0,
                    isMale: this.isMale.value !== null ? this.isMale.value : false,
                    isNeutered: this.isNeutered.value !== null ? this.isNeutered.value : false,
                    description: this.description !== null ? this.description.value : "",
                    photo: base64String
                };

                // Elküldjük az új állatot a szolgáltatásnak
                this.animalService.updateAnimal(updatedAnimal).subscribe(
                    result => {
                        console.log('Állatinfó: ', updatedAnimal);
                        this.dialogRef.close();
                    },
                    error => {
                        console.log('Mentési hiba!', error);
                        console.log('Állatinfó: ', updatedAnimal);
                    }
                );
            });
        }
    }

    onFileSelected(event: any) {
        this.selectedFile = event.target.files[0];
    }

    private convertImageToBase64(): Promise<string> {
        return new Promise((resolve, reject) => {
            if (this.selectedFile) {
                const reader = new FileReader();
                reader.readAsDataURL(this.selectedFile);
                reader.onload = () => {
                    resolve(reader.result as string);
                };
                reader.onerror = error => reject(error);
            } else {
                resolve('');
            }
        });
    }
}