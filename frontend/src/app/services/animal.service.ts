import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AnimalsResponseModel } from 'src/app/models/animalsresponsemodel.model';
import { AnimalResponseModel } from 'src/app/models/animalresponsemodel.model';
import { Animal } from 'src/app/models/animal.model';
import { Animalregistration } from 'src/app/models/animalregistration';
import { AuthInterceptor } from 'src/app/services/auth.interceptor';

@Injectable({
  providedIn: 'root'
})
export class AnimalService {
  private apiUrl = 'https://localhost:7120/Animal/'; 
  // private apiUrl = 'http://localhost:41577/Animal/'; 
  private interceptor: AuthInterceptor = new AuthInterceptor;

  constructor(private http: HttpClient) { }

  getAnimalsByKindId(categoryId: number): Observable<AnimalsResponseModel> {
    return this.http.get<AnimalsResponseModel>(this.apiUrl + 'GetAnimalsByKindId?kindId=' + categoryId);
  }

  getAnimalById(id: number): Observable<AnimalResponseModel> {
    return this.http.get<AnimalResponseModel>(this.apiUrl + 'GetAnimalById?id=' + id);
  }

  createAnimal(animalInfo: Animalregistration): Observable<Boolean> {    
    return this.http.post<Boolean>(this.apiUrl + 'CreateAnimal', animalInfo);    
  }
}