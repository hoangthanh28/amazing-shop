import * as React from 'react';
import { connect } from 'react-redux';
import { CallbackComponent } from 'redux-oidc';
import { push } from 'react-router-redux';
import userManager from '../auth/Oidc';
import store from '../reduxs/index';
import { User } from "oidc-client";
interface Props {
    dispatch: Function;
}

class OidcCallback extends React.Component<Props, {}> {
    successCallback = (user: User) => {
        const { dispatch } = this.props;
        // get the user's previous location, passed during signinRedirect()
        //const redirectPath = user.state.path;
        // store.dispatch(userLoaded());
        dispatch(push('/'));
    };

    errorCallback = () => {
        const { dispatch } = this.props;
        dispatch(push('/'));
    };

    render() {
        return (
            <CallbackComponent
                userManager={userManager}
                successCallback={this.successCallback}
                errorCallback={this.errorCallback}
            >
                <div />
            </CallbackComponent>
        );
    }
}

export default connect()(OidcCallback);
