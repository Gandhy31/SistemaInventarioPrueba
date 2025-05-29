using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaInventarios.Dominio.Entidades;

namespace SistemaInventarios.Infraestructura.Contextos
{
    public partial class SistemaInventariosContext: DbContext
    {
        public SistemaInventariosContext(DbContextOptions<SistemaInventariosContext> options): base(options){ }
        public virtual DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("Producto");

                entity.Property(e => e.Categoria).IsRequired();
                entity.Property(e => e.Descripcion).IsRequired();
                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
                entity.Property(e => e.Imagen).IsRequired();
                entity.Property(e => e.Nombre).IsRequired();
                entity.Property(e => e.Precio).HasColumnType("decimal");
            });


            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
