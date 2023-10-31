using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configurations
{
    public class AdminT019ExceptionLogCongiguration : IEntityTypeConfiguration<AdminT019ExceptionLog>
    {
        public void Configure(EntityTypeBuilder<AdminT019ExceptionLog> entity)
        {
            entity.HasKey(e => e.PkT019codigo)
                  .HasName("PK_ADMINT_T019_EXCEPTIONLOG");

            entity.ToTable("ADMINT_T019_EXCEPTIONLOG");

            entity.Property(e => e.PkT019codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T019CODIGO");

            entity.Property(e => e.A019Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("A019_TIMESTAMP");

            entity.Property(e => e.A019Mensaje)
                .HasMaxLength(500)
                .HasColumnName("A019_MENSAJE");

            entity.Property(e => e.A019Detalles)
                .HasMaxLength(500)
                .HasColumnName("A019_DETALLES");

            entity.Property(e => e.A019Fuente)
                .HasMaxLength(500)
                .HasColumnName("A019_FUENTE");

            entity.Property(e => e.A019Tipo)
                .HasMaxLength(500)
                .HasColumnName("A019_TIPO");

            entity.Property(e => e.A019StackTrace)
                .HasMaxLength(500)
                .HasColumnName("A019_STACKTRACE");

            entity.Property(e => e.A019Modulo)
               .HasMaxLength(255)
               .HasColumnName("A019_MODULO");
        }
    }
}