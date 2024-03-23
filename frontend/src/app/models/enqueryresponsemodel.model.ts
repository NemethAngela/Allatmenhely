import { BaseResponseModel } from './baseresponsemodel.model'; 
import { Enquery } from './enquery.model'; 

export interface EnqueryResponseModel extends BaseResponseModel {
    enquery: Enquery;
}
