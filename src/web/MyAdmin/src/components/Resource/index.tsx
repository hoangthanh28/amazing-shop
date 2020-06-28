import React, { Component } from 'react';
import { connect } from 'react-redux'
import { endLoadResource } from '../../reduxs/actions/Resource'
import { Loading, Loaded } from '../../reduxs/actions/System'
import { StateToPropInterface } from '../../interfaces/PagePropsInterface'
import AppContext from '../../AppContext';
import store from '../../reduxs';
import _ from 'lodash'
interface ResourceProps {
  user: StateToPropInterface['oidc']['user'];
  resources: [],
  loading: boolean
}
interface ResourceStates {
}

class Resource extends Component<ResourceProps, ResourceStates> {
  componentDidMount() {
    this.populateResourceData();
  }

  renderResourceTable(resources) {
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
          </tr>
        </thead>
        <tbody>
          {resources.map(resource =>
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
    const { loading, resources, user } = this.props;
    console.log('resource.render', user);
    let contents = (loading === undefined || loading || _.isEmpty(user))
      ? <p><em>Loading...</em></p>
      : this.renderResourceTable(resources);

    return (
      <div>
        <h3>Resources</h3>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateResourceData() {
    store.dispatch(Loading());
    const { resourceService } = this.context;
    const response = await resourceService.getAllResources();
    store.dispatch(endLoadResource(response));
    store.dispatch(Loaded());
  }
}

const mapStateToProps = (store: any) => {
  const { oidc, resource, system } = store;
  return {
    user: oidc.user,
    resources: resource.resources,
    loading: system.isLoading
  };
};
Resource.contextType = AppContext;
export default connect(mapStateToProps)(Resource);
