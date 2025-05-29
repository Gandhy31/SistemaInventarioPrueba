import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { InventarioService } from '../services/inventario/inventario.service';
import { TransaccionService } from '../services/transaccion/transaccion.service';
import { Router } from '@angular/router';
import { Form, FormBuilder, FormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormArray } from '@angular/forms';
import { lastValueFrom } from 'rxjs';
import { ModalDismissReasons, NgbDatepickerModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Producto } from '../services/models/producto';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RegistroProducto } from '../services/models/registroProducto';
import { Transaccion } from '../services/models/transaccion';
import { RegistroTransaccion } from '../services/models/registroTransaccion';

@Component({
  selector: 'app-transacciones',
  templateUrl: './transacciones.component.html',
  styleUrls: ['./transacciones.component.css']
})
export class TransaccionesComponent implements OnInit {
  paginaActual = 1;
  form!: FormGroup;
  transacciones: any[] = [];
  transaccionesFiltradas: any[] = [];
  productos: any[] = [];
  producto!: any;
  tiposTransacciones: string[] = ['Compra', 'Venta'];
  filtroProducto: string = '';
  cantidad: number = 0;
  totalTransaccion: number = 0;
  idTransaccion: number = 0;
  transaccion!: any;
  filtroFechaInicial: any = null;
  filtroFechaFinal: any = null;

  constructor(private http: HttpClient, private _serviceInventario: InventarioService, private _serviceTransaccion: TransaccionService, private router: Router, private formService: FormBuilder, private modalService: NgbModal, private snackBar: MatSnackBar) { }

  async ngOnInit() {
    this.transacciones = await this.ObtenerTransacciones();
    this.productos = await this.ObtenerProductos();
    this.transaccionesFiltradas = this.transacciones;
    //console.log(this.transacciones)
  }

  //funciones de validacion formulario get
  get ProductoInvalido() {
    return this.form.get('producto')?.invalid && this.form.get('producto')?.touched
  }

  get FechaInvalida() {
    return this.form.get('fecha')?.invalid && this.form.get('fecha')?.touched
  }

  get TipoTransaccionInvalida() {
    return this.form.get('tipoTransaccion')?.invalid && this.form.get('tipoTransaccion')?.touched
  }

  get CantidadInvalida() {
    return this.form.get('cantidad')?.invalid && this.form.get('cantidad')?.touched
  }

  get PrecioUnitarioInvalido() {
    return this.form.get('precioUnitario')?.invalid && this.form.get('precioUnitario')?.touched
  }

  get PrecioTotalInvalido() {
    return this.form.get('precioTotal')?.invalid && this.form.get('precioTotal')?.touched
  }

  get DetalleInvalido() {
    return this.form.get('detalle')?.invalid && this.form.get('detalle')?.touched
  }

  CrearFormulario() {
    this.form = this.formService.group({
      producto: ['', Validators.required],
      fecha: ['', Validators.required],
      tipoTransaccion: ['', Validators.required],
      cantidad: ['', Validators.required],
      precioUnitario: ['', Validators.required],
      precioTotal: ['', Validators.required],
      detalle: ['', Validators.required]
    })
  }

