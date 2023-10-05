import { Component } from '@angular/core';
import { GameList, ProfileModel } from 'src/app/services/models/profile';
import { ProfileService } from 'src/app/services/profile.service';
import { of, catchError } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GlobalVariable } from 'src/app/global';

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
  verification:string = "yes";
  urlSteam:string = `https://steamcommunity.com/openid/login?openid.ns=http://specs.openid.net/auth/2.0&openid.mode=checkid_setup&openid.return_to=${GlobalVariable.BASE_FRONT_URL}add-steam-account&openid.realm=${GlobalVariable.BASE_FRONT_URL}&openid.identity=http://specs.openid.net/auth/2.0/identifier_select&openid.claimed_id=http://specs.openid.net/auth/2.0/identifier_select`;
  steamIds:string[] = [];

  selectState(state: string) {
    this.selectedState = state;
    if(this.selectedState == 'all'){
      this.profileService.profile("",this.sort,this.verification).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
    if(this.selectedState == 'playing'){
      this.profileService.profile("1",this.sort,this.verification).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
    if(this.selectedState == 'completed'){
      this.profileService.profile("2",this.sort,this.verification).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
    if(this.selectedState == 'dropped'){
      this.profileService.profile("3",this.sort,this.verification).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
    if(this.selectedState == 'planned'){
      this.profileService.profile("4",this.sort,this.verification).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
  }

  constructor(private profileService: ProfileService, private snackBar: MatSnackBar) {}
  ngOnInit(): void {
    this.profileService.profile("",this.sort,this.verification).pipe(
      catchError(error => {
        if (error.status === 401 || error.status === 404) {
          localStorage.setItem('auth', 'false');
          window.location.href = '/';}
        return of(null);
      })
    ).subscribe(response => {  if (response) { this.profile = response;this.gamesCount = this.profile.gameList.length;
      localStorage.setItem('auth', 'true');}});

      this.profileService.getSteanAccounts().subscribe(response => {this.steamIds = response;});

  }
  public updateGame(game: GameList) {
    this.profileService.updateGame(game.id, game.hours, game.statusGame).subscribe();
  }
  public deleteGame(game: GameList) {
    this.profileService.deleteGame(game.id).subscribe();
  }

  deleteGameConfirm(game: GameList){
    let snackBarRef = this.snackBar.open('Are you sure you want to delete', 'Confirm');

    snackBarRef.onAction().subscribe(() => {
      this.deleteGame(game);
      this.profile.gameList = this.profile.gameList.filter(item => item.id !== game.id);
    });
  }

  updateVereficated(){
    this.profileService.updateVereficatedHours().subscribe();
  }


  logout() {
    this.profileService.logout().subscribe(response=>window.location.href = "/games");
  } 
}

