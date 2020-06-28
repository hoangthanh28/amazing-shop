import CommonService from "./common";
import { RESOURCE_LIST } from '../constants/Constants'
export default class ResourceService {
    commonService: CommonService;

    constructor() {
        this.commonService = new CommonService();
    }
    getAllResources() {
        console.log('Get all resources');
        return this.commonService.fetchData(RESOURCE_LIST).then(res => res.data);
    }
}