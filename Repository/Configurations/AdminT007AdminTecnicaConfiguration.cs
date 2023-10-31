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
    public class AdminT007AdminTecnicaConfiguration : IEntityTypeConfiguration<AdmintT007AdminTecnica>
    {
        public void Configure(EntityTypeBuilder<AdmintT007AdminTecnica> entity)
        {
            entity.HasKey(e => e.pkT007Codigo)
                  .HasName("PK_ADMINT_T017_ADMIN_TECNICA");

            entity.ToTable("ADMINT_T007_ADMIN_TECNICA");

            entity.Property(e => e.pkT007Codigo)
                  .HasColumnType("numeric(18, 0)")
                  .ValueGeneratedOnAdd()
                  .HasColumnName("PK_T007_CODIGO");

            entity.Property(e => e.a007codigoUsuarioCreacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A007CODIGO_USUARIO_CREACION");

            entity.Property(e => e.a007codigoUsuarioModificacion)
                  .HasColumnType("numeric(18, 0)")
                  .IsRequired(false)
                  .HasColumnName("A007CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.a007fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A007FECHA_CREACION");

            entity.Property(e => e.a007fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A007FECHA_MODIFICACION");

            entity.Property(e => e.a007estadoRegistro)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A007ESTADO_REGISTRO");

            entity.Property(e => e.a007valor)
                  .HasMaxLength(50)
                  .IsUnicode(false)
                  .IsRequired(true)
                  .HasColumnName("A007VALOR");

            entity.Property(e => e.a007nombre)
                  .HasMaxLength(50)
                  .IsRequired(true)
                  .HasColumnName("A007NOMBRE")
                  .IsFixedLength();

            entity.Property(e => e.a007descripcion)
                  .HasMaxLength(200)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A007DESCRIPCION");

            entity.Property(e => e.a007modulo)
                  .HasMaxLength(50)
                  .IsRequired(false)
                  .IsUnicode(false)
                  .HasColumnName("A007MODULO");
        }
    }
}
