import { SYSTEM_LOADING, SYSTEM_LOADED, SHOW_MESSAGE, DISCARD_MESSAGE } from '../../constants/ActionTypes';
import Message from '../../models/Message';

const INIT_STATE = {
    openDialog: false,
    message: new Message(),
    isLoading: false

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
        case SHOW_MESSAGE: {
            return {
                ...state,
                message: { ...action.message },
                openDialog: true
            };
        }
        case DISCARD_MESSAGE: {
            return {
                ...state,
                openDialog: false
            };
        }
        default:
            return state;
    }
};
