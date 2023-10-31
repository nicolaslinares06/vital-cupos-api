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
    public class CitesT010RlCertificadoDocumentoConfiguration : IEntityTypeConfiguration<CitestT010RlCertificadoDocumento>
    {
        public void Configure(EntityTypeBuilder<CitestT010RlCertificadoDocumento> entity)
        {
            entity.HasKey(e => e.PkT010codigo)
                  .HasName("PK_RL_CERTIFICADO_DOCUMENTO");

            entity.ToTable("CITEST_T010_RL_CERTIFICADO_DOCUMENTO");

            entity.Property(e => e.PkT010codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T010CODIGO");

            entity.Property(e => e.A010codigoCertificado)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A010CODIGO_CERTIFICADO");

            entity.Property(e => e.A010codigoDocumento)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A010CODIGO_DOCUMENTO");

            entity.Property(e => e.A010codigoParametricaTipoDocumento)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A010CODIGO_PARAMETRICA_TIPO_DOCUMENTO");

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

            entity.HasOne(d => d.A010codigoCertificadoNavigation)
                .WithMany(p => p.CitestT010RlCertificadoDocumentos)
                .HasForeignKey(d => d.A010codigoCertificado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RL_CERTIFICADO_DOCUMENTO_CITEST_T001_CERTIFICADO");

            entity.HasOne(d => d.A010codigoDocumentoNavigation)
                .WithMany(p => p.CitestT010RlCertificadoDocumentos)
                .HasForeignKey(d => d.A010codigoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RL_CERTIFICADO_DOCUMENTO_ADMINT_T009_DOCUMENTO");
        }
    }
}
