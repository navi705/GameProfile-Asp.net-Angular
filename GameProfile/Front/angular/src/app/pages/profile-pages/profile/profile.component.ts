import { Component } from '@angular/core';
import { GameList, ProfileModel } from 'src/app/services/models/profile';
import { ProfileService } from 'src/app/services/profile.service';
import { of, catchError } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  providers: [ProfileService]
})
export class ProfileComponent {
  profile: ProfileModel = {} as ProfileModel;
  gamesCount:number = 0;
  selectedState: string = 'all';
  sort:string = "hoursDesc";

  selectState(state: string) {
    this.selectedState = state;
    if(this.selectedState == 'all'){
      this.profileService.profile("",this.sort).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
    if(this.selectedState == 'playing'){
      this.profileService.profile("1",this.sort).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
    if(this.selectedState == 'completed'){
      this.profileService.profile("2",this.sort).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
    if(this.selectedState == 'dropped'){
      this.profileService.profile("3",this.sort).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
    if(this.selectedState == 'planned'){
      this.profileService.profile("4",this.sort).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
  }

  constructor(private profileService: ProfileService) {
  }
  ngOnInit(): void {
    // this.profileService.profile("",this.sort).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; if (response) {
    //   localStorage.setItem('auth', 'true');
    //   }
    //   else
    //   {localStorage.setItem('auth','false');
    //     window.location.href = '/';}
    // });
    this.profileService.profile("",this.sort).pipe(
      catchError(error => {
        if (error.status === 401 || error.status === 404) {
          localStorage.setItem('auth', 'false');
          window.location.href = '/';}
        return of(null);
      })
    ).subscribe(response => {  if (response) { this.profile = response;this.gamesCount = this.profile.gameList.length;
      localStorage.setItem('auth', 'true');}});


  }
  public updateGame(game: GameList) {
    this.profileService.updateGame(game.id, game.hours, game.statusGame).subscribe();
  }
  public deleteGame(game: GameList) {
    this.profileService.deleteGame(game.id).subscribe();
  }
  logout() {
    this.profileService.logout().subscribe(response=>window.location.href = "/games");
  } 
}

