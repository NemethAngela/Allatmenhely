import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponseModel } from 'src/app/models/loginresponsemodel.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private loggedInUserSubject: BehaviorSubject<LoginResponseModel | null>;

  constructor() {
    const storedUser = localStorage.getItem('loggedInUser');
    this.loggedInUserSubject = new BehaviorSubject<LoginResponseModel | null>(
      storedUser ? JSON.parse(storedUser) : null
    );
  }

  get loggedInUser(): Observable<LoginResponseModel | null> {
    return this.loggedInUserSubject.asObservable();
  }

  setLoggedInUser(loginResponse: LoginResponseModel | null): void {
    if (loginResponse) {
      localStorage.setItem('loggedInUser', JSON.stringify(loginResponse.email));
      localStorage.setItem('loggedInUserToken', JSON.stringify(loginResponse.token));
    } else {
      localStorage.removeItem('loggedInUser');
      localStorage.removeItem('loggedInUserToken');
    }
    this.loggedInUserSubject.next(loginResponse);
  }
}
