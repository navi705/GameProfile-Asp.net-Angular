import { Component} from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { ProfileService } from 'src/app/services/profile.service';
import { GlobalVariable } from '../../../global';

@Component({
    selector: 'game-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    providers: [ProfileService,Router]
  })
  export class LoginComponent{
    urlSteam:string = `https://steamcommunity.com/openid/login?openid.ns=http://specs.openid.net/auth/2.0&openid.mode=checkid_setup&openid.return_to=${GlobalVariable.BASE_FRONT_URL}after-login-steam&openid.realm=${GlobalVariable.BASE_FRONT_URL}&openid.identity=http://specs.openid.net/auth/2.0/identifier_select&openid.claimed_id=http://specs.openid.net/auth/2.0/identifier_select`;
    constructor(
        private route: ActivatedRoute,
        private profileService: ProfileService
      ){
      }
      
  } 