import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReceiptViewerComponent } from './receipt-viewer/receipt-viewer.component';
import { ViewReceiptsComponent } from './view-receipts/view-receipts.component';

const routes: Routes = [
  { path: 'createReceipt', component: ReceiptViewerComponent},
  { path: 'receipts', component: ViewReceiptsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
