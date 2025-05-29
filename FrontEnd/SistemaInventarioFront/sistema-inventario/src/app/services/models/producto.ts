export  class Producto {
    constructor(
      public nombre: string,
      public descripcion: string,
      public categoria: string,
      public imagen: string,
      public precio: number,
      public stock: number,
      public fechaCreacion: Date,
      public fechaModificacion: Date
    ) {
  }
}