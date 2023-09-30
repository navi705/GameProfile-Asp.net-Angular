import { Component } from '@angular/core';
import { ForumService } from 'src/app/services/forum.service';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/services/models/forum';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent {
  post: any;
  id: any;
  addMessage: string = '';
  status: string = '';
  j:number = 0;

  editContentMessagePost:string = '';
  editPost: number=-1;

  addReplie: number=-1;
  replieContent: string = '';

  editReplie:number=-1;
  editReplieContent: string = '';

  constructor(private route: ActivatedRoute, private forumService: ForumService) {
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.forumService.getPostById(this.id).subscribe(response => { this.post = response; if (response.closed == true) { this.status = 'Open post again' } else { this.status = 'Close this post' } });

  }

  send(): void {
    if(this.addMessage == ''){
      return;
    }
    this.forumService.createMessagePost(this.addMessage, this.post.id).subscribe(x => { window.location.reload() });
  }

  auth(): boolean {
    if (localStorage.getItem('auth') === 'false' || localStorage.getItem('auth') == undefined) {
      return false;
    }
    else {
      return true;
    }
  }

  checkCanClosePost(): boolean {
    if (localStorage.getItem('userId') == this.post.author) {
      return true;
    }
    return false;
  }


  closeOrOpen(): void {
    this.forumService.postChangeStatus(this.post.id, !this.post.closed).subscribe(x => {window.location.reload() });
  }

  deletePost(): void {
    this.forumService.deletePost(this.post.id).subscribe(x => { window.location.href = "/forum"; });
  }

  deleteMessagePost(messagePostId: string): void {
    this.forumService.deleteMessagePost(messagePostId).subscribe(x => { window.location.reload() });
  }
  updateMessagePost(messagePostId: string, content: string): void {
    content = this.editContentMessagePost;
    this.forumService.updateMessagePost(messagePostId, content).subscribe(x => { window.location.reload() });
  }

  editMessagePost(i:number):void{
    this.editPost=i;
    if(i>-1){
      this.editContentMessagePost = this.post.messagePosts[i].content;
    }
  }

  checkCanControlMessagePost(idAuthorMessage:string):boolean{
    if (localStorage.getItem('userId') == idAuthorMessage) {
      return true;
    }
    return false;
  }

  addReplieF(i:number):void{
    this.addReplie = i;
  }
  
  sendReplie(messagePostID:string):void{
  this.forumService.createReplie(this.replieContent,messagePostID,).subscribe(x => { window.location.reload() });
  }

  deleteReplie(id:string):void{
    this.forumService.deleteReplie(id).subscribe(x => { window.location.reload() });
  }

  editReplieQuery(id:string):void{
    this.forumService.updateReplie(id,this.editReplieContent).subscribe(x => { window.location.reload() });
  }

  editReplieF(i:number,message:any){
    this.editReplie= i;
    if(i>-1){
      this.editReplieContent = this.post.messagePosts[message].replies[i].content;
    }
  }

}
