import store from '../reduxs/index';
import axios from 'axios';
import _ from 'lodash'
export default class CommonService {
    fetchData(api: string) {
        return axios
            .get(api, { headers: this.getHeader() })
            .then(res => res.data)
            .catch(err => this.handleErrorHttp(err));
    }

    postData(api: string, data) {
        return axios
            .post(api, data, { headers: this.getHeader() })
            .then(res => res.data)
            .catch(err => this.handleErrorHttp(err));
    }

    putData(api: string, data) {
        return axios
            .put(api, data, { headers: this.getHeader() })
            .then(res => res.data)
            .catch(err => this.handleErrorHttp(err));
    }

    deleteData(api: string) {
        return axios
            .delete(api, { headers: this.getHeader() })
            .then(res => res.data)
            .catch(err => this.handleErrorHttp(err));
    }
    getHeaderFormData() {
        return {
            'Content-Type': 'multipart/form-data;charset=UTF-8',
            accept: 'application/json',
            Authorization: `Bearer ${this.getToken()}`
        };
    }
    getHeader(hasCache: boolean = true) {
        const cacheControl = !hasCache ? { 'cache-control': 'no-cache' } : {};
        return {
            'Content-Type': 'application/json;charset=UTF-8',
            accept: 'application/json',
            Authorization: `Bearer ${this.getToken()}`,
            ...cacheControl
        }
    }
    getToken() {
        const reduxState = store.getState();
        const { oidc } = reduxState;
        return (oidc as any).user.access_token;
    }

    handleErrorHttp(err) {
        const errorResponse = err.response ? err.response.data : err.response;
        if (!_.isEmpty(err)) {
            console.error(err);
        }
        throw errorResponse;
    }
}
