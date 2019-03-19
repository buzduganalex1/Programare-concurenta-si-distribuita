import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ViewReceiptsComponent } from './view-receipts/view-receipts.component';
import { CreateReceiptsComponent } from './create-receipts/create-receipts.component';

const routes: Routes = [
  { path: 'receipts/create', component: CreateReceiptsComponent},
  { path: 'receipts', component: ViewReceiptsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
