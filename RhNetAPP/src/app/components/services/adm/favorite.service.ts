import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Constants } from 'src/app/components/constants';

@Injectable({
    providedIn: 'root'
})
export class FavoriteService {

    constructor(private http: HttpClient, private constants: Constants) { }    

    isFavorite(path): Observable<boolean> {
        let httpParams = new HttpParams().set('path', path)
        return this.http.get<boolean>(this.constants.Url + 'favorite/isFavorite', { params: httpParams });
    };

    addFavorite(path): Observable<string> {
        let httpParams = new HttpParams().set('path', path)
        return this.http.post<string>(this.constants.Url + 'favorite/addFavorite', { params: httpParams });
    };

  
}