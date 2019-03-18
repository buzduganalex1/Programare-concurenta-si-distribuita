import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReceiptMessage } from 'src/models/ReceiptMessage';

@Injectable({
  providedIn: 'root'
})
export class ConvertReceiptService {

  serverUrl: string;
  productionUrl: string;
  functionUrl: string;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor( private http: HttpClient) {
    this.serverUrl = "http://localhost:5000/api/message";
    this.productionUrl = "https://ftiapi.azurewebsites.net/api/message";
    this.functionUrl =
      "https://fti-conversion.azurewebsites.net/api/Function2?code=BLytKhxIwTXiT/ZWhte2dQzRxI54HwjHaNU/3B1a4TYKlKVU7EwJAg==";
  }

  convertReceipt(message: ReceiptMessage): Observable<ReceiptMessage>{
    return this.http.post<ReceiptMessage>(this.productionUrl, message, this.httpOptions);
  }
}
