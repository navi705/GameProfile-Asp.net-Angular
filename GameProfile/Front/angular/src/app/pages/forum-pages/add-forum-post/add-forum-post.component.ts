import { Component } from '@angular/core';
import { GameService } from 'src/app/services/game.service';
import { Post } from 'src/app/services/models/forum';
import { Game } from 'src/app/services/models/game';
import {Observable} from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { of, catchError } from 'rxjs';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { ForumService } from 'src/app/services/forum.service';

@Component({
  selector: 'app-add-forum-post',
  templateUrl: './add-forum-post.component.html',
  styleUrls: ['./add-forum-post.component.css']
})
export class AddForumPostComponent {
  errorMessage:string ='';

  post: Post= {} as Post;
  searchString:string = '';
  findGames: Observable<Game[]> = new Observable<Game[]>();
  games:Game[] = [];

  languages:string[] = ["Afar", "Abkhazian", "Avestan", "Afrikaans", "Akan", "Amharic", "Aragonese", "Arabic", "Assamese", "Avaric", "Aymara", "Azerbaijani", "Bashkir", "Belarusian", "Bulgarian", "Bihari languages", "Bislama", "Bambara", "Bengali", "Tibetan", "Breton", "Bosnian", "Catalan", "Chechen", "Chamorro", "Corsican", "Cree", "Czech", "Church Slavic", "Chuvash", "Welsh", "Danish", "German", "Maldivian", "Dzongkha", "Ewe", "Greek", "English", "Esperanto", "Spanish", "Estonian", "Basque", "Persian", "Fulah", "Finnish", "Fijian", "Faroese", "French", "Western Frisian", "Irish", "Gaelic", "Galician", "Guarani", "Gujarati", "Manx", "Hausa", "Hebrew", "Hindi", "Hiri Motu", "Croatian", "Haitian", "Hungarian", "Armenian", "Herero", "Interlingua", "Indonesian", "Interlingue", "Igbo", "Sichuan Yi", "Inupiaq", "Ido", "Icelandic", "Italian", "Inuktitut", "Japanese", "Javanese", "Georgian", "Kongo", "Kikuyu", "Kuanyama", "Kazakh", "Kalaallisut", "Central Khmer", "Kannada", "Korean", "Kanuri", "Kashmiri", "Kurdish", "Komi", "Cornish", "Kirghiz", "Latin", "Luxembourgish", "Ganda", "Limburgan", "Lingala", "Lao", "Lithuanian", "Luba-Katanga", "Latvian", "Malagasy", "Marshallese", "Maori", "Macedonian", "Malayalam", "Mongolian", "Marathi", "Malay", "Maltese", "Burmese", "Nauru", "Norwegian", "North Ndebele", "Nepali", "Ndonga", "Dutch", "Norwegian", "Norwegian", "South Ndebele", "Navajo", "Chichewa", "Occitan", "Ojibwa", "Oromo", "Oriya", "Ossetic", "Panjabi", "Pali", "Polish", "Pushto", "Portuguese", "Quechua", "Romansh", "Rundi", "Romanian", "Russian", "Kinyarwanda", "Sanskrit", "Sardinian", "Sindhi", "Northern Sami", "Sango", "Sinhala", "Slovak", "Slovenian", "Samoan", "Shona", "Somali", "Albanian", "Serbian", "Swati", "Sotho, Southern", "Sundanese", "Swedish", "Swahili", "Tamil", "Telugu", "Tajik", "Thai", "Tigrinya", "Turkmen", "Tagalog", "Tswana", "Tonga", "Turkish", "Tsonga", "Tatar", "Twi", "Tahitian", "Uighur", "Ukrainian", "Urdu", "Uzbek", "Venda", "Vietnamese", "Volapük", "Walloon", "Wolof", "Xhosa", "Yiddish", "Yoruba", "Zhuang", "Chinese", "Zulu"];
  findLanguages: Observable<string[]> = new Observable<[]>();
  languagesPick:string[]=[];
  searchStringLanguage:string = '';

  isSending: boolean = false;

  constructor(public gameService:GameService, public forumService:ForumService){
  }

  onSearchChange(): void {  
    if(this.searchString == ""){
      this.findGames = new Observable<Game[]>();
      return;
    }    
    this.gameService.fecthGameByString(this.searchString).subscribe(response=> this.findGames=of(response));
  }  

  remove(game: Game): void {
    const index = this.games.indexOf(game);

    if (index >= 0) {
      this.games.splice(index, 1);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    if(this.games.includes(event.option.value)){
      return;
    }
    this.games.push(event.option.value);
  }

  removeLanguage(language: string): void {
    const index = this.languagesPick.indexOf(language);

    if (index >= 0) {
      this.languagesPick.splice(index, 1);
    }
  }

  selectedLanguage(event: MatAutocompleteSelectedEvent): void {
    if(this.languagesPick.includes(event.option.value)){
      return;
    }
    this.languagesPick.push(event.option.value);
  }
  
  onSearchChangeLanguage(){
    this.findLanguages = of(this.languages.filter(language =>
      language.toLowerCase().includes(this.searchStringLanguage.toLowerCase())
    ));
  }

  createPost() :void{
    this.post.games = this.games.map(game => game.id);
    this.post.languages = this.languagesPick

    if(this.post.title.length < 6){
      this.errorMessage = 'The title must be at least 6 symbols.';
      return;
    }
    if(this.post.description.length < 10){
      this.errorMessage = 'The description must be at least 10 symbols.';
      return;
    }
    if(this.post.topic == null || this.post.topic == undefined){
      this.errorMessage = 'You have to choose a topic.';
      return;
    }
    if( this.post.languages!=undefined || this.post.languages != null){
      if((this.post.languages as string[]).length === 0 ){
      this.errorMessage = 'You have to choose a language.';
      return;
      }
    }
    this.errorMessage = '';

    if(!this.isSending){
      this.isSending = true;
      this.forumService.createPost(this.post).subscribe(   x => 
        window.location.href = 'forum');
    }
  }
}
