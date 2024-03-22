import { BaseResponseModel } from './baseresponsemodel.model';

export interface LoginResponseModel extends BaseResponseModel {
    id: number;
    email: string;
    token: string;
}
