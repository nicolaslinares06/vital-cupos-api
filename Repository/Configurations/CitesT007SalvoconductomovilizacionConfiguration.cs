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
    internal class CitesT007SalvoconductomovilizacionConfiguration : IEntityTypeConfiguration<CitestT007Salvoconductomovilizacion>
    {
        public void Configure(EntityTypeBuilder<CitestT007Salvoconductomovilizacion> entity)
        {
            entity.HasKey(e => e.PkT007codigo)
                .HasName("PK_CITEST_T007_SALVOCONDUCTOMOVILIZACION");

            entity.ToTable("CITEST_T007_SALVOCONDUCTOMOVILIZACION");

            entity.Property(e => e.PkT007codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T007CODIGO");

            entity.Property(e => e.A007codigoCertificado)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A007CODIGO_CERTIFICADO");

            entity.Property(e => e.A007codigoCiudad)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A007CODIGO_CIUDAD");

            entity.Property(e => e.A007codigoParametricaMedioTransporte)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A007CODIGO_PARAMETRICA_MEDIO_TRANSPORTE");

            entity.Property(e => e.A007codigoParametricaTipoRuta)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A007CODIGO_PARAMETRICA_TIPO_RUTA");

            entity.Property(e => e.A007codigoParametricaTipoVehiculo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A007CODIGO_PARAMETRICA_TIPO_VEHICULO");

            entity.Property(e => e.A007codigoPersonaSalvoconducto)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A007CODIGO_PERSONA_SALVOCONDUCTO");

            entity.Property(e => e.A007codigoPersonaTransportador)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A007CODIGO_PERSONA_TRANSPORTADOR");

            entity.Property(e => e.A007codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A007CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A007codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A007CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A007empresaTransportadora)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A007EMPRESA_TRANSPORTADORA");

            entity.Property(e => e.A007estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A007ESTADO_REGISTRO");

            entity.Property(e => e.A007fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A007FECHA_CREACION");

            entity.Property(e => e.A007fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A007FECHA_MODIFICACION");

            entity.Property(e => e.A007fechaVencimientoSalvoconducto)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A007FECHA_VENCIMIENTO_SALVOCONDUCTO");

            entity.Property(e => e.A007finalidadMovilizacion)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A007FINALIDAD_MOVILIZACION");

            entity.Property(e => e.A007numeroSalvoconducto)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A007NUMERO_SALVOCONDUCTO");

            entity.Property(e => e.A007placaVehiculo)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A007PLACA_VEHICULO");

            entity.HasOne(d => d.A007codigoCertificadoNavigation)
                .WithMany(p => p.CitestT007Salvoconductomovilizacions)
                .HasForeignKey(d => d.A007codigoCertificado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T007_SALVOCONDUCTOMOVILIZACION_CITEST_T001_CERTIFICADO");

            entity.HasOne(d => d.A007codigoCiudadNavigation)
                .WithMany(p => p.CitestT007Salvoconductomovilizacions)
                .HasForeignKey(d => d.A007codigoCiudad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T007_SALVOCONDUCTOMOVILIZACION_ADMINT_T004_CIUDAD");

            entity.HasOne(d => d.A007codigoParametricaTipoRutaNavigation)
                .WithMany(p => p.CitestT007SalvoconductomovilizacionA007codigoParametricaTipoRutaNavigations)
                .HasForeignKey(d => d.A007codigoParametricaTipoRuta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T007_SALVOCONDUCTOMOVILIZACION_ADMINT_T008_PARAMETRICA_02");

            entity.HasOne(d => d.A007codigoParametricaTipoVehiculoNavigation)
                .WithMany(p => p.CitestT007SalvoconductomovilizacionA007codigoParametricaTipoVehiculoNavigations)
                .HasForeignKey(d => d.A007codigoParametricaTipoVehiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T007_SALVOCONDUCTOMOVILIZACION_ADMINT_T008_PARAMETRICA");

            entity.HasOne(d => d.A007codigoPersonaSalvoconductoNavigation)
                .WithMany(p => p.CitestT007SalvoconductomovilizacionA007codigoPersonaSalvoconductoNavigations)
                .HasForeignKey(d => d.A007codigoPersonaSalvoconducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T007_SALVOCONDUCTOMOVILIZACION_CITEST_T003_PERSONA_02");

            entity.HasOne(d => d.A007codigoPersonaTransportadorNavigation)
                .WithMany(p => p.CitestT007SalvoconductomovilizacionA007codigoPersonaTransportadorNavigations)
                .HasForeignKey(d => d.A007codigoPersonaTransportador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T007_SALVOCONDUCTOMOVILIZACION_CITEST_T003_PERSONA");
        }
    }
}
