import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message } from 'primeng/api';
import { ConvertReceiptService } from '../../services/convert-receipt.service';
import { NotificationMessage } from 'src/models/NotificationMessage';
import { environment } from 'src/environments/environment';
import { NotificationService } from 'src/services/notification.service';

@Component({
  selector: 'app-view-receipts',
  templateUrl: './view-receipts.component.html',
  styleUrls: ['./view-receipts.component.css']
})

export class ViewReceiptsComponent implements OnInit {

  constructor(public conversionService: ConvertReceiptService, private notificationService: NotificationService) { }
  
  jsonMessages :NotificationMessage[] = [];
  xmlMessages :NotificationMessage[] = [];
  ptMessages :NotificationMessage[] = [];
  totalEarnings :string;

  ngOnInit() {
    this.notificationService.hubConnection.on('BroadcastMessage', (type: string, payload: string, id: string) => {
      this.conversionService.convertReceipt(new NotificationMessage(payload,"Xml", id)).subscribe(x =>{
        this.xmlMessages.push(new NotificationMessage(x.payload, x.type, x.id))
      });

      this.conversionService.convertReceipt(new NotificationMessage(payload,"Json", id)).subscribe(x =>{
        this.jsonMessages.push(new NotificationMessage(x.payload, x.type, x.id))
      });

      this.conversionService.convertReceipt(new NotificationMessage(payload,"PlainText", id)).subscribe(x =>{
        this.ptMessages.push(new NotificationMessage(x.payload, x.type, x.id))
      });

      this.conversionService.getTotalEarnings().subscribe(x => {
        this.totalEarnings = x.value
      })
    });
  }
}
