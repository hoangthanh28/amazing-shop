import React from "react";
import CommonService from './services/common';
import ProductService from './services/product';
import ResourceService from './services/resource';

export const services = {
    commonService: new CommonService(),
    productService: new ProductService(),
    resourceService: new ResourceService(),
};

const AppContext = React.createContext(services);
export default AppContext;