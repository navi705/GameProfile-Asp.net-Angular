import { Component} from '@angular/core';
import { ActivatedRoute } from '@angular/router';


import { GameService } from 'src/app/services/game.service';
import { Game,GamePut } from 'src/app/services/models/game';
@Component({
    selector: 'game-add',
    templateUrl: './add-game.component.html',
    styleUrls: ['./add-game.component.css'],
    providers: [GameService]
  })
  export class GameAddComponent{
    name:any;
    date:any;
    image:any;
    nsfw:any;
    constructor(
        private route: ActivatedRoute,
        private gameService: GameService
      ){}
    
       ngOnInit(): void {     

      }
      createGame(){
        let game:GamePut = {title: this.name, releaseDate: this.date,headerImage: this.image, nsfw: false, 
        description: '', screenshots: [], genres: [],publishers: [], developers: [],shopsLinkBuyGame:[],achievementsCount: 0} ;
        console.log(game);
        this.gameService.putGame(game).subscribe();
      }
  }