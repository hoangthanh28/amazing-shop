import CommonService from "./common";
import { PRODUCT_LIST, PRODUCT_DETAIL } from '../constants/Constants'
export default class ProductService {
    commonService: CommonService;

    constructor() {
        this.commonService = new CommonService();
    }
    getAllProducts() {
        return this.commonService.fetchData(PRODUCT_LIST);
    }
    getProductById(id) {
        return this.commonService.fetchData(PRODUCT_DETAIL.replace('{0}', id));
    }
}