import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { InventarioService } from '../services/inventario/inventario.service';
import { Router } from '@angular/router';
import { Form, FormBuilder, FormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormArray } from '@angular/forms';
import { lastValueFrom } from 'rxjs';
import { ModalDismissReasons, NgbDatepickerModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Producto } from '../services/models/producto';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RegistroProducto } from '../services/models/registroProducto';

@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css']
})
export class ProductosComponent implements OnInit {
  paginaActual = 1;
  form!: FormGroup;
  totalProductos: any = "0";
  totalTransacciones: any = "0";
  totalCompras: any = "0";
  totalVentas: any = "0";
  productos: any[] = [];
  productosFiltrados: any[] = [];
  idProducto: number = 0;
  producto!: any;
  filtro: string = '';
  constructor(private http: HttpClient, private _serviceInventario: InventarioService, private router: Router, private formService: FormBuilder, private modalService: NgbModal, private snackBar: MatSnackBar) { }

  async ngOnInit() {
    this.productos = await this.ObtenerProductos();
    this.productosFiltrados = this.productos;
  }

  //funciones de validacion formulario get
  get NombreInvalido() {
    return this.form.get('nombre')?.invalid && this.form.get('nombre')?.touched
  }

  get DescripcionInvalida() {
    return this.form.get('descripcion')?.invalid && this.form.get('descripcion')?.touched
  }

  get CategoriaInvalida() {
    return this.form.get('categoria')?.invalid && this.form.get('categoria')?.touched
  }

  get ImagenInvalida() {
    return this.form.get('imagen')?.invalid && this.form.get('imagen')?.touched
  }

  get PrecioInvalido() {
    return this.form.get('precio')?.invalid && this.form.get('precio')?.touched
  }

  get StockInvalido() {
    return this.form.get('stock')?.invalid && this.form.get('stock')?.touched
  }

  CrearFormulario() {
    this.form = this.formService.group({
      nombre: ['', Validators.required],
      descripcion: ['', Validators.required],
      categoria: ['', Validators.required],
      imagen: ['', Validators.required],
      precio: ['', Validators.required],
      stock: ['', Validators.required]
    })
  }

  async GuardarFormularioAgregar(modal: any) {
    if (this.form.invalid) {
      return Object.values(this.form.controls).forEach(control => {
        control.markAllAsTouched();
      })
    } else {
      var producto = new Producto(
        this.form.value.nombre,
        this.form.value.descripcion,
        this.form.value.categoria,
        this.form.value.imagen,
        this.form.value.precio,
        this.form.value.stock,
        new Date(),
        new Date()
      );
      var response = await this.AgregarProducto(producto);

      if (response) {
        this.abrirAlertaOk("Guardado Exitosamente");
        this.productos = await this.ObtenerProductos();
        this.productosFiltrados = this.productos
        modal.close();
      } else {
        this.abrirAlertaBad();
      }
    }
  }

  async GuardarFormularioActualizar(modal: any) {
    this.form.patchValue({ imagen: this.producto.imagen });
    if (this.form.invalid) {
      return Object.values(this.form.controls).forEach(control => {
        control.markAllAsTouched();
      })
    } else {
      var producto = new RegistroProducto(
        this.producto.idProducto,
        this.form.value.nombre,
        this.form.value.descripcion,
        this.form.value.categoria,
        this.producto.imagen,
        this.form.value.precio,
        this.form.value.stock,
        new Date(),
        new Date()
      );
      var response = await this.ActualizarProducto(producto);

      if (response) {
        this.abrirAlertaOk("Actualizado Exitosamente");
        this.productos = await this.ObtenerProductos();
        this.productosFiltrados = this.productos
        modal.close();
      } else {
        this.abrirAlertaBad();
      }
    }
  }

  async EliminarProductoBD(id: number) {
    var response = await this.EliminarProducto(id);

    if (response) {
      this.abrirAlertaOk("Eliminado Exitosamente");
      this.productos = await this.ObtenerProductos();
      this.productosFiltrados = this.productos
    } else {
      this.abrirAlertaBad();
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

  async AgregarProducto(body: any) {
    debugger
    try {
      let response = await lastValueFrom(
        this._serviceInventario.AgregarProducto(body)
      );
      return (response);
    } catch (e: HttpErrorResponse | any) {
      console.log(e);
      return false;
    }
  }

  async EliminarProducto(id: number) {
    debugger
    try {
      let response = await lastValueFrom(
        this._serviceInventario.EliminarProducto(id)
      );
      return (response);
    } catch (e: HttpErrorResponse | any) {
      console.log(e);
      return false;
    }
  }

  async ActualizarProducto(body: any) {
    debugger
    try {
      let response = await lastValueFrom(
        this._serviceInventario.ActualizarProducto(body)
      );
      return (response);
    } catch (e: HttpErrorResponse | any) {
      console.log(e);
      return false;
    }
  }

  async AgregarProductoModal(content: any) {
    this.CrearFormulario();
    this.modalService.open(content);
  }

  async EliminarProductoModal(content: any, body: any) {
    this.idProducto = body.idProducto;
    this.modalService.open(content);
  }

  async ActualizarProductoModal(content: any, body: any) {
    this.producto = body;
    this.CrearFormulario();
    this.modalService.open(content);
  }

  convertirImagenBase64(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;

    const archivo = input.files[0];
    const lector = new FileReader();

    lector.onload = () => {
      const base64 = lector.result as string;
      const base64SinHeader = (lector.result as string).split(',')[1];
      this.form.patchValue({ imagen: base64SinHeader });
    };

    lector.readAsDataURL(archivo);
  }

  convertirImagenBase64Actualizar(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;

    const archivo = input.files[0];
    const lector = new FileReader();

    lector.onload = () => {
      const base64 = lector.result as string;
      const base64SinHeader = (lector.result as string).split(',')[1];
      this.producto.imagen = base64SinHeader;
    };

    lector.readAsDataURL(archivo);
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

  filtrarItems(): void {
    debugger
    const texto = this.filtro.trim().toLowerCase();

    this.productosFiltrados = this.productos.filter(item =>
      item.nombre.toLowerCase().includes(texto)
    );
  }
}
