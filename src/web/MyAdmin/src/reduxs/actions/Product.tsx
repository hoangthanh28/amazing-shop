import { PRODUCT_LOADED, PRODUCT_DETAIL_LOADED } from '../../constants/ActionTypes';
export function endLoadProducts(products: any) {
    return {
        type: PRODUCT_LOADED,
        products: products
    };
}
export function endLoadProduct(product: any) {
    return {
        type: PRODUCT_DETAIL_LOADED,
        product: product
    };
}
