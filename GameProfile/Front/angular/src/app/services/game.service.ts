import { Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { GlobalVariable } from '../global';
import { Game,GamePut } from './models/game';

@Injectable({
    providedIn: 'root',
  })
  export class GameService{
    constructor(private http: HttpClient) { }

    public fetchGame(id:string){
      
      return this.http.get<Game>(GlobalVariable.BASE_API_URL+`game/${id}`);     
    }
    public fetchGames(){
      return this.http.get<Array<Game>>(GlobalVariable.BASE_API_URL + 'game/games');   
    }
    public putGame(game:GamePut){
      return this.http.put<GamePut>(GlobalVariable.BASE_API_URL + 'game',game);
    }
  }