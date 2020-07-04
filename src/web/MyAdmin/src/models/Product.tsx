export default class Product {
    id: number = 0;
    name: string = '';
    images: ProductImage[] = [];
}
export class ProductImage {
    name: string = '';
    url: string = '';
    contentType: string = '';
    isEdit: boolean = true
}
