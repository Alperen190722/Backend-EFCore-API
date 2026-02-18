import {Photo} from "./photo";

export class City {
    id! : number;
    name! : string;
    description! : string;
    photoUrl!: string;
    userId! : number;
    photos! : Photo[];
}
