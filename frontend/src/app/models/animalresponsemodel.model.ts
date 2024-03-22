import { BaseResponseModel } from './baseresponsemodel.model'; 
import { Animal } from './animal.model';

export interface AnimalResponseModel extends BaseResponseModel {
    animal: Animal;
}
