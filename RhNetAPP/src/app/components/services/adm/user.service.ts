import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Profile} from 'src/app/components/models/adm/profile.model';
import {Menu} from 'src/app/components/models/adm/menu.model';
import {Constants} from 'src/app/components/constants';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  constructor(private http : HttpClient, private constants: Constants) { }

  getRoles() : Observable<Profile[]> {
    return this.http.get<Profile[]>(this.constants.Url + 'user/getroles');
  };

  
}
