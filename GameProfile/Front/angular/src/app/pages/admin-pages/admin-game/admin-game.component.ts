import { Component } from '@angular/core';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-admin-game',
  templateUrl: './admin-game.component.html',
  styleUrls: ['./admin-game.component.css'],
  providers: [GameService]
})
export class AdminGameComponent {
  games:any;
  constructor(
    private gameService: GameService
  ){}
  ngOnInit(): void {
    this.gameService.fetchGamesBySort('titleAtoZ').subscribe(response => this.games = response);         
}
  public deleteGame(id:string):void{
    this.gameService.deleteGame(id).subscribe();
    window.location.reload();
  }
  
  public updateGame(game:any):void{
    let gameObject={
      title: game.title,
      releaseDate: game.releaseDate,
      headerImage: game.headerImage,
      nsfw: game.nsfw,
      description: game.description,
      screenshots: [
      ],
      genres: [
      ],
      publishers: [
      ],
      developers: [
      ],
      shopsLinkBuyGame: [
      ],
      achievementsCount: game.achievementsCount
    };
    this.gameService.updateGame(gameObject,game.id).subscribe();
    window.location.reload();
  }
}
