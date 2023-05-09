import { Component } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-after-login-steam',
  templateUrl: './after-login-steam.component.html',
  styleUrls: ['./after-login-steam.component.css'],
  providers: [ProfileService]
})
export class AfterLoginSteamComponent {
  constructor(private route: ActivatedRoute,private profile:ProfileService) {}
  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      localStorage.setItem('openid.assoc_handle',params['openid.assoc_handle']);
      localStorage.setItem('openid.signed',params['openid.signed' ]);
      localStorage.setItem('openid.sig',params['openid.sig']);
      let object = {
        'openidassoc_handle': params['openid.assoc_handle'],
        'openidsigned': params['openid.signed' ],
        'openidsig': params['openid.sig'],
        'openidns': 'http://specs.openid.net/auth/2.0',
        'openidmode': 'check_authentication',
        'openidop_endpoint': params['openid.op_endpoint'],
        'openidclaimed_id': params['openid.claimed_id'],
        'openididentity':params['openid.identity'],
        'openidreturn_to': params['openid.return_to'],
        'openidresponse_nonce': params['openid.response_nonce']
        };
      //console.log(this.profile.checkSteamLogin(object));
      //window.location.href= this.profile.checkSteamLogin(object);
      this.profile.getLoginWithSteam(object).subscribe((response:any) => {
        localStorage.setItem('id',response['id']);
        localStorage.setItem('avatar',response['avatar']);
      });

    });
 }
}
