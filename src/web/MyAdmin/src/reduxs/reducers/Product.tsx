import { PRODUCT_LOADED } from '../../constants/ActionTypes';

const INIT_STATE = {
    products: []
};

export default (state = INIT_STATE, action: any) => {
    switch (action.type) {
        case PRODUCT_LOADED: {
            return {
                ...state,
                products: action.products
            };
        }
        default:
            return state;
    }
};
