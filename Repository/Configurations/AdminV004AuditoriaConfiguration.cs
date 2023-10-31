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
    public class AdminV004AuditoriaConfiguration : IEntityTypeConfiguration<AdmintV004Auditoria>
    {
        public void Configure(EntityTypeBuilder<AdmintV004Auditoria> entity)
        {
            entity.HasNoKey();

            entity.ToView("ADMINT_V004_AUDITORIA");

            entity.Property(e => e.pkT013codigo)
                .HasColumnType("numeric(20,0)")
                .HasColumnName("PK_T013CODIGO");

            entity.Property(e => e.usuarioAuditado)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("USUARIO_AUDITADO");

            entity.Property(e => e.rol)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Rol");

            entity.Property(e => e.modulo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("MODULO");

            entity.Property(e => e.fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");

            entity.Property(e => e.accion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ACCION");

            entity.Property(e => e.ip)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("IP");
        }
    }
}
