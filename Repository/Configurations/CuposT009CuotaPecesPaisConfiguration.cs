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
    public class CuposT009CuotaPecesPaisConfiguration : IEntityTypeConfiguration<CupostT009CuotaPecesPai>
    {
        public void Configure(EntityTypeBuilder<CupostT009CuotaPecesPai> entity)
        {
            entity.HasKey(e => e.PkT009codigo)
                  .HasName("PK_CUPOST_T009_CUOTAPECESPAIS");

            entity.ToTable("CUPOST_T009_CUOTA_PECES_PAIS");

            entity.Property(e => e.PkT009codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T009CODIGO");

            entity.Property(e => e.A009codigoDocumentoSoporte)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A009CODIGO_DOCUMENTO_SOPORTE");

            entity.Property(e => e.A009codigoParametricaTipoMaritimo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A009CODIGO_PARAMETRICA_TIPO_MARITIMO");

            entity.Property(e => e.A009codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A009CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A009codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A009CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A009estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A009ESTADO_REGISTRO");

            entity.Property(e => e.A009fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A009FECHA_CREACION");

            entity.Property(e => e.A009fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A009FECHA_MODIFICACION");

            entity.Property(e => e.A009fechaResolucion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A009FECHA_RESOLUCION");

            entity.Property(e => e.A009fechaVigencia)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A009FECHA_VIGENCIA");

            entity.Property(e => e.A009numeroResolucion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A009NUMERO_RESOLUCION");

            entity.Property(e => e.A009objetoResolucion)
                .HasMaxLength(200)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A009OBJETO_RESOLUCION");

            entity.Property(e => e.A009fechaInicialVigencia)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A009FECHA_INICIAL_VIGENCIA");

            entity.Property(e => e.A009fechaFinalVigencia)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A009FECHA_FINAL_VIGENCIA");
        }
    }
}
