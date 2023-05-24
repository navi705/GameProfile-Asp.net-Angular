import { Component } from '@angular/core';
import { GameForProfile } from 'src/app/services/models/game';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  providers: [ProfileService]
})
export class ProfileComponent {
  avatar:any;
  name:any;
  profileGames: GameForProfile[];
  constructor(private profile:ProfileService){
    this.profileGames = new Array<GameForProfile>();
  }
  ngOnInit(): void {   
    this.avatar = localStorage.getItem('avatar')?.replace('medium','full');
    this.name = localStorage.getItem('name');
    this.profile.profile().subscribe(response=> this.profileGames = response);
  }
  logout(){
    this.profile.logout().subscribe();
    window.location.href="/games";
  }
}
