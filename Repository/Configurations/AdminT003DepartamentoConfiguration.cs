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
    public class AdminT003DepartamentoConfiguration : IEntityTypeConfiguration<AdmintT003Departamento>
    {
        public void Configure(EntityTypeBuilder<AdmintT003Departamento> entity)
        {
            entity.HasKey(e => e.PkT003codigo)
                  .HasName("PK_ADMINT_T003_DEPARTAMENTO");

            entity.ToTable("ADMINT_T003_DEPARTAMENTO");

            entity.Property(e => e.PkT003codigo)
                  .HasColumnType("numeric(20, 0)")
                  .ValueGeneratedOnAdd()
                  .HasColumnName("PK_T003CODIGO");

            entity.Property(e => e.A003codigoUsuarioCreacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A003CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A003codigoUsuarioModificacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A003CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A003codigoPais)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A003CODIGO_PAIS");

            entity.Property(e => e.A003estadoRegistro)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A003ESTADO_REGISTRO");

            entity.Property(e => e.A003fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A003FECHA_CREACION");

            entity.Property(e => e.A003fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A003FECHA_MODIFICACION");

            entity.Property(e => e.A003nombre)
                  .HasMaxLength(50)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A003NOMBRE");

            entity.HasOne(d => d.A003codigoPaisNavigation)
                  .WithMany(p => p.AdmintT003Departamentos)
                  .HasForeignKey(d => d.A003codigoPais)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_ADMINT_T003_DEPARTAMENTO_ADMINT_T002_PAIS");
        }
    }
}
