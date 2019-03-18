import { Item } from './Item';
import { Amount } from './Amount';

export class Receipt {  
  CustomerNumber: string;
  Items: Item[] = [];
  Total: Amount;
  
  constructor() {
      this.Total = new Amount("EUR", 0);
  }
  
  get total():Amount{
    var sum = this.Items.reduce((sum, current) => sum + current.Price.Value, 0);

    return new Amount("EUR", sum);
  }
}