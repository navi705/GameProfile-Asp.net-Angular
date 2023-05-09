import { Injectable} from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { GlobalVariable } from '../global';


@Injectable({
    providedIn: 'root',

  })
  export class ProfileService{
    constructor(private http: HttpClient) { }
   
    public steamLoginIn(){
        const params = new HttpParams()
        .set('openid.ns', 'http://specs.openid.net/auth/2.0')
        .set('openid.mode', 'checkid_setup')
        .set('openid.return_to','http://localhost:4200/games')
        .set('openid.realm','http://localhost:4200')
        .set('openid.identity','http://specs.openid.net/auth/2.0/identifier_select')
        .set('openid.claimed_id','http://specs.openid.net/auth/2.0/identifier_select');
        return'https://steamcommunity.com/openid/login?' + params;   
    }
    public checkSteamLogin(objects:any){
      const params = new HttpParams()
      .set('openid.assoc_handle',objects['openid.assoc_handle'])
      .set('openid.signed',objects['openid.signed'])
      .set('openid.sig',objects['openid.sig'])
      .set('openid.ns',objects['openid.ns'])
      .set('openid.mode',objects['openid.mode'])
      .set('openid.op_endpoint',objects['openid.op_endpoint'])
      .set('openid.claimed_id',objects['openid.claimed_id'])
      .set('openid.identity',objects['openid.identity'])
      .set('openid.return_to',objects['openid.return_to'])
      .set('openid.response_nonce',objects['openid.response_nonce']);
         //return this.http.get('https://steamcommunity.com/openid/login?' + params);
         return 'https://steamcommunity.com/openid/login?' + params;
    }
    public getLoginWithSteam(objects:any){
        return this.http.post(GlobalVariable.BASE_API_URL +'login/steam',objects);
    }
    public logout(){
        console.log(GlobalVariable.BASE_API_URL + 'logout');
        return this.http.post(GlobalVariable.BASE_API_URL + 'logout',{});
    }

  }