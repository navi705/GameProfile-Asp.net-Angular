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
     
       ngOnInit(): void {     
      }
      public steamLogin(){  

        window.location.href='https://steamcommunity.com/openid/login?openid.ns=http://specs.openid.net/auth/2.0&openid.mode=checkid_setup&openid.return_to=http://localhost:4200/after-login-steam&openid.realm=http://localhost:4200&openid.identity=http://specs.openid.net/auth/2.0/identifier_select&openid.claimed_id=http://specs.openid.net/auth/2.0/identifier_select';
        
      }
  } 