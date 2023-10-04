import { Component} from '@angular/core';
import {Observable} from 'rxjs';
import { GameService } from 'src/app/services/game.service';
import { Game } from 'src/app/services/models/game';
import { of, catchError } from 'rxjs';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css'],
    providers: [GameService,ProfileService]
  })

  export class HeaderComponent{
    image:any;
    searchString:string;
    findGames: Observable<Game[]>;
    isMenuOpen:boolean = false;

    notificationCount: number = 0;

    notifyShow:Array<NotificationShow> = [];

    constructor(public gameService:GameService,public profileService:ProfileService){
      this.searchString = "";
      this.findGames = new Observable<Game[]>();
    }

    ngOnInit(): void {     
      //this.profileService.getAvatar().subscribe(response=> this.image = response);

      if(localStorage.getItem('auth') != 'false' ){

        this.profileService.getAvatar().pipe(
          catchError(error => {
            if (error.status === 401) {
              localStorage.setItem('auth', 'false');
            }
            return of(null);
          })
        ).subscribe(response => {
          if (response) {
          this.image = response;
          localStorage.setItem('auth', 'true');
          this.profileService.getNotification().subscribe(response => {this.notificationCount = response.length;
            response.forEach(r=>{
              let temp = r.stringFor.split(" ");

              if(temp[0] == 'ReplieGame'){
                let notShow: NotificationShow ={link:'game?id='+temp[1],content:'Someone replied to your comment in the games',name:r.stringFor};
                this.notifyShow.push(notShow);
              }

              if(temp[0] == 'RepliePost'){
                let notShow: NotificationShow ={link:'forum/'+temp[1],content:'Someone replied to your answer in the forum',name:r.stringFor};
                this.notifyShow.push(notShow);
              }

              if(temp[0] == 'MessagePost'){
                let notShow: NotificationShow ={link:'forum/'+temp[1],content:'Someone replied to your post in the forum',name:r.stringFor};
                this.notifyShow.push(notShow);
              }

            })
            console.log(this.notifyShow);
          }         
          );
          }
        });

      }

    }
    onSearchChange(): void {  
      if(this.searchString == ""){
        this.findGames = new Observable<Game[]>();
        return;
      }    
      this.gameService.fecthGameByString(this.searchString).subscribe(response=> this.findGames=of(response));
    }
    toggleMenu():void{
      this.isMenuOpen = !this.isMenuOpen;
    }
    deleteNotify(i:number){
      this.profileService.deleteNotification(this.notifyShow[i].name).subscribe();
    }

    
  }
  interface NotificationShow{
      link:string,
      content:string,
      name:string
  }