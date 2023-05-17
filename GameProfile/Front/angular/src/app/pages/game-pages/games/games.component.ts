import { Component, Inject} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import { Moment } from "moment";


import { GameService } from 'src/app/services/game.service';
import { SotrtFilter } from 'src/app/services/models/sortFilters';
import { TempSortFilters } from 'src/app/services/models/temSortFilters';

@Component({
    selector: 'games-catalog-page',
    templateUrl: './games.component.html',
    styleUrls: ['./games.component.css'],
    providers: [GameService,SotrtFilter,TempSortFilters]
  })
  export class GamesComponent{
    games:any;
    constructor(     
      private route: ActivatedRoute,
      private gameService: GameService,
      public sortFilters:SotrtFilter,
      public sortTemp:TempSortFilters,
    ){
      this.sortFilters.Sorting = 'titleAtoZ';
    }
  
     ngOnInit(): void {
        this.gameService.fetchGamesBySort('titleAtoZ').subscribe(response => this.games = response);        
    }
    public sort(){
     // this.gameService.fetchGamesBySort(this.selected).subscribe(response => this.games = response);        
    }
    public find(){
    this.sortFilters.ReleaseDateOf = this.sortTemp.dateOf?.format('MM-DD-YYYY');
    this.sortFilters.ReleaseDateTo = this.sortTemp.dateTo?.format('MM-DD-YYYY');
    this.gameService.fetchGamesBySortFilters(this.sortFilters).subscribe(response => this.games = response);
    }
  } 