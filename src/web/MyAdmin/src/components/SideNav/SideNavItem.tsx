interface SideNavItemInterface {
    key: string;
    icon: string;
    idText: string;
    sub: {
        url?: string;
        icon: string;
        idText: string;
        key?: string;
        sub?: {
            url: string;
            icon: string;
            idText: string;
            key?: string;
            sub?: {
                url: string;
                icon: string;
                idText: string;
                key?: string;
            }[];
        }[];
    }[];
}

export const SideNavItem: SideNavItemInterface[] = [
    {
        key: 'resources',
        icon: 'icon icon-resource-ico',
        idText: 'NAV.RESOURCE',
        sub: [
            {
                url: '/resources',
                icon: 'icon icon-debt',
                idText: 'NAV.RESOURCE.VIEW_SEARCH'
            }
        ]
    }
]
