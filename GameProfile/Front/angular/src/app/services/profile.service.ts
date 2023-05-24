import { Injectable} from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { GlobalVariable } from '../global';
import { GameForProfile } from './models/game';


@Injectable({
    providedIn: 'root',

  })
  export class ProfileService{
    constructor(private http: HttpClient) { }
   
    public steamLoginIn(){
        const params = new HttpParams()
        .set('openid.ns', 'http://specs.openid.net/auth/2.0')
        .set('openid.mode', 'checkid_setup')
        .set('openid.return_to','https://localhost:4200/games')
        .set('openid.realm','https://localhost:4200')
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
      const httpOptions = {
        withCredentials: true //this is required so that Angular returns the Cookies received from the server. The server sends cookies in Set-Cookie header. Without this, Angular will ignore the Set-Cookie header
      };
        return this.http.post(GlobalVariable.BASE_API_URL +'login/steam',objects,httpOptions);
    }
    public logout(){
      const httpOptions = {
        withCredentials: true //this is required so that Angular returns the Cookies received from the server. The server sends cookies in Set-Cookie header. Without this, Angular will ignore the Set-Cookie header
      };
        console.log(GlobalVariable.BASE_API_URL + 'logout');
        return this.http.post(GlobalVariable.BASE_API_URL + 'logout',{},httpOptions);
    }
    public profile(){
      const httpOptions = {
        withCredentials: true //this is required so that Angular returns the Cookies received from the server. The server sends cookies in Set-Cookie header. Without this, Angular will ignore the Set-Cookie header
      };
      return this.http.get<Array<GameForProfile>>(GlobalVariable.BASE_API_URL + 'profile',httpOptions);
    }
  }