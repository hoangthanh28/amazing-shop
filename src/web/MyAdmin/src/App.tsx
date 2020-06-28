import React, { Component } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.css';
import Home from './components/Home';
import { history, configureStore } from './reduxs/index';
import { Provider } from 'react-redux';
import { OidcProvider } from 'redux-oidc';
import { ConnectedRouter } from 'connected-react-router';
import { Route, Switch } from 'react-router-dom';
import userManager from './auth/Oidc';
import AppContext, { services } from './AppContext';
import './assets/app.scss'
const store = configureStore();
export default class App extends Component<{}, {}> {
  //static displayName = App.name;

  render() {
    // const {
    //   location: { pathname }
    // } = history;
    return (
      <Provider store={store}>
        <OidcProvider userManager={userManager} store={store}>
          <ConnectedRouter history={history}>
            <AppContext.Provider value={services}>
              <Switch>
                <Route
                  path="/"
                  render={props => {
                    return <Home {...props} {...services} />;
                  }}
                />
              </Switch>
            </AppContext.Provider>
          </ConnectedRouter>
        </OidcProvider>
      </Provider>
    );
  }
}
