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
    public class AdminT004CiudadConfiguration : IEntityTypeConfiguration<AdmintT004Ciudad>
    {
        public void Configure(EntityTypeBuilder<AdmintT004Ciudad> entity)
        {
            entity.HasKey(e => e.PkT004codigo)
                  .HasName("PK_ADMINT_T004_CIUDAD");

            entity.ToTable("ADMINT_T004_CIUDAD");

            entity.Property(e => e.PkT004codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T004CODIGO");

            entity.Property(e => e.A004codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A004codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A004CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A004codigoDepartamento)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004CODIGO_DEPARTAMENTO");

            entity.Property(e => e.A004estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A004ESTADO_REGISTRO");

            entity.Property(e => e.A004fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A004FECHA_CREACION");

            entity.Property(e => e.A004fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A004FECHA_MODIFICACION");

            entity.Property(e => e.A004nombre)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A004NOMBRE");

            entity.HasOne(d => d.A004codigoDepartamentoNavigation)
                .WithMany(p => p.AdmintT004Ciudads)
                .HasForeignKey(d => d.A004codigoDepartamento)
                .HasConstraintName("FK_ADMINT_T004_CIUDAD_ADMINT_T003_DEPARTAMENTO");
        }
    }
}
