import { PRODUCT_LOADED } from '../../constants/ActionTypes';
export function endLoadProducts(products: any) {
    return {
        type: PRODUCT_LOADED,
        products: products
    };
}
