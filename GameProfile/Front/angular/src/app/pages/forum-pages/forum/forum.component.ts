import { Component } from '@angular/core';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { Moment } from 'moment';
import { Observable, of } from 'rxjs';
import { ForumService } from 'src/app/services/forum.service';
import { GameService } from 'src/app/services/game.service';
import { Game } from 'src/app/services/models/game';
import { SortForForum } from 'src/app/services/models/sortForForum';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent {
  showFilters: boolean = false;
  sortForForum: SortForForum = new SortForForum();
  indeterminate: boolean = false;
  // for working denial checkbox
  closedCount: number = 0;
  posts: any;

  dateOf?: Moment;
  dateTo?: Moment;

  languages: string[] = ["Afar", "Abkhazian", "Avestan", "Afrikaans", "Akan", "Amharic", "Aragonese", "Arabic", "Assamese", "Avaric", "Aymara", "Azerbaijani", "Bashkir", "Belarusian", "Bulgarian", "Bihari languages", "Bislama", "Bambara", "Bengali", "Tibetan", "Breton", "Bosnian", "Catalan", "Chechen", "Chamorro", "Corsican", "Cree", "Czech", "Church Slavic", "Chuvash", "Welsh", "Danish", "German", "Maldivian", "Dzongkha", "Ewe", "Greek", "English", "Esperanto", "Spanish", "Estonian", "Basque", "Persian", "Fulah", "Finnish", "Fijian", "Faroese", "French", "Western Frisian", "Irish", "Gaelic", "Galician", "Guarani", "Gujarati", "Manx", "Hausa", "Hebrew", "Hindi", "Hiri Motu", "Croatian", "Haitian", "Hungarian", "Armenian", "Herero", "Interlingua", "Indonesian", "Interlingue", "Igbo", "Sichuan Yi", "Inupiaq", "Ido", "Icelandic", "Italian", "Inuktitut", "Japanese", "Javanese", "Georgian", "Kongo", "Kikuyu", "Kuanyama", "Kazakh", "Kalaallisut", "Central Khmer", "Kannada", "Korean", "Kanuri", "Kashmiri", "Kurdish", "Komi", "Cornish", "Kirghiz", "Latin", "Luxembourgish", "Ganda", "Limburgan", "Lingala", "Lao", "Lithuanian", "Luba-Katanga", "Latvian", "Malagasy", "Marshallese", "Maori", "Macedonian", "Malayalam", "Mongolian", "Marathi", "Malay", "Maltese", "Burmese", "Nauru", "Norwegian", "North Ndebele", "Nepali", "Ndonga", "Dutch", "Norwegian", "Norwegian", "South Ndebele", "Navajo", "Chichewa", "Occitan", "Ojibwa", "Oromo", "Oriya", "Ossetic", "Panjabi", "Pali", "Polish", "Pushto", "Portuguese", "Quechua", "Romansh", "Rundi", "Romanian", "Russian", "Kinyarwanda", "Sanskrit", "Sardinian", "Sindhi", "Northern Sami", "Sango", "Sinhala", "Slovak", "Slovenian", "Samoan", "Shona", "Somali", "Albanian", "Serbian", "Swati", "Sotho, Southern", "Sundanese", "Swedish", "Swahili", "Tamil", "Telugu", "Tajik", "Thai", "Tigrinya", "Turkmen", "Tagalog", "Tswana", "Tonga", "Turkish", "Tsonga", "Tatar", "Twi", "Tahitian", "Uighur", "Ukrainian", "Urdu", "Uzbek", "Venda", "Vietnamese", "Volapük", "Walloon", "Wolof", "Xhosa", "Yiddish", "Yoruba", "Zhuang", "Chinese", "Zulu"];
  findLanguages: Observable<string[]> = new Observable<[]>();
  searchStringLanguage: string = '';

  searchString: string = '';
  findGames: Observable<Game[]> = new Observable<Game[]>();
  games: Game[] = [];

  constructor(private forumService: ForumService, private gameService: GameService) { this.sortForForum.Sorting = 'dateCreateDescending'; }

  ngOnInit(): void {
    console.log(this.sortForForum)
    this.forumService.getPosts(this.sortForForum).subscribe(response => this.posts = response);
  }

  toggleFilters(): void {
    this.showFilters = !this.showFilters;
  }

  find(): void {
    this.sortForForum.Game = undefined;
    if (this.games.length > 0) {
      this.sortForForum.Game = this.games.map(s => s.id);
    }
    if (this.dateOf != undefined) {
      this.sortForForum.CreatedDateOf = this.dateOf.format('MM-DD-YYYY');
    }
    if (this.dateTo != undefined) {
      this.sortForForum.CreatedDateTo = this.dateTo.format('MM-DD-YYYY');
    }
    this.forumService.getPosts(this.sortForForum).subscribe(response => this.posts = response);

  }

  removeLanguage(language: string): void {
    if (this.sortForForum.Language == undefined) {
      return;
    }
    const index = this.sortForForum.Language.indexOf(language);

    if (index >= 0) {
      this.sortForForum.Language.splice(index, 1);
    }
    if (this.sortForForum.Language.length == 0) {
      this.sortForForum.Language == undefined
    }
  }

  selectedLanguage(event: MatAutocompleteSelectedEvent): void {

    if (this.sortForForum.Language == undefined) {
      this.sortForForum.Language = []
    }

    if (this.sortForForum.Language.includes(event.option.value)) {
      return;
    }
    this.sortForForum.Language.push(event.option.value);
  }

  onSearchChangeLanguage() {
    this.findLanguages = of(this.languages.filter(language =>
      language.toLowerCase().includes(this.searchStringLanguage.toLowerCase())
    ));
  }

  onSearchChange(): void {
    if (this.searchString == "") {
      this.findGames = new Observable<Game[]>();
      return;
    }
    this.gameService.fecthGameByString(this.searchString).subscribe(response => this.findGames = of(response));
  }

  remove(game: Game): void {
    const index = this.games.indexOf(game);

    if (index >= 0) {
      this.games.splice(index, 1);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    if (this.games.includes(event.option.value)) {
      return;
    }
    this.games.push(event.option.value);
  }


  changeClosed(): void {
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
