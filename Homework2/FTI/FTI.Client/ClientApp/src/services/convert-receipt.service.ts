import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NotificationMessage } from 'src/models/NotificationMessage';
import { environment } from 'src/environments/environment';
import { TotalReceiptResponse } from 'src/models/TotalReceiptResponse';

@Injectable({
  providedIn: 'root'
})
export class ConvertReceiptService {
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor( private http: HttpClient) {}

  convertReceipt(message: NotificationMessage): Observable<NotificationMessage>{
    return this.http.post<NotificationMessage>(environment.host + "/api/message", message, this.httpOptions);
  }

  getTotalEarnings(): Observable<TotalReceiptResponse>{
    return this.http.get<TotalReceiptResponse>(environment.host + "/api/message", this.httpOptions);
  }
}
