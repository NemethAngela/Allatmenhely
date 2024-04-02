import { Animal } from "./animal.model";

export interface Enquery {
    id?: number;
    timeStamp?: Date;
    phone?: string | null;
    animalId: number;
    email: string;

    animal?: Animal;
}
