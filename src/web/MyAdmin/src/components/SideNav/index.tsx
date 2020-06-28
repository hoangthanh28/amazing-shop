import React from 'react';
import { connect } from 'react-redux';
import { withRouter, RouteComponentProps } from 'react-router-dom';
import SidenavContent from './SidenavContent';

interface Props extends RouteComponentProps {
}

interface State {
    isClick: boolean;
    isCollapsed: boolean;
}


class SideNav extends React.PureComponent<Props, State> {
    isMobile: boolean;

    constructor(props: Props) {
        super(props);
        const element = document.body;
        //window.innerWidth <= 1201 && element.classList.add('is-collapsed');

        this.state = {
            isClick: true,
            isCollapsed: document.body.className !== 'is-collapsed'
        };
        this.isMobile = navigator.userAgent.match('Mobile') ? true : false;
    }

    handleHover = (isCollapsed: boolean) => {
        const element = document.body;
        this.setState({
            isCollapsed: isCollapsed
        });
        element.classList.toggle('is-collapsed');
    };

    addClassSidebar = () => {
        const element = document.body;
        element.classList.toggle('is-collapsed');
        this.setState({
            isCollapsed: document.body.className.includes('is-collapsed')
        });
        if (window.innerWidth > 1201) {
            this.setState(prevState => {
                return {
                    isClick: !prevState.isClick
                };
            });
        }
    };

    render() {
        //const { conman } = this.props;
        const { isClick, isCollapsed } = this.state;

        return (
            <nav className="sidebar">
                <div className="sidebar-inner">
                    <div className="sidebar-logo" onClick={this.addClassSidebar}>
                        <div className="logo-box">
                            <div className="logo-menu">
                                <span />
                                <span />
                                <span />
                            </div>
                        </div>
                        <div className="logo-mb">
                            <a>
                                <img
                                    src=''
                                    alt=""
                                />
                            </a>
                        </div>
                    </div>
                    <SidenavContent
                        isCollapsed={isCollapsed}
                        clicked={isClick}
                        isMobile={this.isMobile}
                        handleHover={this.handleHover}
                        addClassSidebar={this.addClassSidebar}
                    />
                </div>
            </nav>
        );
    }
}

const mapStateToProps = ({ conman, auth }) => {
    return { conman, auth };
};

export default withRouter(
    connect(
        mapStateToProps,
        {}
    )(SideNav)
);
