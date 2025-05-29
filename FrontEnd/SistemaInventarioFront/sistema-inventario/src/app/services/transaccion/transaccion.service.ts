import { Injectable } from '@angular/core';
import { Request } from '../common/requestTransaccion';

@Injectable({
  providedIn: 'root'
})
export class TransaccionService {

  constructor(private request:Request) { }

  ObtenerTransaccion(id: number) {
    return this.request.ejecutarQueryGet<any>(`Transaccion/BuscarTransaccion/${id}`);
  }

  ObtenerTransacciones() {
    return this.request.ejecutarQueryGet<any>(`Transaccion/ObtenerTransacciones`);
  }

  AgregarVenta(body: any) {
    return this.request.ejecutarQueryPost<boolean>(`Transaccion/AgregarVenta`, body);
  }

  AgregarCompra(body: any) {
    return this.request.ejecutarQueryPost<boolean>(`Transaccion/AgregarCompra`, body);
  }
  
  ActualizarTransaccion(body: any) {
    return this.request.ejecutarQueryPut<boolean>(`Transaccion/ActualizarTransaccion`, body);
  }

  EliminarTransaccion(id: number) {

    return this.request.ejecutarQueryDelete<boolean>(`Transaccion/EliminarTransaccion/${id}`);

  }
}
