import { Moment } from "moment";

export class TempSortFilters{
   public dateOf?: Moment;
    public dateTo?: Moment;
    public nsfw: number = 0;
}

export class GenresCheckBox{
    public name?:string;
    public click:number = 0;
    constructor(name?: string, click: number = 0) {
        this.name = name;
        this.click = click;
      }
}