import { BaseResponseModel } from './baseresponsemodel.model'; 
import { Kind } from './kind.model'; 

export interface KindResponseModel extends BaseResponseModel {
    kind: Kind;
}
