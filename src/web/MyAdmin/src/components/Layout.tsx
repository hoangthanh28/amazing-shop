import React, { Component } from 'react';
import { connect } from 'react-redux';
import { StateToPropInterface } from '../interfaces/PagePropsInterface';
//import NavMenu from './NavMenu';
import SideNav from './SideNav'
import Header from './Header'
import appRoutes from '../routers/index'
import CustomModal from './Modal/index'
import { withTranslation, WithTranslation } from 'react-i18next';
import { withRouter, RouteComponentProps } from 'react-router-dom';
import Message from '../models/Message';
import { DiscardMessage } from '../reduxs/actions/System'
import store from '../reduxs';
interface MainProps extends RouteComponentProps<{}> {
  user: StateToPropInterface['oidc']['user'];
  openDialog: boolean;
  message: Message
}
interface MainState {
  somethingChanged: boolean;
  forceChangeRoute: boolean;
}
class Main extends Component<MainProps & WithTranslation, MainState> {
  static displayName = Main.name;
  constructor(props: MainProps & WithTranslation) {
    super(props);
    this.state = {
      somethingChanged: false,
      forceChangeRoute: false
    };
  }

  render() {
    const { openDialog, message } = this.props;
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
          {openDialog && <CustomModal
            classHolder={message.classHolder}
            isOpen={openDialog}
            title={message.name}
            closeModal={() => { store.dispatch(DiscardMessage()) }}
            content={
              <React.Fragment>
                <p>{message.message}</p>
                {message.content}
              </React.Fragment>
            }
          />}
        </div>
      </React.Fragment>
    );
  }
}
const mapStateToProps = (store: any) => {
  const { oidc, system } = store;
  return {
    user: oidc.user,
    openDialog: system.openDialog,
    message: system.message

  };
};
export default withRouter(connect(mapStateToProps)(withTranslation()(Main)));