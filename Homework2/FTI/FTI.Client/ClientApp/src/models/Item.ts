import { Amount } from './Amount';

export class Item {
    Description:string;
    Price:Amount;

    constructor(description:string, price: Amount) {
        this.Description = description;
        this.Price = price;
    }
}