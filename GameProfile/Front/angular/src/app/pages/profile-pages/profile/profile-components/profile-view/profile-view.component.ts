import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProfileService } from 'src/app/services/profile.service';
import { GameList, ProfileModel } from 'src/app/services/models/profile';

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

  constructor(private route: ActivatedRoute,private profileService: ProfileService) { }
  
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

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    console.log(this.id)
  }

}
