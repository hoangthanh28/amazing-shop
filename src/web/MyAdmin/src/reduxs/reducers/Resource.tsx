import { RESOURCE_LOADED } from '../../constants/ActionTypes';

const INIT_STATE = {
    resources: []
};

export default (state = INIT_STATE, action: any) => {
    switch (action.type) {
        case RESOURCE_LOADED: {
            return {
                ...state,
                resources: action.resources
            };
        }
        default:
            return state;
    }
};