  async GuardarFormularioAgregar(modal: any) {
    debugger
    //console.log(this.form)
    if (this.form.invalid) {
      return Object.values(this.form.controls).forEach(control => {
        control.markAllAsTouched();
      })
    } else {
      this.form.get('precioUnitario')?.enable();
      this.form.get('precioTotal')?.enable();
      var transaccionEnviar = new Transaccion(
        this.form.value.producto,
        this.form.value.fecha.year + '-' +
        this.ValidarFecha(this.form.value.fecha.month) + '-' +
        this.ValidarFecha(this.form.value.fecha.day) + 'T00:00:00',
        this.form.value.tipoTransaccion,
        this.form.value.cantidad,
        this.form.value.precioUnitario,
        this.form.value.precioTotal,
        this.form.value.detalle,
        new Date(),
        new Date()
      );

      debugger
      if (transaccionEnviar.tipoTransaccion === "Compra") {
        var response = await this.AgregarTransaccionCompra(transaccionEnviar);
        if (response) {
          this.transacciones = await this.ObtenerTransacciones();
          this.transaccionesFiltradas = this.transacciones;
          this.abrirAlertaOk("Agregado Exitosamente");
          modal.close();
        } else {
          this.abrirAlertaBad();
        }
      } else if (transaccionEnviar.tipoTransaccion === "Venta") {
        var response = await this.ValidarTransaccion(transaccionEnviar.idProducto, transaccionEnviar.cantidad);
        if (response) {
          var responseT = await this.AgregarTransaccionVenta(transaccionEnviar);
          if (responseT) {
            this.transaccionesFiltradas = await this.ObtenerTransacciones();
            this.abrirAlertaOk("Agregado Exitosamente");
            modal.close();
          } else {
            this.abrirAlertaBad();
          }
        } else {
          this.abrirAlertaOk("Transaccion invalida: insuficiente stock");
        }
      }
    }
  }

  async GuardarFormularioActualizar(modal: any) {
    debugger
    console.log(this.form)
    if (this.form.invalid) {
      return Object.values(this.form.controls).forEach(control => {
        control.markAllAsTouched();
      })
    } else {
      this.form.get('precioUnitario')?.enable();
      this.form.get('precioTotal')?.enable();
      var transaccionEnviar = new RegistroTransaccion(
        this.transaccion.idTransaccion,
        this.form.value.producto,
        this.form.value.fecha.year + '-' +
        this.ValidarFecha(this.form.value.fecha.month) + '-' +
        this.ValidarFecha(this.form.value.fecha.day) + 'T00:00:00',
        this.form.value.tipoTransaccion,
        this.form.value.cantidad,
        this.form.value.precioUnitario,
        this.form.value.precioTotal,
        this.form.value.detalle,
        new Date(this.transaccion.fechaCreacion),
        new Date(this.transaccion.fechaModificacion)
      );

      debugger
      var response = await this.ActualizarTransaccion(transaccionEnviar);
      if (response) {
        this.transacciones = await this.ObtenerTransacciones();
        this.transaccionesFiltradas = this.transacciones;
        this.abrirAlertaOk("Actualizado Exitosamente");
        modal.close();
      } else {
        this.abrirAlertaBad();
      }
    }
  }

