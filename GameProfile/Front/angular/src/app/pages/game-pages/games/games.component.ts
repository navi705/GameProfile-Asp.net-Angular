import { Component, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameClass, StatusGameProgressions } from 'src/app/services/models/game';

import { GameService } from 'src/app/services/game.service';
import { SotrtFilter } from 'src/app/services/models/sortFilters';
import { EnumsCheckBox, GenresCheckBox, TempSortFilters } from 'src/app/services/models/temSortFilters';
import { FilterService } from 'src/app/services/filterService';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'games-catalog-page',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css'],
  providers: [GameService, SotrtFilter, TempSortFilters, GameClass]
})
export class GamesComponent {
  games: any[];
  showFilters: boolean = false;
  indeterminate: boolean;
  lenghtArray: number;
  filtersArray: GenresCheckBox[];
  tagsArray: GenresCheckBox[] = new Array<GenresCheckBox>();
  gameStatus = StatusGameProgressions;
  gamesStatusArray: EnumsCheckBox[];

  constructor(
    private route: ActivatedRoute,
    private _snackBar: MatSnackBar,
    private gameService: GameService,
    public sortFilters: SotrtFilter,
    public sortTemp: TempSortFilters,
    public filterService: FilterService,
  ) {
    this.sortFilters.Sorting = 'titleAtoZ';
    this.sortFilters.Page = 0;
    this.games = new Array<any>();
    this.lenghtArray = 0;
    this.indeterminate = false;
    this.filtersArray = new Array<GenresCheckBox>();
    this.gamesStatusArray = new Array<EnumsCheckBox>(new EnumsCheckBox(1,0),new EnumsCheckBox(2,0),new EnumsCheckBox(3,0),new EnumsCheckBox(4,0));
  }
  @HostListener("window:scroll", [])
  onScroll(): void {
    if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight - 100) {
      if (this.lenghtArray == this.games.length) {
        return;
      }
      this.lenghtArray = this.games.length;
      this.sortFilters.Page!++;
      this.gameService.fetchGamesBySortFilters(this.sortFilters).subscribe(response => response.forEach(element => this.games.push(element)));
    }
  }
  ngOnInit(): void {
    this.gameService.fetchGames(this.sortFilters).subscribe(response => this.games = response);
    this.filterService.fetchGenres().subscribe(response => response.forEach(x => this.filtersArray.push(new GenresCheckBox(x, 0))));
    this.filterService.fetchTags().subscribe(response => response.forEach(x => this.tagsArray.push(new GenresCheckBox(x, 0))));
  }

  public find() {
    this.sortFilters.Page = 0;
    this.sortFilters.ReleaseDateOf = this.sortTemp.dateOf?.format('MM-DD-YYYY');
    this.sortFilters.ReleaseDateTo = this.sortTemp.dateTo?.format('MM-DD-YYYY');
    this.sortFilters.GenresExcluding = undefined;
    this.sortFilters.Genres = undefined;
    this.sortFilters.Tags = undefined;
    this.sortFilters.TagsExcluding = undefined;
    this.sortFilters.StatusGameProgressions = undefined;
    this.sortFilters.StatusGameProgressionsExcluding = undefined;

    if (this.sortFilters.RateOf != undefined && this.sortFilters.RateTo) {
      if (this.sortFilters.RateOf > this.sortFilters.RateTo) {
        const tempRate = this.sortFilters.RateOf;
        this.sortFilters.RateOf = this.sortFilters.RateTo;
        this.sortFilters.RateTo = tempRate;
      }
    }

    this.filtersArray.forEach((item) => {
      if (item.click === 1) {
        if (item.name != undefined) {
          if (this.sortFilters.Genres == undefined)
            this.sortFilters.Genres = new Array<string>();
          this.sortFilters.Genres?.push(item.name);

        }
      }
      if (item.click === 2) {
        if (item.name != undefined) {
          if (this.sortFilters.GenresExcluding == undefined)
            this.sortFilters.GenresExcluding = new Array<string>();
          this.sortFilters.GenresExcluding?.push(item.name);
        }
      }
    });

    this.tagsArray.forEach((item) => {
      if (item.click === 1) {
        if (item.name != undefined) {
          if (this.sortFilters.Tags == undefined)
            this.sortFilters.Tags = new Array<string>();
          this.sortFilters.Tags?.push(item.name);

        }
      }
      if (item.click === 2) {
        if (item.name != undefined) {
          if (this.sortFilters.TagsExcluding == undefined)
            this.sortFilters.TagsExcluding = new Array<string>();
          this.sortFilters.TagsExcluding?.push(item.name);
        }
      }
    });

    this.gamesStatusArray.forEach((item) => {
      if (item.click === 1) {
        if (item.number != undefined) {
          if (this.sortFilters.StatusGameProgressions == undefined)
            this.sortFilters.StatusGameProgressions = new Array<StatusGameProgressions>();
          this.sortFilters.StatusGameProgressions?.push(item.number);

        }
      }
      if (item.click === 2) {
        if (item.number != undefined) {
          if (this.sortFilters.StatusGameProgressionsExcluding == undefined)
            this.sortFilters.StatusGameProgressionsExcluding = new Array<StatusGameProgressions>();
          this.sortFilters.StatusGameProgressionsExcluding?.push(item.number);
        }
      }
    });

    this.gameService.fetchGamesBySortFilters(this.sortFilters).subscribe(response => this.games = response);
  }

  public changeCheckbox1(): void {
    this.sortTemp.nsfw++;
    if (this.sortTemp.nsfw == 3) {
      this.sortFilters.Nsfw = undefined;
      this.sortTemp.nsfw = 0;
      return;
    }
    if (this.sortTemp.nsfw == 1) {
      this.sortFilters.Nsfw = "yes";
      return;
    }
    if (this.sortTemp.nsfw == 2) {
      this.sortFilters.Nsfw = "no";
      this.indeterminate = true;
      return;
    }
  }

  onChange(event: any) {
    let item = this.filtersArray.find((genre) => genre.name === event.source.name);
    if (item?.click != null) {
      item.click++;
    }
    if (item?.click === 3) {
      event.source.checked = false;
      item.click = 0;
    }
    if (item?.click === 2) {
      event.source.indeterminate = true;
    }
    if (item?.click === 1) {
      event.source.checked = true;
    }
  }

  onChangeTags(event: any) {
    let item = this.tagsArray.find((genre) => genre.name === event.source.name);
    if (item?.click != null) {
      item.click++;
    }
    if (item?.click === 3) {
      event.source.checked = false;
      item.click = 0;
    }
    if (item?.click === 2) {
      event.source.indeterminate = true;
    }
    if (item?.click === 1) {
      event.source.checked = true;
    }
  }

  onChangeStatusProgressions(event: any){
    if(localStorage.getItem('auth') == 'false' || localStorage.getItem('auth') == undefined){
      event.source.checked = false;
      this._snackBar.open('You must be logged in before selecting status','OK', {
        panelClass: ['error-snackbar'],duration: 10000,
      });
      return;
    }
    let item = this.gamesStatusArray.find((genre) => genre.number == event.source.name);

    if (item?.click != null) {
      item.click++;
    }
    if (item?.click === 3) {
      event.source.checked = false;
      item.click = 0;
    }
    if (item?.click === 2) {
      event.source.indeterminate = true;
    }
    if (item?.click === 1) {
      event.source.checked = true;
    }
  }

  toggleFilters() {
    this.showFilters = !this.showFilters;
  }
} 