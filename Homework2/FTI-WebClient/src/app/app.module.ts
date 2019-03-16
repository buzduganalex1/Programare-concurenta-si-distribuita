import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { HttpClientModule }    from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReceiptViewerComponent } from './receipt-viewer/receipt-viewer.component';
import { ViewReceiptsComponent } from './view-receipts/view-receipts.component';

@NgModule({
  declarations: [
    AppComponent,
    ReceiptViewerComponent,
    ViewReceiptsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
