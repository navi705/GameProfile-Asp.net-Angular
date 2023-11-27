import { Component } from '@angular/core';
import { ProfileService } from 'src/app/services/profile.service';
import { GlobalVariable } from 'src/app/global';

@Component({
  selector: 'app-profile-settings-component',
  templateUrl: './profile-settings-component.component.html',
  styleUrls: ['./profile-settings-component.component.css']
})
export class ProfileSettingsComponentComponent {
  urlSteam: string = `https://steamcommunity.com/openid/login?openid.ns=http://specs.openid.net/auth/2.0&openid.mode=checkid_setup&openid.return_to=${GlobalVariable.BASE_FRONT_URL}add-steam-account&openid.realm=${GlobalVariable.BASE_FRONT_URL}&openid.identity=http://specs.openid.net/auth/2.0/identifier_select&openid.claimed_id=http://specs.openid.net/auth/2.0/identifier_select`;
  steamIds: string[] = [];


  constructor(private profileService: ProfileService) {

  }
  ngOnInit(): void {
    this.profileService.getSteanAccounts().subscribe(response => { this.steamIds = response; });
  }

  updateVereficated() {
    this.profileService.updateVereficatedHours().subscribe();
  }
}
