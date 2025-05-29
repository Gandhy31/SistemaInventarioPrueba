export  class RegistroTransaccion {
    constructor(
      public idTransaccion: number,
      public idProducto: number,
      public fecha: string,
      public tipoTransaccion: string,
      public cantidad: number,
      public precioUnitario: number,
      public precioTotal: number,
      public detalle: string,
      public fechaCreacion: Date,
      public fechaModificacion: Date
    ) {
  }
}