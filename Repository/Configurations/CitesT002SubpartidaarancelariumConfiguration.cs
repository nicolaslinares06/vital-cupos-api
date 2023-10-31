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
    public class CitesT002SubpartidaarancelariumConfiguration : IEntityTypeConfiguration<CitestT002Subpartidaarancelarium>
    {
        public void Configure(EntityTypeBuilder<CitestT002Subpartidaarancelarium> entity)
        {
            entity.HasKey(e => e.PkT002codigo)
                  .HasName("PK_CITEST_T002_SUBPARTIDAARANCELARIA");

            entity.ToTable("CITEST_T002_SUBPARTIDAARANCELARIA");

            entity.Property(e => e.PkT002codigo)
                  .HasColumnType("numeric(20, 0)")
                  .ValueGeneratedOnAdd()
                  .HasColumnName("PK_T002CODIGO");

            entity.Property(e => e.A002cantidadTotal)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A002CANTIDAD_TOTAL");

            entity.Property(e => e.A002codigoCertificado)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A002CODIGO_CERTIFICADO");

            entity.Property(e => e.A002codigoUsuarioCreacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A002CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A002codigoUsuarioModificacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A002CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A002descripcionProducto)
                  .HasMaxLength(200)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A002DESCRIPCION_PRODUCTO");

            entity.Property(e => e.A002descripcionSubpartida)
                  .HasMaxLength(200)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A002DESCRIPCION_SUBPARTIDA");

            entity.Property(e => e.A002estadoRegistro)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .IsUnicode(false)
                  .HasColumnName("A002ESTADO_REGISTRO");

            entity.Property(e => e.A002fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A002FECHA_CREACION");

            entity.Property(e => e.A002fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A002FECHA_MODIFICACION");

            entity.Property(e => e.A002subpartidaArancelaria)
                  .HasMaxLength(50)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A002SUBPARTIDA_ARANCELARIA");

            entity.Property(e => e.A002unidadComercial)
                  .HasMaxLength(50)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A002UNIDAD_COMERCIAL");

            entity.Property(e => e.A002valorFbo)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A002VALOR_FBO");

            entity.Property(e => e.A002valorUnitario)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A002VALOR_UNITARIO");

            entity.HasOne(d => d.A002codigoCertificadoNavigation)
                  .WithMany(p => p.CitestT002Subpartidaarancelaria)
                  .HasForeignKey(d => d.A002codigoCertificado)
                  .HasConstraintName("FK_CITEST_T002_SUBPARTIDAARANCELARIA_CITEST_T001_CERTIFICADO");
        }
    }
}
