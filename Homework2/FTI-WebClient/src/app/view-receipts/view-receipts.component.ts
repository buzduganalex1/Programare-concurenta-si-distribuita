import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message } from 'primeng/api';
import { ConvertReceiptService } from '../services/convert-receipt.service';
import { ReceiptMessage } from 'src/models/ReceiptMessage';

@Component({
  selector: 'app-view-receipts',
  templateUrl: './view-receipts.component.html',
  styleUrls: ['./view-receipts.component.css']
})

export class ViewReceiptsComponent implements OnInit {

  constructor(public convertService: ConvertReceiptService) { }
  
  messages: Message[] = [];
  messageType: string;

  private _hubConnection: HubConnection;

  ngOnInit() {
    this._hubConnection = new HubConnectionBuilder().withUrl("http://localhost:5000/notify").build();
    this._hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this._hubConnection.on('BroadcastMessage', (type: string, payload: string, id: string) => {
      console.log(payload);
      this.messages.push({ severity: type, summary: payload, id: id });
    });
  }
  
  onChange(newValue) {
    var message1 = new ReceiptMessage(newValue.summary, newValue.severity, newValue.id);

    console.log(newValue);

    this.convertService.convertReceipt(message1).subscribe(message => 
    {
      var bindedMessages = this.messages.filter(x => x.id == message.id);
      
      if(bindedMessages.length > 0){
        var bindedMessage = bindedMessages[0];
        bindedMessage.summary = message.payload;
      }
    });
  }
}
