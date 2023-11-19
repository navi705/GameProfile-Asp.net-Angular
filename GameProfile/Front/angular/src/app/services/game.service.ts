import { Injectable} from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { GlobalVariable } from '../global';
import { Game,GameForBrowse,GamePut } from './models/game';
import { SotrtFilter } from './models/sortFilters';

@Injectable({
    providedIn: 'root',
  })
  export class GameService{
     httpOptions = {
      withCredentials: true 
    };
  
    constructor(private http: HttpClient) { }

    public fetchGame(id:string){
      
      return this.http.get<Game>(GlobalVariable.BASE_API_URL+`game/${id}`);     
    }
    public fetchGames(sortFilters:any){
      const httpOptions = {
        withCredentials: true 
      };
      let params = new HttpParams();
      Object.keys(sortFilters).forEach(function (key) {
        if(sortFilters[key]== null)
          return;     
      params = params.append(key, sortFilters[key]);
      });
        return this.http.get<Array<GameForBrowse>>(GlobalVariable.BASE_API_URL + 'game/games?' +params,httpOptions);   
    }
    public fetchGamesByDeveloper(type:string,name:string){
      let params = new HttpParams().set('type',type).set('who',name);
      return this.http.get<Array<Game>>(GlobalVariable.BASE_API_URL + 'game/games/devorpub?'+params);   
    }

    public fetchGamesBySortFilters(sortFilters:any){
      const httpOptions = {
        withCredentials: true 
      };
      let params = new HttpParams();
      Object.keys(sortFilters).forEach(function (key) {
        if(sortFilters[key]== null)
          return;     
      params = params.append(key, sortFilters[key]);
      });
  
        console.log(GlobalVariable.BASE_API_URL + 'game/games?' +params);
        return this.http.get<Array<Game>>(GlobalVariable.BASE_API_URL + 'game/games?' +params,httpOptions);   
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
    public getRatingForUser(id:string){

      const httpOptions = {
        withCredentials: true 
      };
      return this.http.get<number>(GlobalVariable.BASE_API_URL + 'game/review?gameId='+id,httpOptions); 
    }
    public putRatingForUser(id:string,score: number){
      const httpOptions = {
        withCredentials: true 
      };
      return this.http.put(GlobalVariable.BASE_API_URL + 'game/review?gameId='+id+'&score='+score,null,httpOptions); 
    }

    public getGameComments(id:string){
      return this.http.get<Array<any>>(GlobalVariable.BASE_API_URL + 'game/comments?gameId='+id); 
    }

    public putComment(id:string,comment:string){
      const httpOptions = {
        withCredentials: true 
      };
      return this.http.put(GlobalVariable.BASE_API_URL + 'game/comments?gameId='+id+'&comment='+comment,null,httpOptions); 
    }

    public deleteComment(id:string){
      return this.http.delete(GlobalVariable.BASE_API_URL + 'game/comments?commentId='+id,this.httpOptions)
    }

    public updateComment(id:string,comment:string){
      return this.http.put(GlobalVariable.BASE_API_URL + 'game/comments/update?commentId='+id+'&comment='+comment,null,this.httpOptions); 
    }

    public putReplies(id:string,reply:string){
      return this.http.put(GlobalVariable.BASE_API_URL + 'game/replie?commentId='+id+'&replie='+reply,null,this.httpOptions); 
    }

    public deleteReplies(id:string){
      return this.http.delete(GlobalVariable.BASE_API_URL + 'game/replie?replieId='+id,this.httpOptions);
    }

    public updateReplies(id:string,reply:string){
      return this.http.put(GlobalVariable.BASE_API_URL + 'game/replie/update?replieId='+id+'&replie='+reply,null,this.httpOptions); 

    }

  }