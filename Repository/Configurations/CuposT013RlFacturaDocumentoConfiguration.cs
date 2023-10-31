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
    public class CuposT013RlFacturaDocumentoConfiguration : IEntityTypeConfiguration<CupostT013RlFacturaDocumento>
    {
        public void Configure(EntityTypeBuilder<CupostT013RlFacturaDocumento> entity)
        {
            entity.HasKey(e => e.PkT013codigo)
                  .HasName("PK_CITEST_T013RL_FACTURA_DOCUMENTO");

            entity.ToTable("CUPOST_T013_RL_FACTURA_DOCUMENTO");

            entity.Property(e => e.PkT013codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T013CODIGO");

            entity.Property(e => e.A013codigoDocumento)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A013CODIGO_DOCUMENTO");

            entity.Property(e => e.A013codigoFacturacompraCartaventa)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A013CODIGO_FACTURACOMPRA_CARTAVENTA");

            entity.Property(e => e.A013codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A013CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A013codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A013CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A013estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A013ESTADO_REGISTRO");

            entity.Property(e => e.A013fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A013FECHA_CREACION");

            entity.Property(e => e.A013fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A013FECHA_MODIFICACION");
        }
    }
}
