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
  jsonMessages :ReceiptMessage[] = [];
  xmlMessages :ReceiptMessage[] = [];
  ptMessages :ReceiptMessage[] = [];
  messageType: string;
  productionUrl: string;
  serverUrl: string;
  private _hubConnection: HubConnection;

  ngOnInit() {
    this.productionUrl = "https://ftiapi.azurewebsites.net/notify";
    this.serverUrl = "http://localhost:5000/notify";
    this._hubConnection = new HubConnectionBuilder().withUrl(this.productionUrl).build();
    this._hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this._hubConnection.on('BroadcastMessage', (type: string, payload: string, id: string) => {
      console.log(payload);
      this.messages.push({ severity: type, summary: payload, id: id });

      this.convertService.convertReceipt(new ReceiptMessage(payload,"Xml", id)).subscribe(x =>{
        this.xmlMessages.push(new ReceiptMessage(x.payload, x.type, x.id))
      });
      this.convertService.convertReceipt(new ReceiptMessage(payload,"Json", id)).subscribe(x =>{
        this.jsonMessages.push(new ReceiptMessage(x.payload, x.type, x.id))
      });
      this.convertService.convertReceipt(new ReceiptMessage(payload,"PlainText", id)).subscribe(x =>{
        this.ptMessages.push(new ReceiptMessage(x.payload, x.type, x.id))
      });
    });
  }
}
