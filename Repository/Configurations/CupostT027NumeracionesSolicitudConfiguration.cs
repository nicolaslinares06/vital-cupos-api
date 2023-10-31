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
    public class CupostT027NumeracionesSolicitudConfiguration : IEntityTypeConfiguration<CupostT027NumeracionesSolicitud>
    {
        public void Configure(EntityTypeBuilder<CupostT027NumeracionesSolicitud> entity)
        {
            entity.HasKey(e => e.Pk_T027Codigo)
                  .HasName("PK_T027CODIGO");

            entity.ToTable("CUPOST_T027_NUMERACIONES_SOLICITUD");

            entity.Property(e => e.Pk_T027Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T027CODIGO");

            entity.Property(e => e.A027CodigoSolicitud)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A027CODIGO_SOLICITUD");

            entity.Property(e => e.A027CodigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A027CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A027CodigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A027CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A027FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A027FECHA_CREACION");

            entity.Property(e => e.A027FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A027FECHA_MODIFICACION");

            entity.Property(e => e.A027EstadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A027ESTADO_REGISTRO");

            entity.Property(e => e.A027NumeroInternoInicial)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A027NUMERO_INTERNO_INICIAL");

            entity.Property(e => e.A027NumeroInternoFinal)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A027NUMERO_INTERNO_FINAL");

            entity.Property(e => e.A027OrigenSolicitud)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("A027CODIGO_PARAMETRICA_ORIGEN_SOLICITUD");

            entity.Property(e => e.A027NumeroInicialPrecintos)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("A027NUMERO_PRECINTO_INICIAL");

            entity.Property(e => e.A027NumeroFinalPrecintos)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("A027NUMERO_PRECINTO_FINAL");

            entity.Property(e => e.A027CodigoEmpresaOrigenNumeraciones)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("A027CODIGO_EMPRESA_ORIGEN_NUMERACIONES");

            entity.Property(e => e.A027NumeroInicialMarquillas)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("A027NUMERO_MARQUILLA_INICIAL");

            entity.Property(e => e.A027NumeroFinalMarquillas)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("A027NUMERO_MARQUILLA_FINAL");
        }
    }
}
