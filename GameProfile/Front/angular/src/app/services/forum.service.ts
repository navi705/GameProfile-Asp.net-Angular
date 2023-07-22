import { Injectable} from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { GlobalVariable } from '../global';
import { SortForForum } from './models/sortForForum';

@Injectable({
    providedIn: 'root',
  })

  export class ForumService{
    constructor(private http: HttpClient) { }
    public getForum(sortForForum : SortForForum){

    }
    
  }