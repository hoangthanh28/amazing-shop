import React from 'react';
import { withRouter, RouteComponentProps } from 'react-router-dom';

import Accordion from './Accordion';

import { SideNavItem } from './SideNavItem';
import {
    DropDown,
    DropDownToggle,
    SubMenu,
    MenuItem,
    isActive
} from './common';

interface SideNavProps extends RouteComponentProps {
    clicked: boolean;
    isCollapsed: boolean;
    isMobile: boolean;
    handleHover: (hover: boolean) => void;
    addClassSidebar: () => void;
}

interface SideNavState {
    active: string;
    showSubMenu: string;
    showSubMenu1: string;
    claims: string[];
    activeLink: string;
}

class SidenavContent extends React.Component<SideNavProps, SideNavState> {
    constructor(props: SideNavProps) {
        super(props);
        const {
            isMobile,
            isCollapsed,
            location: { pathname }
        } = props;
        const keyName = pathname.split('/')[1];
        this.state = {
            active: !isMobile && isCollapsed && keyName ? keyName : '',
            showSubMenu: '',
            showSubMenu1: '',
            claims: [],
            activeLink: pathname
        };
    }

    componentDidMount() {

    }

    componentWillUnmount() {

    }

    static getDerivedStateFromProps(
        nextProps: SideNavProps,
        prevState: SideNavState
    ) {
        const { isCollapsed } = nextProps;
        if (isCollapsed) {
            return {
                active: document.body.className.includes('is-collapsed')
                    ? ''
                    : prevState.active
            };
        }
        return null;
    }

    setClaims = (claims: string[]) => {
        const {
            location: { pathname }
        } = this.props;
        this.setState({
            claims
        });
        if (!pathname.split('/')[2]) {
            this.setState({
                activeLink: ''
            });
        }
    };

    handleOpenMenu = (key: string) => {
        const { isMobile, isCollapsed, addClassSidebar } = this.props;
        if (isMobile) {
            isCollapsed && addClassSidebar();
        }
        this.setState(prevState => {
            return {
                active: prevState.active === key ? '' : key,
                showSubMenu: '',
                showSubMenu1: ''
            };
        });
    };

    onMouseEnter = () => {
        const {
            handleHover,
            isCollapsed,
            location: { pathname }
        } = this.props;
        if (isCollapsed) {
            handleHover(false);
            const keyname = pathname.split('/')[1];
            this.setState({
                active: keyname ? keyname : ''
            });
        }
    };

    onMouseLeave = () => {
        const {
            isCollapsed
        } = this.props;
        if (!isCollapsed) {
            this.setState({
                active: '',
                showSubMenu: '',
                showSubMenu1: ''
            });
            this.props.handleHover(true);
        }
    };

    handleOpenSubMenu1 = (key: string) => {
        this.setState(prevState => {
            return {
                showSubMenu: prevState.showSubMenu === key ? '' : key,
                showSubMenu1: ''
            };
        });
    };

    handleOpenSubMenu2 = (key: string) => {
        this.setState(prevState => {
            return {
                showSubMenu1: prevState.showSubMenu1 === key ? '' : key
            };
        });
    };

    handleChangePage = (url: string) => {
        const { history, isMobile, addClassSidebar, isCollapsed } = this.props;

        // Reset Iframe Subtenant Id
        localStorage.iframeSubtenantId = '';

        history.push(url);
        if (isMobile) {
            !isCollapsed && addClassSidebar();
        }
        this.setState({
            activeLink: url
        });
    };

    checkClaim = (rightName: string) => {
        const { claims } = this.state;
        let hasRight = false;
        const existRight = claims.find(c => c === rightName);
        if (existRight && existRight.length > 0) {
            hasRight = true;
        }
        return hasRight;
    };

    checkClaimLevel0 = sideNavItem => {
        let hasRight = true;
        // sideNavItem.sub.map(menuItem => {
        //     if (menuItem.sub) {
        //         menuItem.sub.map(subItem => {
        //             if (subItem.sub) {
        //                 subItem.sub.map(deepSubItem => {
        //                     const checkRight = this.checkClaim(deepSubItem.rightName);
        //                     if (checkRight) {
        //                         hasRight = true;
        //                     }
        //                 });
        //             } else {
        //                 const checkRight = this.checkClaim(subItem.rightName);
        //                 if (checkRight) {
        //                     hasRight = true;
        //                 }
        //             }
        //         });
        //     } else {
        //         const checkRight = this.checkClaim(menuItem.rightName);
        //         if (checkRight) {
        //             hasRight = true;
        //         }
        //     }
        // });
        return hasRight;
    };

