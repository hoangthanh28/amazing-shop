import React, { Component } from 'react';
import { connect } from 'react-redux';
import { StateToPropInterface } from '../interfaces/PagePropsInterface';
//import NavMenu from './NavMenu';
import SideNav from './SideNav'
import Header from './Header'
import appRoutes from '../routers/index'
import { Container } from 'reactstrap';
interface MainProps {
  user: StateToPropInterface['oidc']['user'];
}
class Main extends Component<MainProps, {}> {
  static displayName = Main.name;
  render() {
    return (
      <React.Fragment>
        <SideNav />
        <div className="page-container">
          <Header />
          <main className="main-content">
            <div className="container1">
              {appRoutes}
            </div>
          </main>
        </div>
      </React.Fragment>
    );
  }
}
const mapStateToProps = (store: any) => {
  const { oidc } = store;
  return {
    user: oidc.user
  };
};
export default connect(mapStateToProps)(Main);