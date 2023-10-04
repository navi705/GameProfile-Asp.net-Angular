import { StatusGameProgressions } from "./game"

export interface ProfileModel{
    nickName:string,
    description: string,
    avatar: string,
    gameList:GameList[],
    totalHours: number,
    totalHoursVerification: number,
    totalHoursNotVerification: number,
    totalHoursForSort: number
}

export interface GameList{
    id:string,
    title:string,
    headerImage:string,
    hours:number,
    hoursVereficated:number,
    statusGame:StatusGameProgressions
}