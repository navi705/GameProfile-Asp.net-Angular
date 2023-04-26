import { Component} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

import { GameService } from 'src/app/services/game.service';
import { Game } from 'src/app/services/models/game';

@Component({
    selector: 'games-catalog-page',
    templateUrl: './games.component.html',
    styleUrls: ['./games.component.css'],
    providers: [GameService]
  })
  export class GamesComponent{
    games:any;
    constructor(
      private route: ActivatedRoute,
      private gameService: GameService
    ){}
  
     ngOnInit(): void {
        this.gameService.fetchGames().subscribe(response => this.games = response);        
    }

  } 