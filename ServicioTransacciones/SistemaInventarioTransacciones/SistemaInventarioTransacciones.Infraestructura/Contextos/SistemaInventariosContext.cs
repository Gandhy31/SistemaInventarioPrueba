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
        public virtual DbSet<Transaccion> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.HasKey(e => e.IdTransaccion);

                entity.ToTable("Transaccion");

                entity.Property(e => e.IdProducto).IsRequired();
                entity.Property(e => e.Detalle).IsRequired();
                entity.Property(e => e.Fecha).HasColumnType("datetime");
                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
                entity.Property(e => e.PrecioTotal).HasColumnType("decimal");
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal");
                entity.Property(e => e.TipoTransaccion)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
