import { Injectable} from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { GlobalVariable } from '../global';
import { Game,GamePut } from './models/game';
import { SotrtFilter } from './models/sortFilters';

@Injectable({
    providedIn: 'root',
  })
  export class GameService{
    constructor(private http: HttpClient) { }

    public fetchGame(id:string){
      
      return this.http.get<Game>(GlobalVariable.BASE_API_URL+`game/${id}`);     
    }
    public fetchGames(sortFilters:any){
      let params = new HttpParams();
      Object.keys(sortFilters).forEach(function (key) {
        if(sortFilters[key]== null)
          return;     
      params = params.append(key, sortFilters[key]);
      });
        return this.http.get<Array<Game>>(GlobalVariable.BASE_API_URL + 'game/games?' +params);   
    }
    public fetchGamesByDeveloper(type:string,name:string){
      let params = new HttpParams().set('type',type).set('who',name);
      return this.http.get<Array<Game>>(GlobalVariable.BASE_API_URL + 'game/games/devorpub?'+params);   
    }
    public fetchGamesBySortFilters(sortFilters:any){
      let params = new HttpParams();
      Object.keys(sortFilters).forEach(function (key) {
        if(sortFilters[key]== null)
          return;     
      params = params.append(key, sortFilters[key]);
      });
  
        //console.log(GlobalVariable.BASE_API_URL + 'game/games?' +params);
        return this.http.get<Array<Game>>(GlobalVariable.BASE_API_URL + 'game/games?' +params);   
      }     
    public fecthGameByString(title:string){
      return this.http.get<Array<Game>>(GlobalVariable.BASE_API_URL + 'game/games/search?title='+title);   
    }
    public putGame(game:GamePut){
      return this.http.put<GamePut>(GlobalVariable.BASE_API_URL + 'game',game);
    }
    public deleteGame(id:string){
      return this.http.delete(GlobalVariable.BASE_API_URL + 'game?gameid=' + id);
    }
    public updateGame(game:any,id:string){
      return this.http.put(GlobalVariable.BASE_API_URL + 'game/update?id='+id,game);
    }
  }