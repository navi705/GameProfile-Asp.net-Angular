import { Component } from '@angular/core';
import { ProfileModel } from 'src/app/services/models/profile';
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
  sort:string = "hoursDesc";
  verification:string = "yes";
  selectedStateMenu: string = "Games";

  constructor(private profileService: ProfileService) {}
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

  }

  selectMenu(menu:string){
    this.selectedStateMenu = menu;

  }

  logout() {
    this.profileService.logout().subscribe(response=>window.location.href = "/games");
  } 

}

