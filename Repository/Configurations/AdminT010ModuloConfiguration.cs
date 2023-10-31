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
    public class AdminT010ModuloConfiguration : IEntityTypeConfiguration<AdmintT010Modulo>
    {
        public void Configure(EntityTypeBuilder<AdmintT010Modulo> entity)
        {
            entity.HasKey(e => e.PkT010codigo)
                  .HasName("PK_ADMINT_T010_MODULO");

            entity.ToTable("ADMINT_T010_MODULO");

            entity.Property(e => e.PkT010codigo)
                  .HasColumnType("numeric(20, 0)")
                  .ValueGeneratedOnAdd()
                  .HasColumnName("PK_T010CODIGO");

            entity.Property(e => e.A010codigoUsuarioCreacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A010CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A010codigoUsuarioModificacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A010CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A010estadoRegistro)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A010ESTADO_REGISTRO");

            entity.Property(e => e.A010fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A010FECHA_CREACION");

            entity.Property(e => e.A010fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A010FECHA_MODIFICACION");

            entity.Property(e => e.A010modulo)
                  .HasMaxLength(50)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A010MODULO");

            entity.Property(e => e.A010descripcion)
                .HasMaxLength(200)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A010DESCRIPCION");

            entity.Property(e => e.A010informacionAyuda)
                .HasMaxLength(1000)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A010INFORMACION_AYUDA");

            entity.Property(e => e.A010aplicativo)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A010APLICATIVO");
        }
    }
}
