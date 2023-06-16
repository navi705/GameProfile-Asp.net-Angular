import { Injectable} from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { GlobalVariable } from '../global';

@Injectable({
    providedIn: 'root',
  })

  export class StatsService{
    constructor(private http: HttpClient) { }
    public GetStats(){
      return this.http.get(GlobalVariable.BASE_API_URL + 'stats');
    }
    public GetCount(){
      return this.http.get(GlobalVariable.BASE_API_URL + 'count');
    }
  }