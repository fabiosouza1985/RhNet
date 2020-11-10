import { Injectable } from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const helper = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http : HttpClient) { }

  public getToken(): string {
    return localStorage.getItem('token');
  }
  public isAuthenticated(): boolean {
    // get the token
    const token = this.getToken();
    if (token === null){
      return false;
    }
    // return a boolean reflecting 
    // whether or not the token is expired    
    return helper.isTokenExpired(token);
  }

  public login( usuario: string,  senha: string): Observable<any>{
 
    return this.http.post<any>('http://localhost:5000/api/account/login', {Username: usuario, Password: senha});
  }

  public login1( usuario: string,  senha: string): Observable<any>{
   
    let grant_type = 'password';
   
    let body = `grant_type=${grant_type}&username=${usuario}&password=${senha}`;

    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');
  

    let options = {
      headers: headers
    };
  
    return this.http.post<any>('http://localhost:4200/rhweb/api/security/token', body, options);
  }
}
