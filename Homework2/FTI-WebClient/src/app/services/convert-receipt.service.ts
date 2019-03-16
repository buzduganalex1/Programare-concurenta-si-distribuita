import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Observable } from 'rxjs';
import { ReceiptMessage } from 'src/models/ReceiptMessage';

@Injectable({
  providedIn: 'root'
})
export class ConvertReceiptService {

  serverUrl :string;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor( private http: HttpClient) {
    this.serverUrl = "http://localhost:5000/api/message"  
  }

  convertReceipt(message: ReceiptMessage): Observable<ReceiptMessage>{
    return this.http.post<ReceiptMessage>(this.serverUrl, message, this.httpOptions);
  }
}
