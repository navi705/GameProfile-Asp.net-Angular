import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


import { GameService } from 'src/app/services/game.service';



@Component({
  selector: 'game-page',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
  providers: [GameService]
})

export class GameComponent {
  id: any;
  game: any;
  constructor(
    private route: ActivatedRoute,
    private gameService: GameService
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.id = params['id'];
      this.gameService.fetchGame(this.id).subscribe(response => this.game = response);
    });
  }
  averageScore(): number {
    if (!this.game?.reviews || this.game?.reviews.length === 0) {
      return 0;
    }
  
    const scores = this.game?.reviews.map((review: { site: number, score: number }) => review.score);
    const sum = scores.reduce((acc: number, score: number) => acc + score, 0);
    return sum / scores.length;
  }

} 