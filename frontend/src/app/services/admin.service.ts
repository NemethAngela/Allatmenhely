import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Admin } from 'src/app/models/admin.model';
import { LoginRequestModel } from 'src/app/models/loginrequestmodel.model';
import { LoginResponseModel } from 'src/app/models/loginresponsemodel.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private baseUrl = 'https://localhost:7120/Admin/'; 

  constructor(private http: HttpClient) { }

  login(loginRequest: LoginRequestModel): Observable<LoginResponseModel> {
    return this.http.post<LoginResponseModel>(`${this.baseUrl}Login`, loginRequest);
  }
}