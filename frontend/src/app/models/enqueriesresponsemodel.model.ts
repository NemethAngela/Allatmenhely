import { BaseResponseModel } from './baseresponsemodel.model'; 
import { Enquery } from './enquery.model'; 

export interface EnqueriesResponseModel extends BaseResponseModel {
    enqueries: Enquery[];
}
