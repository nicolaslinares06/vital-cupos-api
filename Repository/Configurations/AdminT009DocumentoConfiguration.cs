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
    public class AdminT009DocumentoConfiguration : IEntityTypeConfiguration<AdmintT009Documento>
    {
        public void Configure(EntityTypeBuilder<AdmintT009Documento> entity)
        {
            entity.HasKey(e => e.PkT009codigo)
                  .HasName("PK_ADMINT_T009_DOCUMENTO");

            entity.ToTable("ADMINT_T009_DOCUMENTO");

            entity.Property(e => e.PkT009codigo)
                  .HasColumnType("numeric(20, 0)")
                  .ValueGeneratedOnAdd()
                  .HasColumnName("PK_T009CODIGO");

            entity.Property(e => e.A009codigoUsuarioCreacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A009CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A009codigoUsuarioModificacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A009CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A009codigoParametricaTipoDocumento)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A009CODIGO_PARAMETRICA_TIPO_DOCUMENTO");

            entity.Property(e => e.A009codigoPlantilla)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A009CODIGO_PLANTILLA");

            entity.Property(e => e.A009estadoRegistro)
                  .HasColumnType("numeric(20, 0)")
                  .IsUnicode(false)
                  .IsRequired(true)
                  .HasColumnName("A009ESTADO_REGISTRO");

            entity.Property(e => e.A009fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A009FECHA_CREACION");

            entity.Property(e => e.A009fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A009FECHA_MODIFICACION");

            entity.Property(e => e.A009firmaDigital)
                  .HasMaxLength(50)
                  .IsUnicode(false)
                  .IsRequired(true)
                  .HasColumnName("A009FIRMA_DIGITAL");

            entity.Property(e => e.A009documento)
                  .HasMaxLength(500)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A009DOCUMENTO");

            entity.Property(e => e.A009descripcion)
                  .HasMaxLength(500)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A009DESCRIPCION");

            entity.Property(e => e.A009url)
                  .HasMaxLength(500)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A009URL");

            entity.HasOne(d => d.A009codigoParametricaTipoDocumentoNavigation)
                .WithMany(p => p.AdmintT009Documentos)
                .HasForeignKey(d => d.A009codigoParametricaTipoDocumento)
                .HasConstraintName("FK_ADMINT_T009_DOCUMENTO_ADMINT_T008_PARAMETRICA");

            entity.HasOne(d => d.A009codigoPlantillaNavigation)
                .WithMany(p => p.AdmintT009Documentos)
                .HasForeignKey(d => d.A009codigoPlantilla)
                .HasConstraintName("FK_ADMINT_T009_DOCUMENTO_ADMINT_T006_PLANTILLA_DOCUMENTO");
        }
    }
}
