import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message } from 'primeng/api';

@Component({
  selector: 'app-view-receipts',
  templateUrl: './view-receipts.component.html',
  styleUrls: ['./view-receipts.component.css']
})

export class ViewReceiptsComponent implements OnInit {

  constructor() { }
  
  messages: Message[] = [];
  messageType: string;
  
  private _hubConnection: HubConnection;

  ngOnInit() {
    this._hubConnection = new HubConnectionBuilder().withUrl("http://localhost:5000/notify").build();
    this._hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this._hubConnection.on('BroadcastMessage', (type: string, payload: string) => {
      console.log(payload);
      this.messages.push({ severity: type, summary: payload });
    });
  }
}
