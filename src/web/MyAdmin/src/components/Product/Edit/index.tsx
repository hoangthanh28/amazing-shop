import React, { Component } from 'react';
import { connect } from 'react-redux'
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
    }
  }
  inputFile: HTMLInputElement | null = null;
  componentDidMount() {
    this.populateProductData();
  }

  handleSelectFile = () => {
    const { inputFile } = this;
    if (inputFile!.files && inputFile!.files[0]) {
      _.forEach(inputFile?.files, file => this.readFile(file));
      const { product, product: { images } } = this.state;
      const { productService } = this.context;
      const fileUploadRequests = Array.from(inputFile!.files!).map(f => productService.uploadImage(f));
      Promise.all(fileUploadRequests).then(result => {
        const newImages = result.map(r => ({ name: r.name, url: r.payload, contentType: r.contentType, isEdit: false }));
        newImages.forEach(img => {
          images.push(img);
        });
        this.setState({ product: { ...product, images: images } })
      });
    }
  };
  readFile(file) {
    const reader = new FileReader();
    reader.readAsDataURL(file);

  }

  async handleSubmit(event) {
    const { history } = this.props;
    const { product } = this.state;
    const { productService } = this.context;
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
    const { product } = this.state;
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
          {this.renderImagePreview()}
        </div>
        <input
          type="file"
          accept="*.*"
          multiple
          ref={node => (this.inputFile = node)}
          onChange={this.handleSelectFile}
          style={{ display: 'none' }}
        />
        <input className="btn btn-primary" onClick={(e) => this.handleSubmit(e)} defaultValue='Submit' />
      </form >
    );
  }
  removeProductImage(image) {
    const { product, product: { images } } = this.state;
    this.setState({ product: { ...product, images: images.filter(x => x !== image) } });
    // file?.item()?.slice()
  }
  renderImagePreview() {
    const { product: { images } } = this.state;
    return images && images.length ? <div className="row">
      {images.map((image, index) => { return <span className="product-image-edit" key={index} ><img src={image.url} className="uploadFile" key={index} /><button type="button" aria-label="Remove" onClick={() => this.removeProductImage(image)}><span className="icon icon-close-ico" aria-hidden="true"></span></button></span> })}
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
