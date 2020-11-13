import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Profile } from 'src/app/components/models/adm/profile.model';
import { QuickAccess } from 'src/app/components/models/adm/quickAccess.model';
import { Constants } from 'src/app/components/constants';

@Injectable({
    providedIn: 'root'
})
export class QuickAccessService {

    constructor(private http: HttpClient, private constants: Constants) { }    

    getQuickAccess(profile): Observable<QuickAccess[]> {
        let httpParams = new HttpParams().set('profile', profile)
        return this.http.get<QuickAccess[]>(this.constants.Url + 'quickaccess/getQuickAccess', { params: httpParams });
    };
    getAllQuickAccess(): Observable<QuickAccess[]> {
        return this.http.get<QuickAccess[]>(this.constants.Url + 'quickaccess/getallQuickAccess');
    };

    addQuickAccess(quickAccess: QuickAccess): Observable<QuickAccess> {
        return this.http.post<QuickAccess>(this.constants.Url + 'quickaccess/addQuickAccess', quickAccess);
    }

    updateQuickAccess(quickAccess: QuickAccess): Observable<QuickAccess> {
        return this.http.post<QuickAccess>(this.constants.Url + 'quickaccess/updateQuickAccess', quickAccess);
    }

    removeQuickAccess(quickAccess: QuickAccess): Observable<QuickAccess> {
        return this.http.post<QuickAccess>(this.constants.Url + 'quickaccess/removeQuickAccess', quickAccess);
    }
}