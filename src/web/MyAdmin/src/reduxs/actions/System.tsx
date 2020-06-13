import { SYSTEM_LOADING, SYSTEM_LOADED } from '../../constants/ActionTypes';

export function Loading() {
    return {
        type: SYSTEM_LOADING,
    };
}
export function Loaded() {
    return {
        type: SYSTEM_LOADED,
    };
}
