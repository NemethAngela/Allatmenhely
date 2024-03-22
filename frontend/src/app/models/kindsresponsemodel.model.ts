import { BaseResponseModel } from './baseresponsemodel.model';
import { Kind } from './kind.model'; 

export interface KindsResponseModel extends BaseResponseModel {
    kinds: Kind[];
}
