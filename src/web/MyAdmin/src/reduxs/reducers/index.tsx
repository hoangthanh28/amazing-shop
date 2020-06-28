import { combineReducers } from 'redux';
import Product from './Product';
import Resource from './Resource';
import System from './System';
import Category from './Category';
import { connectRouter } from 'connected-react-router';
export default (history, oidcReducer) => combineReducers({
    router: connectRouter(history),
    oidc: oidcReducer,
    product: Product,
    resource: Resource,
    system: System,
    category: Category
});
