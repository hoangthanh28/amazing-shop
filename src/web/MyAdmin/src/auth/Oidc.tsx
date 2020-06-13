/*eslint @typescript-eslint/camelcase: ["error", {properties: "never"}]*/

import { createUserManager } from 'redux-oidc';
import { IDENTITY_ENDPOINT } from '../constants/Constants';

declare var window: any; /* eslint @typescript-eslint/no-explicit-any: 0 */

const identityServer = IDENTITY_ENDPOINT;
const domainURL = `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}`;
const scopes = window._env.SCOPES;
const userManagerConfig = {
    authority: identityServer,
    client_id: window._env.CLIENT_ID,
    redirect_uri: `${domainURL}/callback`,
    silent_redirect_uri: `${domainURL}/silent`,
    post_logout_redirect_uri: `${domainURL}`,
    response_type: 'code',
    scope: scopes,
    automaticSilentRenew: true,
    filterProtocolClaims: true,
    loadUserInfo: true,
    monitorSession: false
};

const userManager = createUserManager(userManagerConfig);

export default userManager;
