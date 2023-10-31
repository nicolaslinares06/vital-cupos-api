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
    public class AdminT006PlantillaDocumentoConfiguration : IEntityTypeConfiguration<AdmintT006PlantillaDocumento>
    {
        public void Configure(EntityTypeBuilder<AdmintT006PlantillaDocumento> entity)
        {
            entity.HasKey(e => e.PkT006codigo)
                  .HasName("PK_ADMINT_T006_PARAMETRICA");

            entity.ToTable("ADMINT_T006_PLANTILLA_DOCUMENTO");

            entity.Property(e => e.PkT006codigo)
                  .HasColumnType("numeric(20, 0)")
                  .ValueGeneratedOnAdd()
                  .HasColumnName("PK_T006CODIGO");

            entity.Property(e => e.A006codigoUsuarioCreacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A006CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A006codigoUsuarioModificacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A006CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A006estadoRegistro)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .IsUnicode(false)
                  .HasColumnName("A006ESTADO_REGISTRO");

            entity.Property(e => e.A006fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A006FECHA_CREACION");

            entity.Property(e => e.A006fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A006FECHA_MODIFICACION");

            entity.Property(e => e.A006nombre)
                  .HasMaxLength(50)
                  .IsRequired(false)
                  .IsUnicode(false)
                  .HasColumnName("A006NOMBRE");

            entity.Property(e => e.A006descripcion)
                  .HasMaxLength(200)
                  .IsRequired(false)
                  .IsUnicode(false)
                  .HasColumnName("A006DESCRIPCION");

            entity.Property(e => e.A006plantillaUrl)
                  .HasMaxLength(500)
                  .IsRequired(false)
                  .IsUnicode(false)
                  .HasColumnName("A006PLANTILLA_URL");
        }
    }
}
