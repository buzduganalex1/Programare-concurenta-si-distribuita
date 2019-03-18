import { Injectable } from '@angular/core';
import { Receipt } from 'src/models/Receipt';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReceiptService {

  serverUrl: string;
  productionUrl: string;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor( private http: HttpClient) {
    this.serverUrl = "http://localhost:5000/api/values";
    this.productionUrl = "https://ftiapi.azurewebsites.net/api/values";
  }

  sendReceipt(receipt: Receipt){
    this.http.post<Receipt>(this.productionUrl, receipt, this.httpOptions).subscribe(item => {
    });
  }
}
