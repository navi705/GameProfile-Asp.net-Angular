export interface Game {
    id: string
    title: string
    releaseDate: string
    headerImage: string
    nsfw: boolean
    description: string
    screenshots: screenshot[]
    genres: genre[]
    publishers: publisher[]
    developers: developer[]
    shopsLinkBuyGame: shopsLinkBuyGame[]
    profileHasGames: any[]
    achievementsCount: number
  }

  export interface GameForBrowse
  {
    id: string
    title: string
    releaseDate: string
    headerImage: string
    genres: genre[]
    publishers: publisher[]
    developers: developer[]
    review: review[]
    status: StatusGameProgressions
  }

  export interface review
  {
    site: number
    score:number
  }
  
  export interface screenshot {
    uri: string
  }
  
  export interface genre {
    gameString: string
  }
  
  export interface publisher {
    gameString: string
  }
  
  export interface developer {
    gameString: string
  }
  
  export interface shopsLinkBuyGame {
    uri: string
  }

  export interface GamePut {
    title: string
    releaseDate: string
    headerImage: string
    nsfw: boolean
    description: string
    screenshots: screenshot[]
    genres: genre[]
    publishers: publisher[]
    developers: developer[]
    shopsLinkBuyGame: shopsLinkBuyGame[]
    achievementsCount: number
  }
  export class GameClass{
    id!: string
    title!: string
    releaseDate!: string
    headerImage!: string
    nsfw!: boolean
    description!: string
    screenshots!: screenshot[]
    genres!: genre[]
    publishers!: publisher[]
    developers!: developer[]
    shopsLinkBuyGame!: shopsLinkBuyGame[]
    achievementsCount!: number
  }

  export interface GameForProfile{
    id:string;
    title:string;
    headerImage:string;
    hours:number;
    statusGame: StatusGameProgressions;
  }
  
  export enum StatusGameProgressions
  {
      Playing=1,
      Completed=2,
      Dropped=3,
      Planned=4,
      NONE=0
  }