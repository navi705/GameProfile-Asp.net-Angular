import { Component } from '@angular/core';
import { ForumService } from 'src/app/services/forum.service';
import { SortForForum } from 'src/app/services/models/sortForForum';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent {
  showFilters: boolean = false;
  sortForForum:SortForForum = new SortForForum();
  indeterminate: boolean = false;
  // for working denial checkbox
  closedCount:number=0;

  constructor(private forumService:ForumService){}

  ngOnInit(): void {

  }

  toggleFilters():void {
    this.showFilters = !this.showFilters;
  }

  find():void{
    
  }

  changeClosed():void{
    this.closedCount++;
    if (this.closedCount == 3) {
      this.sortForForum.Closed = undefined;
      this.closedCount = 0;
      return;
    }
    if (this.closedCount == 1) {
      this.sortForForum.Closed = "yes";
      return;
    }
    if (this.closedCount == 2) {
      this.sortForForum.Closed = "no";
      this.indeterminate = true;
      return;
    }
  }

}
