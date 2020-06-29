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
export const PRODUCT_DETAIL = `${API_ENDPOINT}/prd/products/{0}`;
export const PRODUCT_DETAIL_IMAGE = `${API_ENDPOINT}/prd/products/{0}/images`;
export const RESOURCE_LIST = `${API_ENDPOINT}/prd/resources`;
export const CATEGORY_LIST = `${API_ENDPOINT}/prd/categories`;        