import { Component } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'FTI-WebClient';
  
  constructor() { }
  
  msgs: Message[] = [];

  private _hubConnection: HubConnection;

  ngOnInit(): void {
    
    this._hubConnection = new HubConnectionBuilder().withUrl("http://localhost:5000/notify").build();
    this._hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this._hubConnection.on('BroadcastMessage', (type: string, payload: string) => {
      console.log(payload);
      this.msgs.push({ severity: type, summary: payload });
    });
  }
}
