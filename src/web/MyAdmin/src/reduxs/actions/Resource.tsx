import { RESOURCE_LOADED } from '../../constants/ActionTypes';
export function endLoadResource(resources: any) {
    return {
        type: RESOURCE_LOADED,
        resources: resources
    };
}
