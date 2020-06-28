import CommonService from "./common";
import { CATEGORY_LIST } from '../constants/Constants'
export default class CategoryService {
    commonService: CommonService;

    constructor() {
        this.commonService = new CommonService();
    }
    getAll() {
        return this.commonService.fetchData(CATEGORY_LIST).then(res => res.data);
    }
}