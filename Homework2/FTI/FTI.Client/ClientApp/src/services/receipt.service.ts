import { Injectable } from '@angular/core';
import { Receipt } from 'src/models/Receipt';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReceiptService {

  constructor( private http: HttpClient) {}

  sendReceipt(receipt: Receipt){
    var httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    this.http.post<Receipt>(environment.host + "/api/receipt", receipt, httpOptions).subscribe(x => {});
  }
}
