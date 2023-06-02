import { Component} from '@angular/core';
import {Observable} from 'rxjs';
import { GameService } from 'src/app/services/game.service';
import { Game } from 'src/app/services/models/game';
import { of } from 'rxjs';
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

    constructor(public gameService:GameService,public profileService:ProfileService){
      this.searchString = "";
      this.findGames = new Observable<Game[]>();
    }

    ngOnInit(): void {     
      this.profileService.getAvatar().subscribe(response=> this.image = response);
    }
    onSearchChange(): void {  
      if(this.searchString == ""){
        this.findGames = new Observable<Game[]>();
        return;
      }    
      this.gameService.fecthGameByString(this.searchString).subscribe(response=> this.findGames=of(response));
    }
  }
