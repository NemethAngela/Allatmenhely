export interface Admin {
    id: number;
    email: string;
    passwordHash: string;
    passwordSalt: string;
}