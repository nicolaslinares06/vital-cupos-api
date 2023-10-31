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
    public class CuposT006PrecintosymarquillaConfiguration : IEntityTypeConfiguration<CupostT006Precintosymarquilla>
    {
        public void Configure(EntityTypeBuilder<CupostT006Precintosymarquilla> entity)
        {
            entity.HasKey(e => e.PkT006codigo)
                  .HasName("PK_CUPOST_T009_PRECINTOSYMARQUILLAS");

            entity.ToTable("CUPOST_T006_PRECINTOSYMARQUILLAS");

            entity.Property(e => e.PkT006codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T006CODIGO");

            entity.Property(e => e.A006codigoEspecieExportar)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A006CODIGO_ESPECIE_EXPORTAR");

            entity.Property(e => e.A006codigoParametricaColorPrecintosymarquillas)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A006CODIGO_PARAMETRICA_COLOR_PRECINTOSYMARQUILLAS");

            entity.Property(e => e.A006codigoParametricaTipoPrecintomarquilla)
                .HasColumnType("numeric(18, 0)")
                .IsRequired(false)
                .HasColumnName("A006CODIGO_PARAMETRICA_TIPO_PRECINTOMARQUILLA");

            entity.Property(e => e.A006codigoPrecintoymarquilla)
                .HasMaxLength(50)
                .IsRequired(true)
                .HasColumnName("A006CODIGO_PRECINTOYMARQUILLA");

            entity.Property(e => e.A006codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A006CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A006codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A006CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A006estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A006ESTADO_REGISTRO");

            entity.Property(e => e.A006fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A006FECHA_CREACION");

            entity.Property(e => e.A006fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A006FECHA_MODIFICACION");

            entity.Property(e => e.A006observacion)
                .HasMaxLength(200)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A006OBSERVACION");

            entity.Property(e => e.A006numeroInicial)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("A006NUMERO_INICIAL");

            entity.Property(e => e.A006numeroFinal)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("A006NUMERO_FINAL");

            entity.Property(e => e.A006numeroInicialNumerico)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A006NUMERO_INICIAL_NUMERICO");

            entity.Property(e => e.A006numeroFinalNumerico)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A006NUMERO_FINAL_NUMERICO");

            entity.Property(e => e.A006codigoSolicitud)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A006CODIGO_SOLICITUD");

            entity.HasOne(d => d.A006codigoEspecieExportarNavigation)
                .WithMany(p => p.CupostT006Precintosymarquillas)
                .HasForeignKey(d => d.A006codigoEspecieExportar)
                .HasConstraintName("FK_CUPOST_T006_PRECINTOSYMARQUILLAS_CUPOST_T005_ESPECIEAEXPORTAR");
        }
    }
}
