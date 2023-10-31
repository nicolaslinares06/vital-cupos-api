using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configurations
{
    public class CuposT004FacturacompraCartaventumConfiguration : IEntityTypeConfiguration<CupostT004FacturacompraCartaventum>
    {
        public void Configure(EntityTypeBuilder<CupostT004FacturacompraCartaventum> entity)
        {
            entity.HasKey(e => e.PkT004codigo)
                  .HasName("PK_CUPOST_T004_FACTURADECOMPRAOCARTAVENTA");

            entity.ToTable("CUPOST_T004_FACTURACOMPRA_CARTAVENTA");

            entity.Property(e => e.PkT004codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T004CODIGO");

            entity.Property(e => e.A004codigoDocumentoFactura)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004CODIGO_DOCUMENTO_FACTURA");

            entity.Property(e => e.A004codigoDocumentoSoporte)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004CODIGO_DOCUMENTO_SOPORTE");

            entity.Property(e => e.A004codigoEntidadCompra)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004CODIGO_ENTIDAD_COMPRA");

            entity.Property(e => e.A004codigoEntidadVende)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004CODIGO_ENTIDAD_VENDE");

            entity.Property(e => e.A004codigoParametricaTipoCartaventa)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004CODIGO_PARAMETRICA_TIPO_CARTAVENTA");

            entity.Property(e => e.A004codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A004codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A004CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A004estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004ESTADO_REGISTRO");

            entity.Property(e => e.A004fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A004FECHA_CREACION");

            entity.Property(e => e.A004fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A004FECHA_MODIFICACION");

            entity.Property(e => e.A004fechaVenta)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A004FECHA_VENTA");

            entity.Property(e => e.A004observacionesCompra)
                .HasMaxLength(200)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A004OBSERVACIONES_COMPRA");

            entity.Property(e => e.A004observacionesVenta)
                .HasMaxLength(200)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A004OBSERVACIONES_VENTA");

            entity.Property(e => e.A004saldoEntidadCompraFinal)
                .HasColumnType("numeric(18, 0)")
                .IsRequired(true)
                .HasColumnName("A004SALDO_ENTIDAD_COMPRA_FINAL");

            entity.Property(e => e.A004saldoEntidadCompraInicial)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004SALDO_ENTIDAD_COMPRA_INICIAL");

            entity.Property(e => e.A004saldoEntidadVendeFinal)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004SALDO_ENTIDAD_VENDE_FINAL");

            entity.Property(e => e.A004saldoEntidadVendeInicial)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004SALDO_ENTIDAD_VENDE_INICIAL");

            entity.Property(e => e.A004totalCuposObtenidos)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004TOTAL_CUPOS_OBTENIDOS");

            entity.Property(e => e.A004totalCuposVendidos)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004TOTAL_CUPOS_VENDIDOS");

            entity.Property(e => e.A004codigoCupo)
                .HasColumnType("numeric(18, 0)")
                .IsRequired(false)
                .HasColumnName("A004CODIGO_CUPO");

            entity.Property(e => e.A004numeroCartaVenta)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A004_NUMERO_CARTA_VENTA");

            entity.Property(e => e.A004disponibilidadInventario)
                .HasColumnType("numeric(6, 0)")
                .IsRequired(true)
                .HasColumnName("A004_DISPONIBILIDAD_INVENTARIO");

            entity.Property(e => e.A004fechaRegistroCartaVenta)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A004_FECHA_REGISTRO_CARTA_VENTA_FACTURA");

            entity.Property(e => e.A004tipoEspecimenEntidadVende)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("A004_TIPO_ESPECIMEN_ENTIDAD_VENDE");

            entity.Property(e => e.A004tipoEspecimenEntidadCompra)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("A004_TIPO_ESPECIMEN_ENTIDAD_COMPRA");

            entity.HasOne(d => d.A004codigoEntidadVendeNavigation)
                .WithMany(p => p.CupostT004FacturacompraCartaventa)
                .HasForeignKey(d => d.A004codigoEntidadVende)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUPOST_T004_FACTURACOMPRA_CARTAVENTA_CUPOST_T001_ENTIDAD");

            entity.HasOne(d => d.A004codigoParametricaTipoCartaventaNavigation)
                .WithMany(p => p.CupostT004FacturacompraCartaventa)
                .HasForeignKey(d => d.A004codigoParametricaTipoCartaventa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUPOST_T004_FACTURACOMPRA_CARTAVENTA_ADMINT_T008_PARAMETRICA");
        }
    }
}
