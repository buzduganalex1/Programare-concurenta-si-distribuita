import { Component, OnInit } from '@angular/core';
import {Receipt} from '../../models/Receipt'
import { Item } from 'src/models/Item';
import { Amount } from 'src/models/Amount';
import { ReceiptService } from '../services/receipt.service';

@Component({
  selector: 'app-receipt-viewer',
  templateUrl: './receipt-viewer.component.html',
  styleUrls: ['./receipt-viewer.component.css']
})
export class ReceiptViewerComponent implements OnInit {

  public items: Item[] = [
    new Item("Milk", new Amount("EUR",3)),    
    new Item("Salt", new Amount("EUR",1)),
    new Item("Eggs", new Amount("EUR",2)),
    new Item("Bacon", new Amount("EUR",4))
  ];

  showButton = false;
  receipt: Receipt;
  receiptService: ReceiptService;
  
  constructor(public service: ReceiptService) {
    this.receiptService = service;
  }

  ngOnInit() {
    this.receipt = new Receipt();
  };

  public onSelect(item: Item){
    this.showButton = true;
    this.receipt.Items.push(item)
  }

  public clearCart(){
    this.receiptService.sendReceipt(this.receipt);
    this.receipt = new Receipt();
    this.showButton = false;
  }
}

