import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { HttpClientModule }    from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ViewReceiptsComponent } from './view-receipts/view-receipts.component';
import { CreateReceiptsComponent } from './create-receipts/create-receipts.component';

@NgModule({
  declarations: [
    AppComponent,
    ViewReceiptsComponent,
    CreateReceiptsComponent
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
