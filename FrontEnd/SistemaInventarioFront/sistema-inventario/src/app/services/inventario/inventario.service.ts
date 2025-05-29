import { Injectable } from '@angular/core';
import { Request } from '../common/requestProducto';
import { EntidadRespuesta } from '../common/entidadRespuesta';

@Injectable({
  providedIn: 'root'
})
export class InventarioService {

  constructor(private request:Request) { }

  ObtenerProducto(id: number) {

    return this.request.ejecutarQueryGet<any>(`Productos/ObtenerProducto/${id}`);

  }

  ObtenerProductos() {

    return this.request.ejecutarQueryGet<any>(`Productos/ObtenerProductos`);

  }

  AgregarProducto(body: any) {

    return this.request.ejecutarQueryPost<boolean>(`Productos/AgregarProducto`, body);

  }

  ActualizarProducto(body: any) {

    return this.request.ejecutarQueryPut<boolean>(`Productos/ActualizarProducto`, body);

  }

  EliminarProducto(id: number) {

    return this.request.ejecutarQueryDelete<boolean>(`Productos/EliminarProducto/${id}`);

  }

  ValidarTransaccion(id:number, cantidad:number) {

    return this.request.ejecutarQueryGet<boolean>(`Productos/ValidarTransaccion?idProducto=${id}&cantidad=${cantidad}`);

  }
}
