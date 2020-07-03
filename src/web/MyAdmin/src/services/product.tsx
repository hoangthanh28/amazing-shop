import CommonService from "./common";
import { PRODUCT_LIST, PRODUCT_DETAIL, PRODUCT_DETAIL_IMAGE } from '../constants/Constants'
export default class ProductService {
    commonService: CommonService;

    constructor() {
        this.commonService = new CommonService();
    }
    getAllProducts() {
        return this.commonService.fetchData(PRODUCT_LIST).then(res => res.data);
    }
    getProductById(id) {
        return this.commonService.fetchData(PRODUCT_DETAIL.replace('{0}', id));
    }
    uploadImage(file: File) {
        const data = new FormData();
        data.append('file', file);
        return this.commonService.postData(PRODUCT_DETAIL_IMAGE, data);
    }
    updateProduct(id, data) {
        return this.commonService.putData(PRODUCT_DETAIL.replace('{0}', id), data);
    }
}