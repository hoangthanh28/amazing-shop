import { SYSTEM_LOADING, SYSTEM_LOADED, SHOW_MESSAGE, DISCARD_MESSAGE } from '../../constants/ActionTypes';
import Message from '../../models/Message'
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
export function ShowMessage(message: Message) {
    return {
        type: SHOW_MESSAGE,
        message: message
    };
}
export function DiscardMessage() {
    return {
        type: DISCARD_MESSAGE
    };
}
