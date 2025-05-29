import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductosComponent } from './productos/productos.component';
import { TransaccionesComponent } from './transacciones/transacciones.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [
  { 
    path: '', 
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'productos', 
    loadChildren: () => import('./productos/productos.module').then(m => m.ProductosModule)
  },
  { 
    path: 'transacciones', 
    loadChildren: () => import('./transacciones/transacciones.module').then(m => m.TransaccionesModule) 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
