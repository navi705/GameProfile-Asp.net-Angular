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
  loading: boolean = true
  tagsRating:any[]=[];
  view:[number,number] = [1500,8000];
  viewSmall:[number,number] = [1500,200];

  view1: any;
  view2: any;
  view3: any;

  view11:[number,number] = [1500,8000];
  view22:[number,number] = [1500,8000];
  view33:[number,number] = [1500,8000];

  constructor(private statsService: StatsService){
    this.tagsRating = tagsRating;
  }

  ngOnInit(): void {
    this.statsService.GetAdvancedStats().subscribe(response=> {this.stats = response;
      this.view1 = this.stats.tagsStats;
      this.view2 = tagsRating;
      this.view3 = this.stats.tagsPopularProfileDTO;
      this.loading = false;
    });
  }

  setView(option: number, chartNumber: number): void {
    switch (chartNumber) {
      case 1:
        if(option == 0){
          this.view1 = this.stats.tagsStats;
          this.view11 = [1500,8000];
        }
        if(option == 2){
          this.view1 = this.stats.tagsStats.slice(0, Math.ceil(this.stats.tagsStats.length / 2))
          this.view11 = [1500,4000];
        }
        if(option == 4){
          this.view1 = this.stats.tagsStats.slice(0, Math.ceil(this.stats.tagsStats.length / 4))
          this.view11 = [1500,2000];
        }
        break;
      case 2:
        if(option == 0){
          this.view2 = this.tagsRating;
          this.view22 = [1500,8000];
        }
        if(option == 2){
          this.view2 = this.view2.slice(0, Math.ceil(this.tagsRating.length / 2))
          this.view22 = [1500,4000];
        }
        if(option == 4){
          this.view2 = this.view2.slice(0, Math.ceil(this.tagsRating.length / 4))
          this.view22 = [1500,2000];
        }
        break;
      case 3:
        if(option == 0){
          this.view3 = this.stats.tagsPopularProfileDTO
          this.view33 = [1500,8000];
        }
        if(option == 2){
          this.view3 = this.stats.tagsPopularProfileDTO.slice(0, Math.ceil(this.stats.tagsPopularProfileDTO.length / 2))
          this.view33 = [1500,4000];
        }
        if(option == 4){
          this.view3 = this.stats.tagsPopularProfileDTO.slice(0, Math.ceil(this.stats.tagsPopularProfileDTO.length / 4))
          this.view33 = [1500,2000];
        }
        break;
      default:
        // Handle unexpected chartNumber
        break;
    }
  }

}
