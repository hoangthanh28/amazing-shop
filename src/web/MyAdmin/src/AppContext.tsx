import React from "react";
import CommonService from './services/common';
import ProductService from './services/product';

export const services = {
    commonService: new CommonService(),
    productService: new ProductService()
};

const AppContext = React.createContext(services);

export default AppContext;