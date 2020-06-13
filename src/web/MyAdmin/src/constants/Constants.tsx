declare var window: Record<string, Record<string, string>>;
const identityURL = `${window.location.protocol}//id.${
    window.location.hostname
    }${window.location.port ? `:${window.location.port}` : ''}`;
const domainURL = `${window.location.protocol}//api.${
    window.location.hostname
    }${window.location.port ? `:${window.location.port}` : ''}`;
export const IDENTITY_ENDPOINT =
    identityURL.indexOf('localhost') > -1
        ? `${window._env.AUTHORITY}`
        : identityURL;
export const API_ENDPOINT =
    domainURL.indexOf('localhost') > -1
        ? `${window._env.API_URL}`
        : domainURL;
export const PRODUCT_LIST = `${API_ENDPOINT}/prd/products`;        