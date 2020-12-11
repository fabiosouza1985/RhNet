import { Injectable } from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/components/models/adm/user.model';
import { Client } from 'src/app/components/models/adm/client.model';
import {Constants} from 'src/app/components/constants';
const helper = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  is_refreshing: boolean = false;

  constructor(private http : HttpClient, private constants: Constants) { }

  public getToken(): string {
    return localStorage.getItem('token');
    }

    public getRefreshToken(): string {
        return localStorage.getItem('refresh_token');
    }

    public getCurrentClient(): string {
        return localStorage.getItem('currentClient');
    }

  public isAuthenticated(): boolean {
    // get the token
      const token = this.getToken();      
      if (token === null) {

      return false;
    }
    // return a boolean reflecting 

    // whether or not the token is expired 
    

    return !helper.isTokenExpired(token);
  }

    public login(usuario: string, senha: string, selectedClient: Client): Observable<any>{
        //let headers = new HttpHeaders();
        //headers.append('Content-Type', 'application/x-www-form-urlencoded');

        //let grant_type = 'password';
        //let body = `grant_type=${grant_type}&username=${usuario}&password=${senha}&SelectedClient=${selectedClient}`;

        //let options = {
        //    headers: headers
        //};
      //return this.http.post<any>(this.constants.Url + 'security/token', body,options);
      return this.http.post<any>(this.constants.Url + 'account/login', { Username: usuario, Password: senha, SelectedClient: selectedClient });
      
    }

    public refresh_token() {
        
        if (this.is_refreshing) return;

        this.is_refreshing = true;

        var experiation_date = new Date(this.getExpirationToken());
        var now = new Date();

        var diferenca = Math.abs(now.getTime() - experiation_date.getTime());
        let minutos = Math.round(((diferenca % 86400000) % 3600000) / 60000);

        if (minutos < 10) {
            console.log('refresh');
            this.login_refresh().subscribe(results => {
                localStorage.setItem('token', results.access_token);
                this.is_refreshing = false;
            },
                (err) => {
                    console.log(err)
                    this.is_refreshing = false;
                });
        } else {
            this.is_refreshing = false;
        }
       
    };

    public login_refresh(): Observable<any> {

        const refresh_token = this.getRefreshToken();
        const currentClient = this.getCurrentClient();

        if (currentClient == null) {
            console.log('Cliente atual inválido')
            return;
        }

        return this.http.post<any>(this.constants.Url + 'user/setClient', { clientModel: { cnpj: currentClient}, refresh_token: refresh_token });
        
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

    public getExpirationToken() {
        return helper.getTokenExpirationDate(this.getToken());
    }
}
