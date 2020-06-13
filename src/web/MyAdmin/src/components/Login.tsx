import React, { Component } from 'react';
import userManager from '../auth/Oidc'
export class Login extends Component {
  componentDidMount() {
    userManager.getUser().then(user => {
      if (!user) {
        userManager.signinRedirect();
      }
    });

  }
  render() {
    return (<div></div>);
  }
}

