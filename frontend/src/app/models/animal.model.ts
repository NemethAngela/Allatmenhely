export interface Animal {
    id?: number | -1;
    name: string;
    kindId: number;
    age: number;
    isMale?: boolean | null;
    isNeutered: boolean;
    description?: string | null;
    photo?: string | null;
    isActive?: boolean | true;
    timeStamp?: Date | null;
}