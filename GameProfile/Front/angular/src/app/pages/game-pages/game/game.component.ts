import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


import { GameService } from 'src/app/services/game.service';
import { ProfileService } from 'src/app/services/profile.service';
import { StatusGameProgressions } from 'src/app/services/models/game';



@Component({
  selector: 'game-page',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
  providers: [GameService]
})

export class GameComponent {
  id: any;
  game: any;
  hours:number = 0;
  hoursBefore:number=-1;
  status = StatusGameProgressions.NONE;
  statusBefore = StatusGameProgressions.NONE;

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private profileSerivce: ProfileService
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.id = params['id'];
      this.gameService.fetchGame(this.id).subscribe(response => this.game = response);
    });
    if(localStorage.getItem("auth") === "true"){
      this.profileSerivce.getGameProfile(this.id).subscribe(response=>{ this.hours= response.hours; this.status = response.statusGame; 
        this.hoursBefore = response.hours; this.statusBefore = response.statusGame});
    }
  }
  averageScore(): number {
    if (!this.game?.reviews || this.game?.reviews.length === 0) {
      return 0;
    }
  
    const scores = this.game?.reviews.map((review: { site: number, score: number }) => review.score);
    const sum = scores.reduce((acc: number, score: number) => acc + score, 0);
    return sum / scores.length;
  }

  isAuthenticated(): boolean {
    const authValue = localStorage.getItem('auth');
   if(authValue === 'true'){
    return true
   }
    return false;
  }
  
  addGameToProfile(){
    if(this.hours != this.hoursBefore || this.status != this.statusBefore){
      this.profileSerivce.updateGame(this.id,this.hours,this.status).subscribe();
      return;
    }
    if(this.status == StatusGameProgressions.NONE){
      return;
    }
    this.profileSerivce.addGame(this.id,this.status,this.hours).subscribe();
  }

} 

