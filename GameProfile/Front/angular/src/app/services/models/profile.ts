import { StatusGameProgressions } from "./game"

export interface ProfileModel{
    nickName:string,
    description: string,
    avatar: string,
    gameList:GameList[],
    totalHours: number
}

export interface GameList{
    id:string,
    title:string,
    headerImage:string,
    hours:number,
    statusGame:StatusGameProgressions
}