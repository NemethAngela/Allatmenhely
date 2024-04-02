import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AnimalsResponseModel } from 'src/app/models/animalsresponsemodel.model';
import { AnimalResponseModel } from 'src/app/models/animalresponsemodel.model';
import { Animal } from 'src/app/models/animal.model';
import { AuthInterceptor } from 'src/app/services/auth.interceptor';
import { BaseResponseModel } from '../models/baseresponsemodel.model';
import { Enquery } from '../models/enquery.model';
import { EnqueriesResponseModel } from '../models/enqueriesresponsemodel.model';

@Injectable({
  providedIn: 'root'
})
export class EnqueryService {
  private apiUrl = 'https://localhost:7120/Enquery/';

  constructor(private http: HttpClient) { }

  createEnquery(newEnquery: Enquery): Observable<BaseResponseModel>{
    return this.http.post<BaseResponseModel>(this.apiUrl + 'CreateEnquery', newEnquery);
  }

  getAllEnqueries(): Observable<EnqueriesResponseModel>{
    return this.http.get<EnqueriesResponseModel>(this.apiUrl + 'GetAllEnqueries');
  }

  getAllEnqueriesByAnimalId(){}

}
