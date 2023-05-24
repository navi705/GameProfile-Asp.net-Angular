import { Injectable} from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { GlobalVariable } from '../global';

@Injectable({
    providedIn: 'root',
  })
export class FilterService{
    constructor(private http: HttpClient) { }
    public fetchGenres(){
        return this.http.get<Array<any>>(GlobalVariable.BASE_API_URL+`game/genres`);    
    }
    
}