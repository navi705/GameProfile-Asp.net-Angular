import { Injectable} from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { GlobalVariable } from '../global';

@Injectable({
    providedIn: 'root',
  })

  export class FindTeammateService{
    constructor(private http: HttpClient) { }
    public getTeammates(){

    }
  }