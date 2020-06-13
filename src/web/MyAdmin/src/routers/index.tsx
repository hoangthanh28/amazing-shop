import { Route, Switch } from 'react-router-dom';
import React from 'react';
import Product from '../components/Product';
import { Logout } from '../components/Logout';
import { Welcome } from '../components/Welcome';
const appRoutes: any = (
    <Switch>
        <Route exact path='/products' component={Product} />
        <Route exact path='/logout' component={Logout} />
        <Route exact path='/' component={Welcome} />
    </Switch>
);
export default appRoutes;