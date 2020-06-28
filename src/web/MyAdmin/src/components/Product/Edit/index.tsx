import React, { Component } from 'react';
import { connect } from 'react-redux'
import { endLoadProduct } from '../../../reduxs/actions/Product'
import { Loading, Loaded } from '../../../reduxs/actions/System'
import { StateToPropInterface } from '../../../interfaces/PagePropsInterface'
import AppContext from '../../../AppContext';
import store from '../../../reduxs';
import _ from 'lodash'
import { withRouter, RouteComponentProps } from 'react-router-dom';
import { withTranslation, WithTranslation } from 'react-i18next';
import Product from '../../../models/Product';
interface ProductProps {
  user: StateToPropInterface['oidc']['user'];
  product: Product,
  loading: boolean
}
interface ProductStates {
  id?: string;
  isEditMode: boolean;
}

class EditProduct extends Component<ProductProps & WithTranslation & RouteComponentProps<{}>, ProductStates> {
  constructor(props: ProductProps & WithTranslation & RouteComponentProps<{}>) {
    super(props);
    const pathname = props.location.pathname;
    const id = props.match.params['id'];
    this.state = {
      id: id,
      isEditMode: id && pathname.indexOf('edit') > -1 ? true : false
    }
  }
  componentDidMount() {
    this.populateProductData();
  }

  renderProductTable(product: Product) {
    return (
      <div><label>{product.id}</label></div>
    );
  }

  render() {

    const { loading, product, user } = this.props;
    console.log('product.render', user);
    // console.log('loading', loading);
    let contents = (loading === undefined || loading || _.isEmpty(user))
      ? <p><em>Loading...</em></p>
      : this.renderProductTable(product);

    return (
      <div>
        <h1 id="tabelLabel">Product</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateProductData() {
    store.dispatch(Loading());
    const { productService } = this.context;
    const { id } = this.state;
    const response = await productService.getProductById(id);
    store.dispatch(endLoadProduct(response));
    store.dispatch(Loaded());
  }
}

const mapStateToProps = (store: any) => {
  const { oidc, product, system } = store;
  return {
    user: oidc.user,
    product: product.product,
    loading: system.isLoading
  };
};
EditProduct.contextType = AppContext;
export default withRouter(connect(mapStateToProps)(withTranslation()(EditProduct)));
