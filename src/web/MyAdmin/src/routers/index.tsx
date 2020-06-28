import { Route, Switch } from 'react-router-dom';
import React from 'react';
import Product from '../components/Product';
import Resource from '../components/Resource';
import { Logout } from '../components/Logout';
import { Welcome } from '../components/Welcome';
const appRoutes: any = (
    <Switch>
        <Route exact path='/resources' component={Resource} />
        <Route exact path='/products' component={Product} />
        <Route exact path='/logout' component={Logout} />
        <Route exact path='/' component={Welcome} />
    </Switch>
);
export default appRoutes;