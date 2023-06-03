import { Component } from '@angular/core';
import { StatsService } from 'src/app/services/stats.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
  providers:[StatsService]
})
export class HomePageComponent {
  count:any;
  constructor(private statsService:StatsService){}
  ngOnInit(): void {
    this.statsService.GetCount().subscribe(response=> this.count =response);
  }
  
}
