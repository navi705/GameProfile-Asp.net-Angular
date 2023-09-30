import { Game } from "./game";

export class Post {
    id: string;
    title: string;
    description: string;
    topic: string;
    author: string;
    rating: number;
    closed: boolean = false;
    created: string;
    updated: string;
    languages: string[];
    games?: string[];
    messagePosts?: string[];
  
    constructor() {
      // Initialize properties with default values or leave them undefined
      this.id = '';
      this.title = '';
      this.description = '';
      this.topic = '';
      this.author = '';
      this.rating = 0;
      this.created = '';
      this.updated = '';
      this.languages = [];
    }
  }
  