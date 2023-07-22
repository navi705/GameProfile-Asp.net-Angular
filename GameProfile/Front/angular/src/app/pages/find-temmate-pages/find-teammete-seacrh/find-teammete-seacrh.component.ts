import { Component } from '@angular/core';
import { FindTeammateService } from 'src/app/services/findteammate.service';

@Component({
  selector: 'app-find-teammete-seacrh',
  templateUrl: './find-teammete-seacrh.component.html',
  styleUrls: ['./find-teammete-seacrh.component.css']
})
export class FindTeammeteSeacrhComponent {
  showFilters: boolean = false;

  constructor(private findTeammeateService: FindTeammateService) { }

  ngOnInit(): void {
    this.findTeammeateService.getTeammates();
  }

  toggleFilters() {
    this.showFilters = !this.showFilters;
  }
  find() {

  }

}
