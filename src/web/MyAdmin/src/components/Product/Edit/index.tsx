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
      const reader = new FileReader();
      reader.onload = () => {
        this.setState({
          file: inputFile!.files,
        });
      };
      reader.readAsDataURL(inputFile!.files[0]);
    }
  };

  handleOpenCollapseBtn = () => { };
  handleSubmit(event) {
    const { product } = this.state;
    alert(product.name);
    event.preventDefault();

  }
  handleUploadFile = () => {
    const { file } = this.state;
    debugger;
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
      <form onSubmit={e => { this.handleSubmit(e) }}>
        <div className="form-group">
          <label >Product Name</label>
          <input type="name" className="form-control" value={product.name} onChange={e => {
            const { product } = this.state;
            this.setState({ product: { ...product, name: e.target.value } });
          }} />
        </div>
        <div className="input-upload">
          <div
            className="input-file"
            onClick={() => this.handleOpenFile()}
          >
            <input
              id="input-file"
              value={file ? file![0]!.name : ''}
              onChange={() => { }}
              className="f-input form-control"
              placeholder={t('COMMON.BROWSE')}
            />
          </div>
          <div
            className="fileUpload btn btn-primary"
            onClick={this.handleUploadFile}
          >
            <span>{t('BUTTON.UPLOAD')}</span>
          </div>
        </div>
        <input
          type="file"
          accept="*.*"
          multiple
          ref={node => (this.inputFile = node)}
          onChange={this.handleSelectFile}
          style={{ display: 'none' }}
        />
        <input type="submit" value="Submit" className="btn btn-primary" />
      </form >
    );
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
