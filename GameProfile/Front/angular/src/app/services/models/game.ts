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
    achievementsCount: number
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