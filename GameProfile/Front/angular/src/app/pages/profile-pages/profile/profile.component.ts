import { Component } from '@angular/core';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  providers: [ProfileService]
})
export class ProfileComponent {
  constructor(private profile:ProfileService){}
  logout(){
    localStorage.clear();
    window.location.href="/games";
    //this.profile.logout().subscribe();
    //window.location.href='games';
    // использовать location
  }
}
