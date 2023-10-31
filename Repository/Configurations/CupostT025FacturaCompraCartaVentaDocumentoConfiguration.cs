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
    public class CupostT025FacturaCompraCartaVentaDocumentoConfiguration : IEntityTypeConfiguration<CupostT025FacturaCompraCartaVentaDocumento>
    {
        public void Configure(EntityTypeBuilder<CupostT025FacturaCompraCartaVentaDocumento> entity)
        {
            entity.HasKey(e => e.Pk_T025Codigo)
                  .HasName("PK_T025CODIGO");

            entity.ToTable("CUPOST_T025_RL_FACTURACOMPRA_CARTAVENTA_DOCUMENTO");

            entity.Property(e => e.Pk_T025Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T025CODIGO");

            entity.Property(e => e.A025CodigoFacturaCompraCartaVenta)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A025CODIGO_FACTURACOMPRA_CARTAVENTA");

            entity.Property(e => e.A025CodigoDocumento)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A025CODIGO_DOCUMENTO");

            entity.Property(e => e.A025FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A025FECHA_CREACION");

            entity.Property(e => e.A025FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A025FECHA_MODIFICACION");

            entity.Property(e => e.A025UsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A025CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A025UsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A025CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A025EstadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A025ESTADO_REGISTRO");

        }
    }
}
