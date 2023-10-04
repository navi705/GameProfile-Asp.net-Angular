import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProfileService } from 'src/app/services/profile.service';
import { GameList, ProfileModel } from 'src/app/services/models/profile';
import { filter } from 'rxjs';
import { StatusGameProgressions } from 'src/app/services/models/game';

@Component({
  selector: 'app-profile-view',
  templateUrl: './profile-view.component.html',
  styleUrls: ['./profile-view.component.css']
})
export class ProfileViewComponent {
  id: any;
  profile: ProfileModel = {} as ProfileModel;
  gamesCount:number = 0;
  selectedState: string = 'all';
  sort:string = "hoursDesc";
  gameStatus = StatusGameProgressions;
  verefication:string = "yes";

  constructor(private route: ActivatedRoute,private profileService: ProfileService) { }
  
  selectState(state: string) {
    this.selectedState = state;
    if(this.selectedState == 'all'){
      this.profileService.profileId(this.id,"",this.sort,this.verefication).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
    if(this.selectedState == 'playing'){
      this.profileService.profileId(this.id,"1",this.sort,this.verefication).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
    if(this.selectedState == 'completed'){
      this.profileService.profileId(this.id,"2",this.sort,this.verefication).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
    if(this.selectedState == 'dropped'){
      this.profileService.profileId(this.id,"3",this.sort,this.verefication).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
    if(this.selectedState == 'planned'){
      this.profileService.profileId(this.id,"4",this.sort,this.verefication).subscribe(response => { this.profile = response;this.gamesCount = this.profile.gameList.length; });
    }
  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.profileService.profileId(this.id,this.selectedState,this.sort,this.verefication).subscribe(response => {this.profile = response; this.gamesCount = this.profile.gameList.length;});
  }

}
