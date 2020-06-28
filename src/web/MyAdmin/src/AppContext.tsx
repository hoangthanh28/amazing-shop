import React from "react";
import CommonService from './services/common';
import ProductService from './services/product';
import ResourceService from './services/resource';
import CategoryService from './services/category';

export const services = {
    commonService: new CommonService(),
    productService: new ProductService(),
    resourceService: new ResourceService(),
    categoryService: new CategoryService(),
};

const AppContext = React.createContext(services);
export default AppContext;