import React, { Component } from 'react';
import { connect } from 'react-redux'
import { endLoadProducts } from '../reduxs/actions/Product'
import { Loading, Loaded } from '../reduxs/actions/System'
import { StateToPropInterface } from '../interfaces/PagePropsInterface'
import AppContext from '../AppContext';
import store from '../reduxs';
import _ from 'lodash'
interface ProductProps {
  user: StateToPropInterface['oidc']['user'];
  products: [],
  loading: boolean
}
interface ProductStates {
}

class Product extends Component<ProductProps, ProductStates> {
  componentDidMount() {
    this.populateProductData();
  }

  renderProductTable(products) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
          </tr>
        </thead>
        <tbody>
          {products.map(product =>
            <tr key={product.id}>
              <td>{product.id}</td>
              <td>{product.name}</td>
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
        <h1 id="tabelLabel">Products</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateProductData() {
    console.log('populateProductData', this.props.user);
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
Product.contextType = AppContext;
export default connect(mapStateToProps)(Product);
