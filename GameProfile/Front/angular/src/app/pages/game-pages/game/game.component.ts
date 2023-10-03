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

  rating:number = 0;

  comments: any[] = [];
  comment: string = '';

  editCommentNumber:number = -1;
  editCommentContent:string = '';

  addReplieNumber:number = -1;
  addReplieContent:string = '';

  editReplieNumber:number = -1;
  editReplieContent:string = '';

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private profileSerivce: ProfileService
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.id = params['id'];
      this.gameService.fetchGame(this.id).subscribe(response => this.game = response);
      this.gameService.getGameComments(this.id).subscribe(response => this.comments = response);
    });
    if(localStorage.getItem("auth") === "true"){
      this.profileSerivce.getGameProfile(this.id).subscribe(response=>{ this.hours= response.hours; this.status = response.statusGame; 
        this.hoursBefore = response.hours; this.statusBefore = response.statusGame});
        this.gameService.getRatingForUser(this.id).subscribe(response=>{this.rating = response;});
    }
  }
  averageScore(): number {
    if (!this.game?.reviews || this.game?.reviews.length === 0) {
      return 0;
    }
  
    const scores = this.game?.reviews.map((review: { site: number, score: number }) => review.score).filter((score: number) => score !== 0);
  
    if (scores.length === 0) {
      return 0;
    }
  
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

  addRating(){
    this.gameService.putRatingForUser(this.id,this.rating).subscribe();
  }

  sendComment(): void {
    if(this.comment == ''){
      return;
    }
    this.gameService.putComment(this.id,this.comment).subscribe(x=>{window.location.reload()});
  }

  checkCanControlComment(idAuthorMessage:string):boolean{
    if (localStorage.getItem('userId') == idAuthorMessage) {
      return true;
    }
    return false;
  }

  deleteComment(commnetId:string){
    this.gameService.deleteComment(commnetId).subscribe(x=>{window.location.reload()});
  }

  updateComment(commentId:string,content:string){
    this.gameService.updateComment(commentId,content).subscribe(x=>{window.location.reload()});

  }

  editComment(i:number):void{
    this.editCommentNumber= i;
    if(i>-1){
      this.editCommentContent = this.comments[i].comment;
    }
  }

  addReplieF(i:number,content:string):void{
    this.addReplieNumber = i;
    if(i>-1){
      this.addReplieContent = content;
    }
  }

  sendReplie(messagePostID:string,content:string):void{
    this.gameService.putReplies(messagePostID,content).subscribe(x => { window.location.reload() });
  }

  deleteReplie(Id:string){
    this.gameService.deleteReplies(Id).subscribe(x=>{window.location.reload()});
  }

  updateReplie(id:string,content:string){
    this.gameService.updateReplies(id,content).subscribe(x=>{window.location.reload()});
  }

  editReplieF(i:number,content:string):void{
    this.editReplieNumber= i;
    if(i>-1){
      this.editReplieContent = content;
    }
  }

} 