  async EliminarTransaccionBD(id: number) {
    var response = await this.EliminarTransaccion(id);

    if (response) {
      this.abrirAlertaOk("Eliminado Exitosamente");
      this.transacciones = await this.ObtenerTransacciones();
      this.transaccionesFiltradas = this.transacciones;
    } else {
      this.abrirAlertaBad();
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

  async ValidarTransaccion(id: number, cantidad: number) {
    debugger
    try {
      let response = await lastValueFrom(
        this._serviceInventario.ValidarTransaccion(id, cantidad)
      );
      return (response);
    } catch (e: HttpErrorResponse | any) {
      console.log(e);
      return false
    }
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

  async AgregarTransaccionCompra(body: any) {
    debugger
    try {
      let response = await lastValueFrom(
        this._serviceTransaccion.AgregarCompra(body)
      );
      return (response);
    } catch (e: HttpErrorResponse | any) {
      console.log(e);
      return false;
    }
  }

  async AgregarTransaccionVenta(body: any) {
    debugger
    try {
      let response = await lastValueFrom(
        this._serviceTransaccion.AgregarVenta(body)
      );
      return (response);
    } catch (e: HttpErrorResponse | any) {
      console.log(e);
      return false;
    }
  }

  async ActualizarTransaccion(body: any) {
    //debugger
    try {
      let response = await lastValueFrom(
        this._serviceTransaccion.ActualizarTransaccion(body)
      );
      return (response);
    } catch (e: HttpErrorResponse | any) {
      console.log(e);
      return false;
    }
  }


  async EliminarTransaccion(id: number) {
    //debugger
    try {
      let response = await lastValueFrom(
        this._serviceTransaccion.EliminarTransaccion(id)
      );
      return (response);
    } catch (e: HttpErrorResponse | any) {
      console.log(e);
      return false;
    }
  }

  async AgregarTransaccionModal(content: any) {
    this.productos = await this.ObtenerProductos()
    this.CrearFormulario();
    this.modalService.open(content);
  }

  async EliminarProductoModal(content: any, body: any) {
    this.idTransaccion = body.idTransaccion;
    this.modalService.open(content);
  }

  async ActualizarProductoModal(content: any, body: any) {
    //debugger
    this.transaccion = body;
    const fechaString = new Date(this.transaccion.fecha).toISOString().split('T')[0];
    const fechas = fechaString.split('-');
    const fechaDatePicker = {
      year: +fechas[0],
      month: +fechas[1],
      day: +fechas[2]
    };
    this.CrearFormulario();
    this.form.patchValue({ "producto": this.transaccion.idProducto });
    this.form.patchValue({ "fecha": fechaDatePicker });
    this.form.patchValue({ "tipoTransaccion": this.transaccion.tipoTransaccion });
    this.form.patchValue({ "cantidad": this.transaccion.cantidad });
    this.form.patchValue({ "precioUnitario": this.transaccion.precioUnitario });
    this.form.patchValue({ "precioTotal": this.transaccion.precioTotal });
    this.form.patchValue({ "detalle": this.transaccion.detalle });
    this.productos = await this.ObtenerProductos();
    this.modalService.open(content);
  }

  abrirAlertaOk(mensaje: string) {
    this.snackBar.open(mensaje, 'Cerrar', {
      duration: 3000
    });
  }

  abrirAlertaBad() {
    this.snackBar.open('Ocurrio un error al realizar la operacion', 'Cerrar', {
      duration: 3000
    });
  }

  abrirAlertaFiltro() {
    this.snackBar.open('Ingrese Fechas Válidas', 'Cerrar', {
      duration: 3000
    });
  }

  CambiarProducto(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    const selectedIndex = selectElement.selectedIndex;
    this.producto = this.productos[selectedIndex];
    this.form.patchValue({ precioUnitario: this.producto.precio });
    this.form.patchValue({ precioTotal: this.producto.precio * this.form.value.cantidad });
    this.form.get('precioUnitario')?.disable();
  }

  CalcularTotal() {
    if (this.producto != null) {
      console.log("entro");
      this.form.patchValue({ precioTotal: this.producto.precio * this.form.value.cantidad });
      this.form.get('precioTotal')?.disable();
    }
  }

  ValidarFecha(fecha: any) {
    if (fecha < 10) {
      return ('0' + fecha);
    } else {
      return fecha;
    }
  }

  reiniciar() {
    this.transaccionesFiltradas = this.transacciones;
  }

  filtrarItems(): void {
    this.transaccionesFiltradas = this.transacciones.filter(item =>
      item.idProducto === Number(this.filtroProducto)
    );
  }

  filtrarPorFecha(): void {
    if (this.filtroFechaInicial === null || this.filtroFechaFinal === null) {
      this.abrirAlertaFiltro();
    } else {
      var fechaInicial = new Date(this.filtroFechaInicial.year + "-" + this.ValidarFecha(this.filtroFechaInicial.month) + "-" + this.ValidarFecha(this.filtroFechaInicial.day) + 'T00:00:00');
      var fechaFinal = new Date(this.filtroFechaFinal.year + "-" + this.ValidarFecha(this.filtroFechaFinal.month) + "-" + this.ValidarFecha(this.filtroFechaFinal.day) + 'T00:00:00');
      console.log(fechaInicial);

      if (fechaFinal < fechaInicial) {
        this.abrirAlertaFiltro();
      } else {
        this.transaccionesFiltradas = this.transacciones.filter(item =>
          new Date(item.fecha) >= fechaInicial && new Date(item.fecha) <= fechaFinal
        );
      }
    }
  }
}
