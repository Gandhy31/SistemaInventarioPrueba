import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { InventarioService } from '../services/inventario/inventario.service';
import { TransaccionService } from '../services/transaccion/transaccion.service';
import { FormBuilder } from '@angular/forms';
import { lastValueFrom } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { __await } from 'tslib';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  totalProductos: any = "0";
  totalTransacciones: any = "0";
  totalCompras: any = "0";
  totalVentas: any = "0";
  transacciones: any[] = [];
  constructor(private http: HttpClient, private _serviceTransaccion: TransaccionService, private _serviceInventario: InventarioService, private router: Router, private formService: FormBuilder) { }

  async ngOnInit() {
    //debugger
    var productos = await this.ObtenerProductos();
    this.transacciones = await this.ObtenerTransacciones();
    this.totalCompras = this.transacciones.filter(x => x.tipoTransaccion === "Compra").length;
    this.totalVentas = this.transacciones.filter(x => x.tipoTransaccion === "Venta").length;
    this.totalProductos = productos.length;
    this.totalTransacciones = this.transacciones.length;
    //console.log(this.totalCompras);
    //console.log(transacciones);
  }

  async ObtenerProductos() {
    debugger
    try {
      let response = await lastValueFrom(
        this._serviceInventario.ObtenerProductos()
      );
      return (response);
    } catch (e: HttpErrorResponse | any) {
      console.log(e);
    }
  }

  async ObtenerTransacciones() {
    debugger
    try {
      let response = await lastValueFrom(
        this._serviceTransaccion.ObtenerTransacciones()
      );
      return (response);
    } catch (e: HttpErrorResponse | any) {
      console.log(e);
    }
  }

  IrDashboardProductos() {
    this.router.navigate(['/productos'])
  }

  IrDashboardTransacciones() {
    this.router.navigate(['/transacciones'])
  }
}
