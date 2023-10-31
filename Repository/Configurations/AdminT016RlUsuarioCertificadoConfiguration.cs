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
    public class AdminT016RlUsuarioCertificadoConfiguration : IEntityTypeConfiguration<AdmintT016RlUsuarioCertificado>
    {
        public void Configure(EntityTypeBuilder<AdmintT016RlUsuarioCertificado> entity)
        {
            entity.HasKey(e => e.PkT016codigo)
                  .HasName("PK_RL_USUARIO_CERTIFICADO");

            entity.ToTable("ADMINT_T016_RL_USUARIO_CERTIFICADO");

            entity.Property(e => e.PkT016codigo)
                  .HasColumnType("numeric(18, 0)")
                  .ValueGeneratedOnAdd()
                  .HasColumnName("PK_T016CODIGO");

            entity.Property(e => e.A016codigoCertificado)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A016CODIGO_CERTIFICADO");

            entity.Property(e => e.A016codigoUsuario)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A016CODIGO_USUARIO");

            entity.Property(e => e.A016codigoUsuarioCreacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A016CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A016codigoUsuarioModificacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A016CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A016estadoRegistro)
                  .HasColumnType("numeric(18, 0)")
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A016ESTADO_REGISTRO");

            entity.Property(e => e.A016fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A016FECHA_CRACION");

            entity.Property(e => e.A016fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A016FECHA_MODIFICACION");

            entity.HasOne(d => d.A016codigoCertificadoNavigation)
                  .WithMany(p => p.AdmintT016RlUsuarioCertificados)
                  .HasForeignKey(d => d.A016codigoCertificado)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_RL_USUARIO_CERTIFICADO_CITEST_T001_CERTIFICADO");

            entity.HasOne(d => d.A016codigoUsuarioNavigation)
                  .WithMany(p => p.AdmintT016RlUsuarioCertificados)
                  .HasForeignKey(d => d.A016codigoUsuario)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_RL_USUARIO_CERTIFICADO_ADMINT_T012_USUARIO");
        }
    }
}
