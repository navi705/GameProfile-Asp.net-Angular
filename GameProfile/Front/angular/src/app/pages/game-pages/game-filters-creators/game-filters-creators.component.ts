import { Component } from '@angular/core';
import { GameService } from 'src/app/services/game.service';
import { ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-game-filters-creators',
  templateUrl: './game-filters-creators.component.html',
  styleUrls: ['./game-filters-creators.component.css'],
  providers:[GameService]
})
export class GameFiltersCreatorsComponent {
  games:any;
  type:any;
  name:any;
  constructor(     
    private route: ActivatedRoute,
    private gameService: GameService,)
    {}
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
    this.type = params['type'];
    this.name = params['name'];
   this.gameService.fetchGamesByDeveloper(this.type,this.name).subscribe(response => this.games = response);
    });
  }
  
}
