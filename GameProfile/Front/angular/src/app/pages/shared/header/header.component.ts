import { Component} from '@angular/core';
import {Observable} from 'rxjs';
import { GameService } from 'src/app/services/game.service';
import { Game } from 'src/app/services/models/game';
import { of } from 'rxjs';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css'],
    providers: [GameService]
  })

  export class HeaderComponent{
    image:any;

    searchString:string;
    findGames: Observable<Game[]>;

    constructor(public gameService:GameService){
      this.searchString = "";
      this.findGames = new Observable<Game[]>();
    }

    ngOnInit(): void {     
      if(localStorage.getItem('avatar') != undefined ){
        this.image = localStorage.getItem('avatar');
      }
    }
    onSearchChange(): void {  
      if(this.searchString == ""){
        this.findGames = new Observable<Game[]>();
        return;
      }    
      this.gameService.fecthGameByString(this.searchString).subscribe(response=> this.findGames=of(response));
    }
  }
