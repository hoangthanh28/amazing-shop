import React, { Component } from 'react';
import { connect } from 'react-redux'
import { endLoadProducts } from '../../reduxs/actions/Product'
import { Loading, Loaded } from '../../reduxs/actions/System'
import { StateToPropInterface } from '../../interfaces/PagePropsInterface'
import AppContext from '../../AppContext';
import store from '../../reduxs';
import _ from 'lodash'
import { withRouter, RouteComponentProps } from 'react-router-dom';
import { withTranslation, WithTranslation } from 'react-i18next';
import Product from '../../models/Product';
interface ProductProps {
  user: StateToPropInterface['oidc']['user'];
  products: Product[],
  loading: boolean
}
interface ProductStates {
}

class ProductList extends Component<ProductProps & WithTranslation & RouteComponentProps<{}>, ProductStates> {
  constructor(props: ProductProps & WithTranslation & RouteComponentProps<{}>) {
    super(props);
  }
  componentDidMount() {
    this.populateProductData();
  }

  renderProductTable(products) {
    const { history } = this.props;
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {products.map(product =>
            <tr key={product.id}>
              <td>{product.id}</td>
              <td>{product.name}</td>
              <td><button className="btn btn-primary btn-edit" onClick={() => { history.push(`/products/${product.id}/edit`) }}>Edit</button></td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {

    const { loading, products, user } = this.props;
    console.log('product.render', user);
    // console.log('loading', loading);
    let contents = (loading === undefined || loading || _.isEmpty(user))
      ? <p><em>Loading...</em></p>
      : this.renderProductTable(products);

    return (
      <div>
        <h3 id="tabelLabel">Products</h3>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateProductData() {
    store.dispatch(Loading());
    const { productService } = this.context;
    const response = await productService.getAllProducts();
    store.dispatch(endLoadProducts(response));
    store.dispatch(Loaded());
  }
}

const mapStateToProps = (store: any) => {
  const { oidc, product, system } = store;
  return {
    user: oidc.user,
    products: product.products,
    loading: system.isLoading
  };
};
ProductList.contextType = AppContext;
export default withRouter(connect(mapStateToProps)(withTranslation()(ProductList)));
