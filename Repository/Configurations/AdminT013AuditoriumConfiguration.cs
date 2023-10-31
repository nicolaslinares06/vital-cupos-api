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
    public class AdminT013AuditoriumConfiguration : IEntityTypeConfiguration<AdmintT013Auditorium>
    {
        public void Configure(EntityTypeBuilder<AdmintT013Auditorium> entity)
        {
            entity.HasKey(e => e.PkT013codigo)
                  .HasName("PK_ADMIN_T013_AUDITORIA");

            entity.ToTable("ADMINT_T013_AUDITORIA");

            entity.Property(e => e.PkT013codigo)
                  .HasColumnType("numeric(20, 0)")
                  .ValueGeneratedOnAdd()
                  .HasColumnName("PK_T013CODIGO");

            entity.Property(e => e.A013codigoUsuarioCreacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A013CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A013codigoUsuarioModificacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A013CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A013fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A013FECHA_CREACION");

            entity.Property(e => e.A013fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A013FECHA_MODIFICACION");

            entity.Property(e => e.A013estadoRegistro)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A013ESTADO_REGISTRO");

            entity.Property(e => e.A013codigoUsuarioAuditado)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A013CODIGO_USUARIO_AUDITADO");

            entity.Property(e => e.A013codigoRol)
                  .IsRequired(false)
                  .HasColumnName("A013CODIGO_ROL");

            entity.Property(e => e.A013codigoModulo)
                  .HasMaxLength(50)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A013CODIGO_MODULO");

            entity.Property(e => e.A013fechaHora)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A013FECHA_HORA");

            entity.Property(e => e.A013accion)
                  .HasMaxLength(50)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A013ACCION");

            entity.Property(e => e.A013ip)
                  .HasMaxLength(50)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A013IP");

            entity.Property(e => e.A013estadoAnterior)
                  .IsUnicode(false)
                  .IsRequired(false)
                  .HasColumnName("A013ESTADO_ANTERIOR");

            entity.Property(e => e.A013estadoActual)
                  .IsUnicode(false)
                  .IsRequired(false)
                  .HasColumnName("A013ESTADO_ACTUAL");

            entity.Property(e => e.A013camposModificados)
                  .HasMaxLength(50)
                  .IsUnicode(false)
                  .IsRequired(false)
                  .HasColumnName("A013CAMPOS_MODIFICADOS");

            entity.Property(e => e.A013registroModificado)
                  .HasMaxLength(50)
                  .IsUnicode(false)
                  .IsRequired(false)
                  .HasColumnName("A013REGISTRO_MODIFICADO");
        }
    }
}
