import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Kind } from 'src/app/models/kind.model';
import { KindsResponseModel } from 'src/app/models/kindsresponsemodel.model';

@Injectable({
  providedIn: 'root'
})
export class KindService {
  private apiUrl = 'https://localhost:7120/Kind/'; 

  constructor(private http: HttpClient) { }

  getAllKinds(): Observable<KindsResponseModel> {
    return this.http.get<KindsResponseModel>(this.apiUrl + 'GetAllKinds');
  }
}