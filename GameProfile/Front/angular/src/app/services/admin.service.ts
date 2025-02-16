import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { GlobalVariable } from '../global';

@Injectable({
    providedIn: 'root',
})
export class AdminService {
    constructor(private http: HttpClient) { }

     httpOptions = {
        withCredentials: true 
      };

    public GetAdmin() {

        return this.http.get<any>(GlobalVariable.BASE_API_URL + `admin`,this.httpOptions);
    }

    public deleteQuery(id: string, name:string) {
        return this.http.delete(GlobalVariable.BASE_API_URL + `admin?id=`+id+`&name=`+name,this.httpOptions);
    }

    public GetUsersAndRoles(){
        return this.http.get(GlobalVariable.BASE_API_URL + 'admin/users',this.httpOptions);
    }

    public AddRoleToUser(id:string){
        return this.http.post(GlobalVariable.BASE_API_URL + 'admin?id=' + id,null,this.httpOptions);
    }

}