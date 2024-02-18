import { Component} from '@angular/core';
import { AdminService } from 'src/app/services/admin.service';

@Component({
  selector: 'app-admin-game',
  templateUrl: './admin-game.component.html',
  styleUrls: ['./admin-game.component.css'],
  providers: []
})
export class AdminGameComponent {
  id:string='';
  name:string='';
  users:any;
  constructor( public adminService: AdminService

  )
  {
  }
  ngOnInit(): void {  
    this.adminService.GetAdmin().subscribe(
    //   response => 
    //   {
    //   if (response.status != 200) {
    //     window.location.href='/';
    //   }
    // },
    // error => {
    //   // Обработка ошибок
    //   window.location.href='/';
    // }
    );

    this.adminService.GetUsersAndRoles().subscribe(response => this.users = response);
  }

  delete(){
    this.adminService.deleteQuery(this.id,this.name).subscribe();
  }

  addModerator(id:string){
    this.adminService.AddRoleToUser(id).subscribe();
  }
    
}

 
