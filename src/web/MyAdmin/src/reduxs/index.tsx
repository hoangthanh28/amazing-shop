import * as Redux from 'redux';
import { createBrowserHistory } from 'history';
import { routerMiddleware } from 'connected-react-router';
import createOidcMiddleware, {
    reducer as oidcReducer,
    loadUser
} from 'redux-oidc';
import thunk from 'redux-thunk';
// import logger from 'redux-logger';
import userManager from '../auth/Oidc';
import reducers from './reducers/index';


const history = createBrowserHistory();
const routeMiddleware = routerMiddleware(history);
const oidcMiddleware = createOidcMiddleware(userManager);
let middlewares = [oidcMiddleware, thunk, routeMiddleware];

const store = Redux.createStore(
    reducers(history, oidcReducer),
    {},
    Redux.compose(Redux.applyMiddleware(...middlewares))
);
export default store;

export function configureStore() {
    loadUser(store, userManager);
    return store;
}

export { history };