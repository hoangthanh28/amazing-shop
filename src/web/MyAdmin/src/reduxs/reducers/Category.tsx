import { CATEGORY_LOADED } from '../../constants/ActionTypes';

const INIT_STATE = {
    categories: []
};

export default (state = INIT_STATE, action: any) => {
    switch (action.type) {
        case CATEGORY_LOADED: {
            return {
                ...state,
                categories: action.categories
            };
        }
        default:
            return state;
    }
};
