import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AnimalsResponseModel } from 'src/app/models/animalsresponsemodel.model';
import { AnimalResponseModel } from 'src/app/models/animalresponsemodel.model';
import { Animal } from 'src/app/models/animal.model';
import { AuthInterceptor } from 'src/app/services/auth.interceptor';
import { BaseResponseModel } from '../models/baseresponsemodel.model';

@Injectable({
  providedIn: 'root'
})
export class AnimalService {
  private apiUrl = 'https://localhost:7120/Animal/';  
  private interceptor: AuthInterceptor = new AuthInterceptor;

  constructor(private http: HttpClient) { }

  getAnimalsByKindId(categoryId: number): Observable<AnimalsResponseModel> {
    return this.http.get<AnimalsResponseModel>(this.apiUrl + 'GetAnimalsByKindId?kindId=' + categoryId);
  }

  getAnimalById(id: number): Observable<AnimalResponseModel> {
    return this.http.get<AnimalResponseModel>(this.apiUrl + 'GetAnimalById?id=' + id);
  }

  createAnimal(newAnimal: Animal): Observable<BaseResponseModel> {
    return this.http.post<BaseResponseModel>(this.apiUrl + 'CreateAnimal', newAnimal);
  }

  updateAnimal(animal: Animal): Observable<AnimalResponseModel> {
    return this.http.put<AnimalResponseModel>(this.apiUrl + 'UpdateAnimal', animal);
  }

  deleteAnimal(id: number): Observable<AnimalResponseModel> {
    return this.http.delete<AnimalResponseModel>(this.apiUrl + 'DeleteAnimal?id=' + id);
  }
}