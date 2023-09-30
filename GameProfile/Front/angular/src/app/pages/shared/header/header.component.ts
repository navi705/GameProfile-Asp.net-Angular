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
  }
