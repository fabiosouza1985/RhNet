import { Injectable } from '@angular/core';
import { Constants } from 'src/app/components/constants';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property} from 'src/app/components/models/shared/property.model';
@Injectable({
  providedIn: 'root'
})
export class SharedService {

    constructor(private http: HttpClient, private constants: Constants) { }   

    getProperties(table: string): Observable<Property[]> {
        return this.http.get<Property[]>(this.constants.Url + table + '/getProperties');
    };

    get(table: string): Observable<any[]> {
        return this.http.get<any[]>(this.constants.Url + table + '/get');
    };

    getById(table: string, id: number): Observable<any> {
        let httpParams = new HttpParams().set('id', id.toString());
        return this.http.get<any>(this.constants.Url + table + '/getById', { params: httpParams });
    };

    add(table: string, object): Observable<any> {
        return this.http.post<any>(this.constants.Url + table + '/add', object);
    }

    update(table: string, object): Observable<any> {
        return this.http.post<any>(this.constants.Url + table + '/update', object);
    }

    remove(table: string, object): Observable<any> {
        return this.http.post<any>(this.constants.Url + table + '/remove', object);
    }
}
