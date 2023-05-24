import { Component,HostListener} from '@angular/core';
import { GameService } from 'src/app/services/game.service';
import { SotrtFilter } from 'src/app/services/models/sortFilters';

@Component({
  selector: 'app-admin-game',
  templateUrl: './admin-game.component.html',
  styleUrls: ['./admin-game.component.css'],
  providers: [GameService,SotrtFilter]
})
export class AdminGameComponent {
  games:any[];
  lenghtArray:number;
  constructor(
    private gameService: GameService,
    public sortFilters:SotrtFilter,
  ){
    this.sortFilters.Sorting = 'titleAtoZ';
      this.sortFilters.Page = 0;
      this.games = new Array<any>();
       this.lenghtArray =0;
  }
  ngOnInit(): void {    
    this.gameService.fetchGamesBySortFilters(this.sortFilters).subscribe(response => response.forEach(element =>this.games.push(element)));  
}
@HostListener("window:scroll", [])
onScroll(): void {
if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight-100) {
  if(this.lenghtArray == this.games.length){
    return;
  }
  this.lenghtArray = this.games.length;
  this.sortFilters.Page!++;
  this.gameService.fetchGamesBySortFilters(this.sortFilters).subscribe(response => response.forEach(element =>this.games.push(element)));
}
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
