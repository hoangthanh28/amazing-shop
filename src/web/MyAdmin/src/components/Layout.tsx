import React, { Component } from 'react';
import { connect } from 'react-redux';
import { StateToPropInterface } from '../interfaces/PagePropsInterface';
import NavMenu from './NavMenu';
import appRoutes from '../routers/index'
import { Container } from 'reactstrap';
interface MainProps {
  user: StateToPropInterface['oidc']['user'];
}
class Main extends Component<MainProps, {}> {
  static displayName = Main.name;
  render() {
    return (
      <div>
        <NavMenu />
        <Container>
          {appRoutes}
        </Container>
      </div>
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