import { Component} from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
    selector: 'game-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    providers: [ProfileService,Router]
  })
  export class LoginComponent{
    constructor(
        private route: ActivatedRoute,
        private profileService: ProfileService
      ){
      }
      
  } 