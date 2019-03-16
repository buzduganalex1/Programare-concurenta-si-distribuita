export class ReceiptMessage {  
    Payload: string;
    Type: string;
    Id: string;
    /**
     *
     */
    constructor(payload:string, type: string, id: string) {
        this.Payload = payload;
        this.Type = type;
        this.Id = id;
    }
}