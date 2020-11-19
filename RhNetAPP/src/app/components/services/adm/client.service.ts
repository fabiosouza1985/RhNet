import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Client } from 'src/app/components/models/adm/client.model';
import { Constants } from 'src/app/components/constants';

@Injectable({
    providedIn: 'root'
})
export class ClientService {

    constructor(private http: HttpClient, private constants: Constants) { }

 
    getAllClients(): Observable<Client[]> {
        return this.http.get<Client[]>(this.constants.Url + 'client/getallClients');
    };

    getClients(): Observable<Client[]> {
        return this.http.get<Client[]>(this.constants.Url + 'client/getClients');
    };

    add(client: Client): Observable<Client> {
        return this.http.post<Client>(this.constants.Url + 'client/addClient', client);
    }

    update(client: Client): Observable<Client> {
        return this.http.post<Client>(this.constants.Url + 'client/updateClient', client);
    }

    remove(client: Client): Observable<Client> {
        return this.http.post<Client>(this.constants.Url + 'client/removeClient', client);
    }
}