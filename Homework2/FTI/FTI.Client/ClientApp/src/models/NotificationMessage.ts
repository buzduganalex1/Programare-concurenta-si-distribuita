export class NotificationMessage {  
    payload: string;
    type: string;
    id: string;

    constructor(Payload:string, Type: string, Id: string) {
        this.payload = Payload;
        this.type = Type;
        this.id = Id;
    }
}