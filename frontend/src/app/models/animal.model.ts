export interface Animal {
    id: number;
    name: string;
    kindId: number;
    age: number;
    isMale?: boolean | null;
    isNeutered: boolean;
    description?: string | null;
    photo?: string | null;
    isActive: boolean;
    timeStamp: Date;
}