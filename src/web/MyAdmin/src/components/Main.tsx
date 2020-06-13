import React, { Component } from 'react';
import { connect } from 'react-redux';
import { StateToPropInterface } from '../interfaces/PagePropsInterface';
import { Login } from '../components/Login'
import Layout from '../components/Layout'
interface MainProps {
  user: StateToPropInterface['oidc']['user'];
}
class Main extends Component<MainProps, {}> {
  static displayName = Layout.name;

  render() {
    const { user } = this.props;
    return (!user || user.expired) ? <Login /> : <Layout />
  }
}
const mapStateToProps = (store: any) => {
  const { oidc } = store;
  return {
    user: oidc.user
  };
};
export default connect(mapStateToProps)(Main);