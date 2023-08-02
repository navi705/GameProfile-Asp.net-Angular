import { Injectable} from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { GlobalVariable } from '../global';
import { GameForProfile, StatusGameProgressions } from './models/game';
import { ProfileModel } from './models/profile';


@Injectable({
    providedIn: 'root',

  })
  export class ProfileService{
    constructor(private http: HttpClient) { }
   
    public steamLoginIn(){
        const params = new HttpParams()
        .set('openid.ns', 'http://specs.openid.net/auth/2.0')
        .set('openid.mode', 'checkid_setup')
        .set('openid.return_to',`${GlobalVariable.BASE_FRONT_URL}games`)
        .set('openid.realm',GlobalVariable.BASE_FRONT_URL)
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
        withCredentials: true 
      };
        console.log(GlobalVariable.BASE_API_URL + 'logout');
        return this.http.post(GlobalVariable.BASE_API_URL + 'logout',{},httpOptions);
    }
    public profile(filter:string,sort:string){
      const httpOptions = {
        withCredentials: true 
      };
      const params = new HttpParams().set('filter',filter).set('sort',sort);
      return this.http.get<ProfileModel>(GlobalVariable.BASE_API_URL + 'profile?'+params,httpOptions);
    }
    public profileId(id: string,filter:string,sort:string){
      const params = new HttpParams().set('filter',filter).set('sort',sort);
      return this.http.get<ProfileModel>(GlobalVariable.BASE_API_URL + `profile/${id}?`+params);
    }

    public getAvatar(){
      const httpOptions = {
        withCredentials: true 
      };
      return this.http.get(GlobalVariable.BASE_API_URL + 'profile/avatar',httpOptions);
    }
    public updateGame(gameId:string,hours:number,statusGame:StatusGameProgressions){
      const httpOptions = {
        withCredentials: true 
      };
      const params = new HttpParams().set('gameId',gameId).set('hours',hours).set('statusGame',statusGame);
      return this.http.put(GlobalVariable.BASE_API_URL + 'profile/update/game?'+params,null,httpOptions);
    }
    public deleteGame(gameId:string){
      const httpOptions = {
        withCredentials: true 
      };
      return this.http.delete(GlobalVariable.BASE_API_URL + 'profile/delete/game?gameId='+gameId,httpOptions);
    }
  }