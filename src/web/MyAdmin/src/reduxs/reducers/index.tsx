import { combineReducers } from 'redux';
import Product from './Product';
import System from './System';
import { connectRouter } from 'connected-react-router';
export default (history, oidcReducer) => combineReducers({
    router: connectRouter(history),
    oidc: oidcReducer,
    product: Product,
    system: System
});
