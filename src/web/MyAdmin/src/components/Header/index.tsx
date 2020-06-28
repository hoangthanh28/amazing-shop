import React from 'react';
import { connect } from 'react-redux';
import { withRouter, RouteComponentProps, NavLink } from 'react-router-dom';
import Language from './../../models/Language'
import i18n from 'i18next';
import { withTranslation, WithTranslation } from 'react-i18next';
import { StateToPropInterface } from './../../interfaces/PagePropsInterface'
import _ from 'lodash'
import './Header.scss';
interface Props extends RouteComponentProps<{}> {
    user: StateToPropInterface['oidc']['user'];
}

interface State {
    isOpenMenuLanguages: boolean;
    isOpenMenuProfile: boolean;
    languages: Language[];
}

const languages = [
    {
        locale: 'en-US',
        name: 'TOPMENU.LANGUAGE.EN'
    },
    {
        locale: 'vi-vn',
        name: 'TOPMENU.LANGUAGE.VI'
    }
];

class Header extends React.Component<Props & WithTranslation, State> {
    languageRef;
    profileRef;
    constructor(props: Props & WithTranslation) {
        super(props);
        this.state = {
            isOpenMenuLanguages: false,
            isOpenMenuProfile: false,
            languages: languages
        };
        this.languageRef = React.createRef();
        this.profileRef = React.createRef();

        // const findLanguageName = languages.find(e => e.locale === i18n.language);
        // const currentLanguageName = findLanguageName ? findLanguageName : languages[0];
        // if (!findLanguageName) {
        //     i18n.changeLanguage(currentLanguageName.locale);
        // }
    }

    componentDidMount() {
        document.addEventListener('click', this.handleClickOutSide);
    }

    handleClickOutSide = e => {
        if (
            this.profileRef &&
            this.profileRef.current &&
            !this.profileRef.current.contains(e.target)
        ) {
            this.setState({
                isOpenMenuProfile: false
            });
        }
        if (
            this.languageRef &&
            this.languageRef.current &&
            !this.languageRef.current.contains(e.target)
        ) {
            this.setState({
                isOpenMenuLanguages: false
            });
        }
    };

    onToggleMenuLanguages = () => {
        const { isOpenMenuLanguages } = this.state;
        this.setState({
            isOpenMenuLanguages: !isOpenMenuLanguages
        });
    };

    onToggleMenuProfile = () => {
        const { isOpenMenuProfile } = this.state;
        this.setState({
            isOpenMenuProfile: !isOpenMenuProfile
        });
    };

    switchLanguage(payload) {
        const iframe = document.getElementById('iframe-id') as HTMLIFrameElement;
        if (iframe) {
            let langCode = 'en';
            if (payload.locale && payload.locale !== '') {
                langCode = payload.locale.split('-')[0];
            }
            if (langCode === 'zh') {
                langCode = 'cn';
            }
            if (iframe.contentWindow) {
                iframe.contentWindow.postMessage({
                    action: 'save',
                    key: 'currentLanguage',
                    value: langCode
                }, '*');
            }
        }
        i18n.changeLanguage(payload.locale);
    }

    addClassSidebar = () => {
        const element = document.body;
        element.classList.toggle('is-collapsed');
    };

    signOut() {
        /* eslint @typescript-eslint/no-explicit-any: 0 */
        const { t } = this.props;
        const content = (
            <React.Fragment>
                <p>{t('PAGE.LOGOUT.WOULD_YOU_LIKE')}</p>
                <div className="modalSaas__row--btn">
                    <button
                        className="btn btn-primary text-uppercase"
                        onClick={() => {
                            // do something
                        }}
                    >
                        {t('BUTTON.YES')}
                    </button>
                    <button
                        className="btn btn-dark text-uppercase"
                        onClick={() => {// do something
                        }}
                    >
                        {t('BUTTON.NO')}
                    </button>
                </div>
            </React.Fragment>
        );
    }

    render() {
        const { t, user } = this.props;
        const { languages } = this.state;
        const currentLanguageName = languages.find(e => e.locale === i18n.language);

        return (
            <header>
                <div className="header">
                    <div className="header__btn">
                        <div className="btn-sidebar btn" onClick={this.addClassSidebar}>
                            <img src='' alt="" />
                        </div>
                    </div>
                    <ul className="header__left">
                        <li className="header__logo">
                            <a>
                                <img
                                    onClick={() => this.props.history.push('/')}
                                    src=''
                                    alt="logo"
                                />
                            </a>
                        </li>
                        <li className="header__search search-box">
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <button className="icon-search-ico" />
                                </div>
                                <input
                                    className="form-control"
                                    type="text"
                                    placeholder={t('TOPMENU.SEARCH.PLACEHOLDER')}
                                />
                            </div>
                        </li>
                    </ul>
                    <ul className="header__right">
                        <li
                            className="header__languages"
                            onClick={() => this.onToggleMenuLanguages()}
                            ref={this.languageRef}
                        >
                            <div className="multi-box">
                                <span className="icon icon-global-ico">
                                    <span className="text-hidden-mb">{t(currentLanguageName ? currentLanguageName.name : '')}</span>
                                </span>
                                <ul
                                    className={`${
                                        this.state.isOpenMenuLanguages ? 'show' : ''
                                        } drop-box dropdown-content`}
                                >
                                    {languages.map((item, index) => (
                                        <li
                                            onClick={() => this.switchLanguage(item)}
                                            className={item.locale === i18n.language ? 'hidden' : ''}
                                            key={index}
                                        >
                                            <a>{t(item.name)}</a>
                                        </li>
                                    ))}
                                </ul>
                            </div>
                        </li>
                        <li className="header__notifications">
                            <a>
                                <span className="icon-noti-icon" />
                            </a>
                        </li>
                        <li
                            className="header__account pr-0"
                            onClick={() => this.onToggleMenuProfile()}
                            ref={this.profileRef}
                        >
                            {user ? (
                                <React.Fragment>
                                    <a className="box-account">
                                        <div className="account-img">
                                            <img src={user.profile.avatar} alt="" />
                                        </div>
                                        <div className="account-name" title={user.profile.name}>
                                            {user.profile.name}
                                        </div>
                                        <span className="icon icon-arrow-down-icon" />
                                    </a>
                                    <div className="multi-box">
                                        <ul
                                            className={`${
                                                this.state.isOpenMenuProfile ? 'show' : ''
                                                } drop-box dropdown-content`}
                                        >
                                            <li>
                                                <NavLink to="/users/profile">
                                                    <i className="icon icon-edit-profile" />
                                                    <span>{t('TOPMENU.USER.EDIT_PROFILE')}</span>
                                                </NavLink>
                                            </li>
                                            <li className="logout" onClick={() => this.signOut()}>
                                                <a>
                                                    <i className="icon icon-logout-1" />
                                                    <span>{t('TOPMENU.USER.LOG_OUT')}</span>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </React.Fragment>
                            ) : (
                                    <></>
                                )}
                        </li>
                    </ul>
                </div>
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

export default withRouter(
    connect(
        mapStateToProps
    )(withTranslation()(Header))
);
