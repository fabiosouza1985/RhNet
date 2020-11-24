import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Permission } from 'src/app/components/models/adm/permission.model';
import { Constants } from 'src/app/components/constants';

@Injectable({
    providedIn: 'root'
})
export class PermissionService {

    constructor(private http: HttpClient, private constants: Constants) { }

    getAllPermissions(): Observable<Permission[]> {
        return this.http.get<Permission[]>(this.constants.Url + 'permission/getAllPermissions');
    };

    addPermission(permission: Permission): Observable<Permission> {
        return this.http.post<Permission>(this.constants.Url + 'permission/addPermission', permission);
    }

    updatePermission(permission: Permission): Observable<Permission> {
        return this.http.post<Permission>(this.constants.Url + 'permission/updatePermission', permission);
    }

    removePermission(permission: Permission): Observable<Permission> {
        return this.http.post<Permission>(this.constants.Url + 'permission/removePermission', permission);
    }
}