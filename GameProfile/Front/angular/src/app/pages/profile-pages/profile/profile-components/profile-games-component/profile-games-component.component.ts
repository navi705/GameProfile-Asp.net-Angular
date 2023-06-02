import { Component } from '@angular/core';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-profile-games-component',
  templateUrl: './profile-games-component.component.html',
  styleUrls: ['./profile-games-component.component.css'],
  providers: [ProfileService]
})
export class ProfileGamesComponentComponent {
  constructor(private profileService: ProfileService
  ) {

  }
  ngOnInit(): void {

  }
}
