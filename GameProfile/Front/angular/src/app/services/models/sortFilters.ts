import { StatusGameProgressions } from "./game";

export class SotrtFilter{
    Sorting?: string;
    Page?: number;
    ReleaseDateOf?:string;
    ReleaseDateTo?:string;
    Nsfw?: string;
    Genres?: string[];
    GenresExcluding? : string[] ;
    Tags?: string[];
    TagsExcluding? : string[] ;
    RateOf?: number;
    RateTo?: number;
    StatusGameProgressions?:StatusGameProgressions[];
    StatusGameProgressionsExcluding?:StatusGameProgressions[];
}