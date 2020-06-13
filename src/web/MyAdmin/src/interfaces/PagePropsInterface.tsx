import ProductService from "../services/product";
import CommonService from "../services/common";

export interface PagePropsInterface {
    commonService: CommonService;
    productService: ProductService;
}

export interface StateToPropInterface {
    oidc: {
        user: {
            access_token: string;
            expires_at: number;
            id_token: string;
            scope: string;
            session_state: string;
            token_type: string;
            profile: {
                amr: string[];
                auth_time: number;
                idp: string;
                name: string;
                rememberLogin: string;
                sid: string;
                sub: string;
                tenantId: string;
                subTenantId: string;
                username: string;
                uid: string;
                MFEndpoints: string;
                operatorId: string;
            };
            expired?: boolean;
        };
    };
}
