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
    public class CupostT014ResolucionConfiguration : IEntityTypeConfiguration<CupostT014Resolucion>
    {
        public void Configure(EntityTypeBuilder<CupostT014Resolucion> entity)
        {
            entity.HasKey(e => e.PkT014codigo)
                  .HasName("PK_CUPOST_T014_RESOLUCION");

            entity.ToTable("CUPOST_T014_RESOLUCION");

            entity.Property(e => e.PkT014codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T014CODIGO");

            entity.Property(e => e.A014codigoDocumentoSoporte)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A014CODIGO_DOCUMENTO_SOPORTE");

            entity.Property(e => e.A014codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A014CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A014codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A014CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A014estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A014ESTADO_REGISTRO");

            entity.Property(e => e.A014fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A014FECHA_CREACION");

            entity.Property(e => e.A014fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A014FECHA_MODIFICACION");

            entity.Property(e => e.A014fechaResolucion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A014FECHA_RESOLUCION");

            entity.Property(e => e.A014fechaInicio)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A014FECHA_INICIO");

            entity.Property(e => e.A014fechaFin)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A014FECHA_FIN");

            entity.Property(e => e.A014numeroResolucion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A014NUMERO_RESOLUCION");

            entity.Property(e => e.A014objetoResolucion)
                .HasMaxLength(200)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A014OBJETO_RESOLUCION");

            entity.Property(e => e.A014codigoEmpresa)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A014CODIGO_EMPRESA");
        }
    }
}
