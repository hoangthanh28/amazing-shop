import React, { Component } from 'react';
import { StateToPropInterface } from '../interfaces/PagePropsInterface';
import { connect } from 'react-redux';
import _ from 'lodash'
import { RouteComponentProps, Route, Switch } from 'react-router-dom';
import Callback from './Callback';
import Main from './Main';
interface HomeProps {
  user: StateToPropInterface['oidc']['user'];
}
class Home extends Component<HomeProps & RouteComponentProps, {}> {
  render() {
    return (
      <Switch>
        <Route exact path="/callback" component={Callback} />
        <Route path="/" component={Main} />
      </Switch >
    )
  }
}
const mapStateToProps = (store: any) => {
  const { oidc } = store;
  return {
    user: oidc.user
  };
};
export default connect(mapStateToProps)(Home);