    checkClaimLevel1 = menuItem => {
        let hasRight = true;
        // menuItem.sub.map(subItem => {
        //     if (subItem.sub) {
        //         subItem.sub.map(deepSubItem => {
        //             const checkRight = this.checkClaim(deepSubItem.rightName);
        //             if (checkRight) {
        //                 hasRight = true;
        //             }
        //         });
        //     } else {
        //         const checkRight = this.checkClaim(subItem.rightName);
        //         if (checkRight) {
        //             hasRight = true;
        //         }
        //     }
        // });
        return hasRight;
    };

    checkClaimLevel2 = subItem => {
        let hasRight = true;
        // subItem.sub.map(deepSubItem => {
        //     const checkRight = this.checkClaim(deepSubItem.rightName);
        //     if (checkRight) {
        //         hasRight = true;
        //     }
        // });
        return hasRight;
    };

    render() {
        const {
            clicked,
            location: { pathname }
        } = this.props;
        const { active, showSubMenu, showSubMenu1, activeLink } = this.state;
        return (
            <ul
                className="sidebar-menu"
                onMouseEnter={() => !clicked && this.onMouseEnter()}
                onMouseLeave={() => !clicked && this.onMouseLeave()}
            >
                {SideNavItem.map(sideNavItem => {
                    return this.checkClaimLevel0(sideNavItem) ? (
                        <DropDown
                            key={sideNavItem.key}
                            isActive={isActive(active, sideNavItem.key, pathname)}
                        >
                            <DropDownToggle
                                menuItem={sideNavItem}
                                openMenu={this.handleOpenMenu}
                            />
                            <Accordion isShow={active === sideNavItem.key}>
                                <SubMenu className="sub-menu" isShow={false}>
                                    {/* sub-menu level 1 */}
                                    {sideNavItem.sub.map(menuItem => {
                                        return !menuItem.sub ? (
                                            // check claims
                                            // this.checkClaim(menuItem.rightName!) ? (
                                            //     <MenuItem
                                            //         key={menuItem.idText}
                                            //         menuItem={menuItem}
                                            //         className="nav-item"
                                            //         changePage={this.handleChangePage}
                                            //         activeLink={activeLink}
                                            //     />
                                            // ) : null
                                            <MenuItem
                                                key={menuItem.idText}
                                                menuItem={menuItem}
                                                className="nav-item"
                                                changePage={this.handleChangePage}
                                                activeLink={activeLink}
                                            />
                                        ) : // check claims
                                            this.checkClaimLevel1(menuItem) ? (
                                                <DropDown
                                                    isActive={showSubMenu === menuItem.key}
                                                    key={menuItem.idText}
                                                >
                                                    <DropDownToggle
                                                        menuItem={menuItem}
                                                        openMenu={this.handleOpenSubMenu1}
                                                    />
                                                    <Accordion isShow={showSubMenu === menuItem.key}>
                                                        <SubMenu className="sub-menu-1" isShow={true}>
                                                            {/* sub-menu level 2 */}
                                                            {menuItem.sub.map(subItem => {
                                                                return !subItem.sub ? (
                                                                    // check claims
                                                                    //this.checkClaim(subItem.rightName!) ? (
                                                                    <MenuItem
                                                                        key={subItem.idText}
                                                                        menuItem={subItem}
                                                                        changePage={this.handleChangePage}
                                                                        activeLink={activeLink}
                                                                    />
                                                                    //) : null
                                                                ) : // check claims
                                                                    this.checkClaimLevel2(subItem) ? (
                                                                        <DropDown
                                                                            key={subItem.idText}
                                                                            isActive={showSubMenu === menuItem.key}
                                                                        >
                                                                            <DropDownToggle
                                                                                menuItem={subItem}
                                                                                openMenu={this.handleOpenSubMenu2}
                                                                            />
                                                                            <SubMenu
                                                                                className="sub-menu-2"
                                                                                isShow={showSubMenu1 === subItem.key}
                                                                            >
                                                                                {subItem.sub.map(deepSubItem => {
                                                                                    return (
                                                                                        // check claims
                                                                                        //this.checkClaim(
                                                                                        //    deepSubItem.rightName!
                                                                                        // ) ? (
                                                                                        <MenuItem
                                                                                            key={deepSubItem.idText}
                                                                                            menuItem={deepSubItem}
                                                                                            changePage={this.handleChangePage}
                                                                                            activeLink={activeLink}
                                                                                        />
                                                                                        //     ) : null
                                                                                    );
                                                                                })}
                                                                            </SubMenu>
                                                                        </DropDown>
                                                                    ) : null;
                                                            })}
                                                        </SubMenu>
                                                    </Accordion>
                                                </DropDown>
                                            ) : null;
                                    })}
                                </SubMenu>
                            </Accordion>
                        </DropDown>
                    ) : null;
                })}
            </ul>
        );
    }
}

export default withRouter(SidenavContent);
