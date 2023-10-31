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
    public class AdminT011RolConfiguration : IEntityTypeConfiguration<AdmintT011Rol>
    {
        public void Configure(EntityTypeBuilder<AdmintT011Rol> entity)
        {
            entity.HasKey(e => e.PkT011codigo)
                  .HasName("PK_ADMINT_T011_ROL");

            entity.ToTable("ADMINT_T011_ROL");

            entity.Property(e => e.PkT011codigo)
                  .HasColumnType("numeric(20, 0)")
                  .ValueGeneratedOnAdd()
                  .HasColumnName("PK_T011CODIGO");

            entity.Property(e => e.A011cargo)
                  .HasMaxLength(50)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A011CARGO");

            entity.Property(e => e.A011codigoUsuarioCreacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A011CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A011codigoUsuarioModificacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A011CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A011descripcion)
                  .HasMaxLength(200)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A011DESCRIPCION");

            entity.Property(e => e.A011estadoRegistro)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  //.IsUnicode(false)
                  .HasColumnName("A011ESTADO_REGISTRO");

            entity.Property(e => e.A011fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A011FECHA_CREACION");

            entity.Property(e => e.A011fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A011FECHA_MODIFICACION");

            entity.Property(e => e.A011nombre)
                  .HasMaxLength(50)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A011NOMBRE");

            entity.Property(e => e.A011modulo)
                  .HasMaxLength(50)
                  .IsRequired(false)
                  .IsUnicode(false)
                  .HasColumnName("A011MODULO");

            entity.Property(e => e.A011tipoUsuario)
                  .HasMaxLength(50)
                  .IsRequired(false)
                  .IsUnicode(false)
                  .HasColumnName("A011TIPO_USUARIO");
        }
    }
}
