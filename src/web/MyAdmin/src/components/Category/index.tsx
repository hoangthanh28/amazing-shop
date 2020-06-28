import React, { Component } from 'react';
import { connect } from 'react-redux'
import { loadCompleted } from '../../reduxs/actions/Category'
import { Loading, Loaded } from '../../reduxs/actions/System'
import { StateToPropInterface } from '../../interfaces/PagePropsInterface'
import AppContext from '../../AppContext';
import store from '../../reduxs';
import _ from 'lodash'
interface CategoryProps {
  user: StateToPropInterface['oidc']['user'];
  categories: [],
  loading: boolean
}
interface CategoryStates {
}

class Resource extends Component<CategoryProps, CategoryStates> {
  componentDidMount() {
    this.populateData();
  }

  renderTable(categories) {
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
          </tr>
        </thead>
        <tbody>
          {categories.map(resource =>
            <tr key={resource.id}>
              <td>{resource.id}</td>
              <td>{resource.name}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    const { loading, categories, user } = this.props;
    let contents = (loading === undefined || loading || _.isEmpty(user))
      ? <p><em>Loading...</em></p>
      : this.renderTable(categories);

    return (
      <div>
        <h3>Category</h3>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateData() {
    store.dispatch(Loading());
    const { categoryService } = this.context;
    const response = await categoryService.getAll();
    store.dispatch(loadCompleted(response));
    store.dispatch(Loaded());
  }
}

const mapStateToProps = (store: any) => {
  const { oidc, category, system } = store;
  return {
    user: oidc.user,
    categories: category.categories,
    loading: system.isLoading
  };
};
Resource.contextType = AppContext;
export default connect(mapStateToProps)(Resource);
