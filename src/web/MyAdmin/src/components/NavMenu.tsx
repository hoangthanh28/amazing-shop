import React, { Component } from 'react'
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import './NavMenu.css';
import { Link } from 'react-router-dom';
import { StateToPropInterface } from '../interfaces/PagePropsInterface'
import { connect } from 'react-redux';
import _ from 'lodash'
interface NavProps {
    user: StateToPropInterface['oidc']['user'];
}
interface NavStates {
    collapsed: boolean,
}
class NavMenu extends Component<NavProps, NavStates> {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        let { user } = this.props;
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/">SPA Shop</NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/products">Products</NavLink>
                                </NavItem>
                                {(user && user!.expired === false) && <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/logout">Logout</NavLink>
                                </NavItem>}
                                {(_.isEmpty(user) || user!.expired === true) && <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/login">Login</NavLink>
                                </NavItem>}
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
const mapStateToProps = (store: any) => {
    const { oidc } = store;
    return {
        user: oidc.user
    };
};
export default connect(mapStateToProps)(NavMenu);