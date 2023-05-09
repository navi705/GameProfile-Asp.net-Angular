import { Component} from '@angular/core';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css'],
  })

  export class HeaderComponent{
    id: any;
    image:any;
    ngOnInit(): void {     
      if(localStorage.getItem('id') != undefined ){
        this.id = localStorage.getItem('id'); 
        this.image = localStorage.getItem('avatar');
      }
      
    }
  }