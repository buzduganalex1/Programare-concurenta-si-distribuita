import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReceiptViewerComponent } from './receipt-viewer/receipt-viewer.component';

const routes: Routes = [
  { path: 'receipt', component: ReceiptViewerComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
