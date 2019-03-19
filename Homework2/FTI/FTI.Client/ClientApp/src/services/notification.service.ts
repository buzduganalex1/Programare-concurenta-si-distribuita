import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection } from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  public hubConnection: HubConnection;

  constructor() { 
    this.hubConnection = new HubConnectionBuilder().withUrl(environment.host + "/notify").build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));
  }
}
