import React from 'react';
import { useTranslation } from 'react-i18next';

interface MenuItemInterface {
    icon?: string;
    idText?: string;
    url?: string;
    key?: string;
}

interface FunctionComponentInterface {
    className?: string;
    activeLink?: string;
    isShow?: boolean;
    children?: React.ReactNode;
    openMenu?: (key: string) => void;
    changePage?: (url: string) => void;
}

const MenuTitle = ({
    className,
    idText
}: MenuItemInterface & FunctionComponentInterface) => {
    const { t } = useTranslation();
    return (
    <>
      <span className="icon-holder">
          <i className={className} />
      </span>
      <span className="title">{t(idText ? idText : '')}</span>
    </>
    );
};

const NavLink = ({
    url,
    icon,
    idText,
    activeLink
}: MenuItemInterface & FunctionComponentInterface) => {
    return (
        <a className={`sidebar-link ${url === activeLink ? 'active' : ''}`}>
            <MenuTitle className={icon} idText={idText} />
        </a>
    );
};

export const DropDown = ({
    isActive,
    children
}: {
    isActive: boolean;
    children: React.ReactNode;
}) => {
    return (
        <li className={`nav-item dropdown ${isActive ? 'active' : ''}`}>
            {children}
        </li>
    );
};

export const DropDownToggle = ({
    menuItem,
    openMenu
}: {
    menuItem: MenuItemInterface;
    openMenu: FunctionComponentInterface['openMenu'];
}) => {
    return (
        <div className="dropdown-toggle" onClick={() => openMenu!(menuItem.key!)}>
            <MenuTitle className={menuItem.icon} idText={menuItem.idText} />
        </div>
    );
};

export const SubMenu = ({
    className,
    isShow,
    children
}: FunctionComponentInterface) => {
    return (
        <ul className={`dropdown-menu ${className} ${isShow ? 'show' : ''}`}>
            {children}
        </ul>
    );
};

export const MenuItem = ({
    menuItem,
    changePage,
    className = '',
    activeLink
}: { menuItem: MenuItemInterface } & FunctionComponentInterface) => {
    return (
        <li
            className={className}
            key={menuItem.idText}
            onClick={() => changePage!(menuItem.url!)}
        >
            <NavLink
                url={menuItem.url}
                icon={menuItem.icon}
                idText={menuItem.idText}
                activeLink={activeLink}
            />
        </li>
    );
};

export const isActive = (
    activeMenu: string,
    keyMenu: string,
    pathname: string
): boolean => {
    if (activeMenu === keyMenu) {
        return true;
    }
    if (activeMenu !== keyMenu) {
        return false;
    }
    return pathname.includes(`/${keyMenu}/`);
};
