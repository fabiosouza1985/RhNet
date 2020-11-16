import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Constants } from 'src/app/components/constants';
import { Favorite } from 'src/app/components/models/adm/favorite.model';

@Injectable({
    providedIn: 'root'
})
export class FavoriteService {

    constructor(private http: HttpClient, private constants: Constants) { }    

    getFavorites(profile): Observable<Favorite[]> {
        let httpParams = new HttpParams().set('profile', profile);
        return this.http.get<Favorite[]>(this.constants.Url + 'favorite/getFavorites', { params: httpParams });
    };

    isFavorite(path): Observable<boolean> {
        let httpParams = new HttpParams().set('path', path)
        return this.http.get<boolean>(this.constants.Url + 'favorite/isFavorite', { params: httpParams });
    };

    addFavorite(path): Observable<Favorite> {
        var favoriteModel = {header: '', path: path};
        return this.http.post<Favorite>(this.constants.Url + 'favorite/addFavorite', favoriteModel);
    };

    removeFavorite(path): Observable<Favorite> {
        var favoriteModel = { header: '', path: path };
        return this.http.post<Favorite>(this.constants.Url + 'favorite/removeFavorite', favoriteModel);
    };
}