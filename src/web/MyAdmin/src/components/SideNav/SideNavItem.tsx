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
    },
    {
        key: 'categories',
        icon: 'icon icon-resource-ico',
        idText: 'NAV.CATEGORY',
        sub: [
            {
                url: '/categories',
                icon: 'icon icon-debt',
                idText: 'NAV.CATEGORY.VIEW_SEARCH'
            }
        ]
    },
    {
        key: 'products',
        icon: 'icon icon-resource-ico',
        idText: 'NAV.PRODUCT',
        sub: [
            {
                url: '/products',
                icon: 'icon icon-debt',
                idText: 'NAV.PRODUCT.VIEW_SEARCH'
            },
            {
                url: '/products/create',
                icon: 'icon icon-debt',
                idText: 'NAV.PRODUCT.CREATE'
            }
        ]
    }
]
