import { Component } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { catchError, of } from 'rxjs';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-after-login-steam',
  templateUrl: './after-login-steam.component.html',
  styleUrls: ['./after-login-steam.component.css'],
  providers: [ProfileService]
})
export class AfterLoginSteamComponent {
  error:any;
  isNotLoading = false;
  constructor(private route: ActivatedRoute, private profile: ProfileService) { }
  async ngOnInit() {

    this.route.queryParams.subscribe(params => {
      if (params['openid.assoc_handle'] == undefined)
        window.location.href = "/login";
      let object = {
        'openidassoc_handle': params['openid.assoc_handle'],
        'openidsigned': params['openid.signed'],
        'openidsig': params['openid.sig'],
        'openidns': 'http://specs.openid.net/auth/2.0',
        'openidmode': 'check_authentication',
        'openidop_endpoint': params['openid.op_endpoint'],
        'openidclaimed_id': params['openid.claimed_id'],
        'openididentity': params['openid.identity'],
        'openidreturn_to': params['openid.return_to'],
        'openidresponse_nonce': params['openid.response_nonce']
      };

      this.profile.getLoginWithSteam(object).pipe(
        catchError(error => {
          if (error.status === 400) {
            this.isNotLoading = true;
            this.error = 'Check your profile settings. Your profile is private or your game details private.';
          } 
          return of(null);
        })
      )
      .subscribe((response: any) => {  
        if(response != null){
          localStorage.setItem('userId',response.id)
          window.location.href = "/profile";
        }

      });
    });

  }
}
