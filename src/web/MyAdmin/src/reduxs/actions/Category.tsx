import { CATEGORY_LOADED } from '../../constants/ActionTypes';
export function loadCompleted(categories: any) {
    return {
        type: CATEGORY_LOADED,
        categories: categories
    };
}
