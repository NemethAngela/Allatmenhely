import { BaseResponseModel } from './baseresponsemodel.model'; 
import { Animal } from './animal.model'; 

export interface AnimalsResponseModel extends BaseResponseModel {
    animals: Animal[];
}
