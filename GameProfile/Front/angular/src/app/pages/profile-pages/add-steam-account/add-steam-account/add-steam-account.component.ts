import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { catchError, of } from 'rxjs';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-add-steam-account',
  templateUrl: './add-steam-account.component.html',
  styleUrls: ['./add-steam-account.component.css']
})
export class AddSteamAccountComponent {
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

    this.profile.addSteamAccount(object).pipe(
      catchError(error => {
        if (error.status === 400) {
          this.isNotLoading = true;
          this.error = 'Check your profile settings. Your profile is private or your game details private. Or is steam account already added.';
        } 
        return of(null);
      })
    )
    .subscribe((response: any) => {  
        window.location.href = "/profile";

    });
    
  });

  }
  
}
