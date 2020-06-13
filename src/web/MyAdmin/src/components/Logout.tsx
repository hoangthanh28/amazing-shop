import React, { Component } from 'react';
import userManager from '../auth/Oidc'
export class Logout extends Component {
  componentDidMount() {
    userManager.getUser().then(user => {
      userManager
        .signoutRedirect({ prompt: 'login', 'id_token_hint': user?.id_token })
        .catch(err => console.log(err));
    });
  }
  render() {
    return (<div></div>);
  }
}

