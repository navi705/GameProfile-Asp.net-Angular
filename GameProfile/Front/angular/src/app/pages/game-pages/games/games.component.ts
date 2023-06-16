import { Component, HostListener} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import { GameClass } from 'src/app/services/models/game';

import { GameService } from 'src/app/services/game.service';
import { SotrtFilter } from 'src/app/services/models/sortFilters';
import { GenresCheckBox, TempSortFilters } from 'src/app/services/models/temSortFilters';
import { FilterService } from 'src/app/services/filterService';

@Component({
    selector: 'games-catalog-page',
    templateUrl: './games.component.html',
    styleUrls: ['./games.component.css'],
    providers: [GameService,SotrtFilter,TempSortFilters,GameClass]
  })
  export class GamesComponent{
    games:any[];
    indeterminate:boolean;
    lenghtArray:number;
    filtersArray:GenresCheckBox[];
    constructor(     
      private route: ActivatedRoute,
      private gameService: GameService,
      public sortFilters:SotrtFilter,
      public sortTemp:TempSortFilters,
      public filterService:FilterService,
    ){
      this.sortFilters.Sorting = 'titleAtoZ';
      this.sortFilters.Page = 0;
      this.games = new Array<any>();
      this.lenghtArray =0;
      this.indeterminate = false;
      this.filtersArray = new Array<GenresCheckBox>();
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
     ngOnInit(): void {
        this.gameService.fetchGames(this.sortFilters).subscribe(response => this.games = response);     
        this.filterService.fetchGenres().subscribe(response=>response.forEach(x=> this.filtersArray.push(new GenresCheckBox(x,0))));
    }

    public find(){
    this.sortFilters.Page = 0;
    this.sortFilters.ReleaseDateOf = this.sortTemp.dateOf?.format('MM-DD-YYYY');
    this.sortFilters.ReleaseDateTo = this.sortTemp.dateTo?.format('MM-DD-YYYY');
      this.sortFilters.GenresExcluding = undefined;
      this.sortFilters.Genres = undefined;

      this.filtersArray.forEach((item) => {
        if (item.click === 1) {
          if(item.name != undefined){
            if(this.sortFilters.Genres == undefined)
            this.sortFilters.Genres = new Array<string>();
          this.sortFilters.Genres?.push(item.name);

          }
        }
        if (item.click === 2) {
          if(item.name != undefined){
            if(this.sortFilters.GenresExcluding == undefined)
            this.sortFilters.GenresExcluding = new Array<string>();
          this.sortFilters.GenresExcluding?.push(item.name);
          }   
        }
      });
      console.log(this.sortFilters)
    this.gameService.fetchGamesBySortFilters(this.sortFilters).subscribe(response => this.games = response);
    }

    public changeCheckbox1():void{
      console.log(this.filtersArray);
      this.sortTemp.nsfw++;
      if(this.sortTemp.nsfw ==3){
        this.sortFilters.Nsfw = undefined;
        this.sortTemp.nsfw = 0;
        return;
      }
      if(this.sortTemp.nsfw ==1){
        this.sortFilters.Nsfw = "yes";
        return;
      }
      if(this.sortTemp.nsfw  == 2){
        this.sortFilters.Nsfw = "no";
        this.indeterminate = true;
        return;
      }     
    }
    onChange(event: any) {
      let item = this.filtersArray.find((genre) => genre.name === event.source.name);
      console.log(this.filtersArray);
      if (item?.click != null) {
        item.click++;
      }
      console.log(item);
      if(item?.click === 3){
        event.source.checked = false;
        item.click =0;
      }
      if(item?.click === 2){
        event.source.indeterminate = true;
      }
      if(item?.click === 1){
        event.source.checked = true;
      }
    }
  } 