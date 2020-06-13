import { SYSTEM_LOADING, SYSTEM_LOADED } from '../../constants/ActionTypes';

const INIT_STATE = {
    products: []
};

export default (state = INIT_STATE, action: any) => {
    switch (action.type) {
        case SYSTEM_LOADING: {
            return {
                ...state,
                isLoading: true
            };
        }
        case SYSTEM_LOADED: {
            return {
                ...state,
                isLoading: false
            };
        }
        default:
            return state;
    }
};
