import CommonService from "./common";
import { PRODUCT_LIST } from '../constants/Constants'
export default class ProductService {
    commonService: CommonService;

    constructor() {
        this.commonService = new CommonService();
    }
    getAllProducts() {
        console.log('Get all product');
        return this.commonService.fetchData(PRODUCT_LIST);
    }
}