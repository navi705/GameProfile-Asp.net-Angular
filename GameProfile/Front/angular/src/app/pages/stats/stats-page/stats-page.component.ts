import { Component } from '@angular/core';
import { StatsService } from 'src/app/services/stats.service';

@Component({
  selector: 'app-stats-page',
  templateUrl: './stats-page.component.html',
  styleUrls: ['./stats-page.component.css'],
  providers:[StatsService]
})
export class StatsPageComponent {
  stats:any;
  constructor(private statsService: StatsService){
  }

  ngOnInit(): void {
    this.statsService.GetStats().subscribe(response=>this.stats = response);
  }

}
