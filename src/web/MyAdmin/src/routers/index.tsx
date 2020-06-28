import { Route, Switch } from 'react-router-dom';
import React from 'react';
import ProductList from '../components/Product/index';
import EditProduct from '../components/Product/Edit';
import Resource from '../components/Resource';
import Category from '../components/Category';
import { Logout } from '../components/Logout';
import { Welcome } from '../components/Welcome';
const appRoutes: any = (
    <Switch>
        <Route exact path='/resources' component={Resource} />
        <Route exact path='/products' component={ProductList} />
        <Route exact path='/products/create' component={ProductList} />
        <Route exact path='/products/:id/edit' component={EditProduct} />
        <Route exact path='/categories' component={Category} />
        <Route exact path='/logout' component={Logout} />
        <Route exact path='/' component={Welcome} />
    </Switch>
);
export default appRoutes;