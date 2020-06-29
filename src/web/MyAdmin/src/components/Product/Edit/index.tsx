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
import { read } from 'fs/promises';
interface ProductProps {
  user: StateToPropInterface['oidc']['user'];
  loading: boolean
}
interface ProductStates {
  id?: string;
  isEditMode: boolean;
  product: Product
  file: FileList | null;
}

class EditProduct extends Component<ProductProps & WithTranslation & RouteComponentProps<{}>, ProductStates> {
  constructor(props: ProductProps & WithTranslation & RouteComponentProps<{}>) {
    super(props);
    const pathname = props.location.pathname;
    const id = props.match.params['id'];
    this.state = {
      id: id,
      isEditMode: id && pathname.indexOf('edit') > -1 ? true : false,
      product: new Product(),
      file: null
    }
  }
  inputFile: HTMLInputElement | null = null;
  componentDidMount() {
    this.populateProductData();
  }

  handleSelectFile = () => {
    const { inputFile } = this;
    if (inputFile!.files && inputFile!.files[0]) {
      this.setState({
        file: inputFile!.files,
      });
      _.forEach(inputFile?.files, file => this.readFile(file));
    }
  };
  readFile(file) {
    var reader = new FileReader();
    reader.readAsDataURL(file);
  }

  async handleSubmit(event) {
    const { history } = this.props;
    const { product, file } = this.state;
    const { productService } = this.context;
    if (file) {
      await Promise.all(Array.from(file!).map(f => productService.uploadImage(product.id, f)));
    }
    await productService.updateProduct(product.id, { ...product });
    history.push('/products');
    event.preventDefault();

  }
  handleOpenFile = () => {
    if (this.inputFile) {
      this.inputFile.click();
    }
  };
  renderProductTable() {
    const { product, file } = this.state;
    const { t } = this.props;
    return (
      <form>
        <div className="form-group">
          <label >Product Name</label>
          <input type="name" className="form-control col-3" value={product.name} onChange={e => {
            const { product } = this.state;
            this.setState({ product: { ...product, name: e.target.value } });
          }} />
        </div>
        <div className="form-group">
          <div
            className="btn btn-primary"
            onClick={() => this.handleOpenFile()}
          >
            <span>{t('BUTTON.SELECT')}</span>
          </div>
        </div>
        <div className="form-group">
          {this.renderImagePreview(file)}
        </div>
        <input
          type="file"
          accept="*.*"
          multiple
          ref={node => (this.inputFile = node)}
          onChange={this.handleSelectFile}
          style={{ display: 'none' }}
        />
        <input value="Submit" className="btn btn-primary" onClick={e => this.handleSubmit(e)} />
      </form >
    );
  }
  renderImagePreview(files) {
    return files != null ? <div className="row">
      {Array.from(files).map((x, index) => { return <img src={URL.createObjectURL(x)} className="uploadFile" key={index} /> })}
    </div> : <></>
  }
  render() {
    const { loading, user } = this.props;
    let contents = (loading === undefined || loading || _.isEmpty(user))
      ? <p><em>Loading...</em></p>
      : this.renderProductTable();

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
    this.setState({ product: { ...response } });
    store.dispatch(Loaded());
  }
}

const mapStateToProps = (store: any) => {
  const { oidc, system } = store;
  return {
    user: oidc.user,
    loading: system.isLoading
  };
};
EditProduct.contextType = AppContext;
export default withRouter(connect(mapStateToProps)(withTranslation()(EditProduct)));
