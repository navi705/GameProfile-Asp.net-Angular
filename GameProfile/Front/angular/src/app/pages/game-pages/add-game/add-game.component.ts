import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { of, catchError } from 'rxjs';
import { GameService } from 'src/app/services/game.service';
import { Game, GamePut } from 'src/app/services/models/game';

@Component({
  selector: 'game-add',
  templateUrl: './add-game.component.html',
  styleUrls: ['./add-game.component.css'],
  providers: [GameService]
})
export class GameAddComponent {
  isLoading: boolean = false;

  appId: number = 0;

  name: any;
  date: any;
  image: any;
  nsfw: any;
  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {

  }

  addGameFromSteam(){
    this.isLoading = true;
    this.gameService.putGameFromSteam(this.appId).pipe(
      catchError(error => {
        this.isLoading = false;
        if (error.status === 400) {
          this.snackBar.open(error.error);
        }
        return of();
      })).subscribe(response=>{
        this.isLoading = false;
        location.href="game?id="+response;
      });
    
  }

  createGame() {
    let game: GamePut = {
      title: this.name, releaseDate: this.date, headerImage: this.image, nsfw: false,
      description: '', screenshots: [], genres: [], publishers: [], developers: [], shopsLinkBuyGame: [], achievementsCount: 0
    };
    console.log(game);
    //this.gameService.putGame(game).subscribe();
  }
}