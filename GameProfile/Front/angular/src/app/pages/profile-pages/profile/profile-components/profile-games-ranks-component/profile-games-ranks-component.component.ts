import { Component } from '@angular/core';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-profile-games-ranks-component',
  templateUrl: './profile-games-ranks-component.component.html',
  styleUrls: ['./profile-games-ranks-component.component.css']
})
export class ProfileGamesRanksComponentComponent {
  ranks:any;
  constructor(private profileService: ProfileService) {}

  ngOnInit(): void {
    this.profileService.getRanks(localStorage.getItem('userId')?.toString()).subscribe(response=>this.ranks = response);
  }
}
