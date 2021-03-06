import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Profile} from 'src/app/components/models/adm/profile.model';
import {ApplicationUser} from 'src/app/components/models/adm/applicationUser.model';
import {Constants} from 'src/app/components/constants';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  constructor(private http : HttpClient, private constants: Constants) { }

    getUsers(): Observable<ApplicationUser[]> {
        return this.http.get<ApplicationUser[]>(this.constants.Url + 'user/getusers');
    };

    getUser(userId): Observable<ApplicationUser> {
        let httpParams = new HttpParams().set('userId', userId);
        return this.http.get<ApplicationUser>(this.constants.Url + 'user/getuser', { params: httpParams });
    };

    addUser(user): Observable<ApplicationUser> {
        return this.http.post<ApplicationUser>(this.constants.Url + 'user/adduser', user);
    }; 

    updateUser(user): Observable<ApplicationUser> {
        return this.http.post<ApplicationUser>(this.constants.Url + 'user/updateuser', user);
    }; 

    getRoles(clientId : number): Observable<Profile[]> {
        let httpParams = new HttpParams().set('clientId', clientId.toString());
        return this.http.get<Profile[]>(this.constants.Url + 'user/getroles', { params: httpParams });
    };

    getAllRoles(): Observable<Profile[]> {
        return this.http.get<Profile[]>(this.constants.Url + 'user/getallroles');
    }; 

    addRole(profile): Observable<Profile> {
        return this.http.post<Profile>(this.constants.Url + 'user/addrole', profile);
    }; 

    updateRole(profile): Observable<Profile> {
        return this.http.post<Profile>(this.constants.Url + 'user/updateRole', profile);
    }; 

    setClient(client): Observable<any> {
        var refresh_token = localStorage.getItem('refresh_token');
        return this.http.post<any>(this.constants.Url + 'user/setClient', { clientModel: client, refresh_token: refresh_token });
    }; 

}
