import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AnimalsResponseModel } from 'src/app/models/animalsresponsemodel.model';
import { AnimalResponseModel } from 'src/app/models/animalresponsemodel.model';

@Injectable({
  providedIn: 'root'
})
export class AnimalService {
  private apiUrl = 'https://localhost:7120/Animal/'; 

  constructor(private http: HttpClient) { }

  getAnimalsByKindId(categoryId: number): Observable<AnimalsResponseModel> {
    return this.http.get<AnimalsResponseModel>(this.apiUrl + 'GetAnimalsByKindId?kindId=' + categoryId);
  }

  getAnimalById(id: number): Observable<AnimalResponseModel> {
    return this.http.get<AnimalResponseModel>(this.apiUrl + 'GetAnimalById?id=' + id);
  }
}