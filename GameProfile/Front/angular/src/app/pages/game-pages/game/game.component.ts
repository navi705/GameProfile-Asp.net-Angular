import { Component} from '@angular/core';
import { ActivatedRoute} from '@angular/router';


import { GameService } from 'src/app/services/game.service';
import { Game } from 'src/app/services/models/game';



@Component({
    selector: 'game-page',
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.css'],
    providers: [GameService]
  })

  export class GameComponent{
    id: any;
    game: any;
    constructor(
      private route: ActivatedRoute,
      private gameService: GameService
    ){}
  
     ngOnInit(): void {
      this.route.queryParams.subscribe(params => {
      this.id = params['id'];
     this.gameService.fetchGame(this.id).subscribe(response => this.game = response);    
      });
    }

  } 