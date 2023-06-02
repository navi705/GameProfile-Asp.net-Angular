import { Component } from '@angular/core';
import { GameList, ProfileModel } from 'src/app/services/models/profile';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  providers: [ProfileService]
})
export class ProfileComponent {
  profile: ProfileModel = {} as ProfileModel;
  profileSort: ProfileModel = {} as ProfileModel;
  allTime: number = 0;
  selectedState: string = 'all';

  selectState(state: string) {
    this.selectedState = state;
    if(this.selectedState == 'all'){
      this.profileSort =  JSON.parse(JSON.stringify(this.profile));
      console.log(this.profile);
    }
    if(this.selectedState == 'playing'){
      this.profileSort =  JSON.parse(JSON.stringify(this.profile));
      this.profileSort.gameList= this.profileSort.gameList.filter(game => game.statusGame === 1);
      console.log(this.profile);
    }
    if(this.selectedState == 'completed'){
      this.profileSort.gameList = this.profile.gameList;
      this.profileSort.gameList.filter(game => game.statusGame === 2);
    }
    if(this.selectedState == 'dropped'){
      this.profileSort.gameList = this.profile.gameList;
      this.profileSort.gameList.filter(game => game.statusGame === 3);
    }
    if(this.selectedState == 'planned'){
      this.profileSort.gameList = this.profile.gameList;
      this.profileSort.gameList.filter(game => game.statusGame === 4);
    }
  }

  constructor(private profileService: ProfileService) {
  }
  ngOnInit(): void {
    this.profileService.profile().subscribe(response => { this.profile = response; this.profileSort = response; this.allTimeGet(); });

  }
  public allTimeGet() {
    this.profile.gameList.forEach(game => { this.allTime += game.hours; });
  }
  public updateGame(game: GameList) {
    this.profileService.updateGame(game.id, game.hours, game.statusGame).subscribe();
  }
  public deleteGame(game: GameList) {
    this.profileService.deleteGame(game.id).subscribe();
  }
  logout() {
    this.profileService.logout().subscribe();
    window.location.href = "/games";
  } 
}

