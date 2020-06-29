import React, { Component } from 'react';
import userManager from '../auth/Oidc'
export class Login extends Component {
  Login() {
    userManager.getUser().then(user => {
      if (!user) {
        userManager.signinRedirect();
      }
    });

  }
  render() {
    return (<div className="login-container login-bg">
      <div id="overlay" className="overlay"></div>
      <div className="wrapper"><p>Amazing! Stay tune.</p><button type="button" onClick={this.Login}>Let's begin</button></div>
    </div>);
  }
}

