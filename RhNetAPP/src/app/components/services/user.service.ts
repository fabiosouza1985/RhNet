import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Profile} from 'src/app/components/models/adm/profile.model';
@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = 'http://localhost/rhnet/api/user'
  constructor(private http : HttpClient) { }

  getRoles() : Observable<Profile[]> {
    return this.http.get<Profile[]>(this.baseUrl + '/getRoles');
};
}
