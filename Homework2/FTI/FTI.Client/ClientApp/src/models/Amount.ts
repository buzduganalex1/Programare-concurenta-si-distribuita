export class Amount {
    Currency:string;
    Value:number;
    
    constructor(currency:string, value:number) {
        this.Currency = currency;
        this.Value = value;
    }
}