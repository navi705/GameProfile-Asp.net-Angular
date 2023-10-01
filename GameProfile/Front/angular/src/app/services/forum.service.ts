import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { GlobalVariable } from '../global';
import { SortForForum } from './models/sortForForum';
import { Post } from './models/forum';

@Injectable({
  providedIn: 'root',
})

export class ForumService {
  httpOptions = {
    withCredentials: true
  };

  constructor(private http: HttpClient) { }
  public getPosts(sortForForum: any) {
    let params = new HttpParams();
    Object.keys(sortForForum).forEach(function (key) {
      if (sortForForum[key] == null)
        return;
      params = params.append(key, sortForForum[key]);
    });
    return this.http.get<Array<any>>(GlobalVariable.BASE_API_URL + 'forum?' + params);
  }

  public createPost(post: Post) {
    const httpOptions = {
      withCredentials: true
    };
    let params: any;
    if (post.games != undefined) {
      params = new HttpParams().set('title', post.title).set('description', post.description).set('topic', post.topic).set('topic', post.topic).set("languages", post.languages.join(',')).set("games", post.games.join(','));
    }
    else {
      params = new HttpParams().set('title', post.title).set('description', post.description).set('topic', post.topic).set('topic', post.topic).set("languages", post.languages.join(','));
    }
    return this.http.put(GlobalVariable.BASE_API_URL + 'forum/add?' + params, null, httpOptions);
  }
  public getPostById(postId: string) {
    return this.http.get<Post>(GlobalVariable.BASE_API_URL + 'forum/' + postId);
  }

  public deletePost(postId: string) {
    const httpOptions = {
      withCredentials: true
    };
    let params = new HttpParams().set('Id', postId);
    return this.http.delete(GlobalVariable.BASE_API_URL + 'forum/delete?' + params, httpOptions);
  }

  public postChangeStatus(postId: string, status: boolean) {
    const httpOptions = {
      withCredentials: true
    };
    let params = new HttpParams().set('Id', postId).set('close', status);
    return this.http.post(GlobalVariable.BASE_API_URL + 'forum?' + params, null, httpOptions);
  }


  public createMessagePost(content: string, postId: string) {
    let params = new HttpParams().set('content', content).set('postId', postId);
    return this.http.put(GlobalVariable.BASE_API_URL + 'forum/messagePost/add?' + params, null, this.httpOptions);
  }

  public deleteMessagePost(messagePostID: string) {
    let params = new HttpParams().set('messagePostID', messagePostID);
    return this.http.delete(GlobalVariable.BASE_API_URL + 'forum/messagePost?' + params, this.httpOptions);
  }

  public updateMessagePost(messagePostID: string, content: string) {
    let params = new HttpParams().set('messagePostID', messagePostID).set('content', content);
    return this.http.put(GlobalVariable.BASE_API_URL + 'forum/messagePost/update?' + params, null, this.httpOptions);
  }

  public createReplie(content: string,messagePostId: string) {
    let params = new HttpParams().set('content', content).set('messagePostId', messagePostId);
    return this.http.put(GlobalVariable.BASE_API_URL + 'forum/replie/add?' + params, null, this.httpOptions);
  }

  public deleteReplie(replieId: string) {
    let params = new HttpParams().set('replieId', replieId);
    return this.http.delete(GlobalVariable.BASE_API_URL + 'forum/replie?' + params, this.httpOptions);
  }

  public updateReplie(replieId: string, content: string) {
    let params = new HttpParams().set('replieId', replieId).set('content', content);
    return this.http.put(GlobalVariable.BASE_API_URL + 'forum/replie/update?' + params, null, this.httpOptions);
  }

  public addRating(postId: string, rating: string) {
    const httpOptions = {
      withCredentials: true
    };
    const params = new HttpParams().set('postId', postId).set('rating', rating);
    return this.http.post(GlobalVariable.BASE_API_URL + 'forum/rating?' + params, null, httpOptions);
  }
}