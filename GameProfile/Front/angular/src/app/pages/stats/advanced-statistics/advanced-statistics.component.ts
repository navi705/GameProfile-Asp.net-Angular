import { Component } from '@angular/core';
import { Stats } from 'src/app/services/models/stats';
import { StatsService } from 'src/app/services/stats.service';
import { tagsRating } from './data';

@Component({
  selector: 'app-advanced-statistics',
  templateUrl: './advanced-statistics.component.html',
  styleUrls: ['./advanced-statistics.component.css'],
  providers:[StatsService]
})
export class AdvancedStatisticsComponent {
  stats:any;
  tagsRating:any[]=[];
  view:[number,number] = [1500,8000];
  viewSmall:[number,number] = [1500,200];

  constructor(private statsService: StatsService){
    this.tagsRating = tagsRating;
  }

  ngOnInit(): void {
    this.statsService.GetAdvancedStats().subscribe(response=> {this.stats = response;});
  }



}
