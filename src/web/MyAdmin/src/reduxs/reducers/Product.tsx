import { PRODUCT_LOADED, PRODUCT_DETAIL_LOADED } from '../../constants/ActionTypes';
import Product from '../../models/Product';

const INIT_STATE = {
    products: [],
    product: Product
};

export default (state = INIT_STATE, action: any) => {
    switch (action.type) {
        case PRODUCT_LOADED: {
            return {
                ...state,
                products: action.products
            };
        }
        case PRODUCT_DETAIL_LOADED: {
            return {
                ...state,
                product: { ...action.product }
            };
        }
        default:
            return state;
    }
};
