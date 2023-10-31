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
    public class CitesT011RlCertificadoEvaluacionConfiguration : IEntityTypeConfiguration<CitestT011RlCertificadoEvaluacion>
    {
        public void Configure(EntityTypeBuilder<CitestT011RlCertificadoEvaluacion> entity)
        {
            entity.HasKey(e => e.PkT011codigo)
                  .HasName("PK_RL_CERTIFICADO_EVALUACION");

            entity.ToTable("CITEST_T011_RL_CERTIFICADO_EVALUACION");

            entity.Property(e => e.PkT011codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T011CODIGO");

            entity.Property(e => e.A011codigoCertificado)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A011CODIGO_CERTIFICADO");

            entity.Property(e => e.A011codigoEvaluacion)
                .HasColumnType("numeric(18, 0)")
                .IsRequired(true)
                .HasColumnName("A011CODIGO_EVALUACION");

            entity.Property(e => e.A011codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A011CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A011codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A011CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A011estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A011ESTADO_REGISTRO");

            entity.Property(e => e.A011fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A011FECHA_CREACION");

            entity.Property(e => e.A011fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A011FECHA_MODIFICACION");

            entity.Property(e => e.A011tipoDocumento)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A011TIPO_DOCUMENTO");

            entity.HasOne(d => d.A011codigoCertificadoNavigation)
                .WithMany(p => p.CitestT011RlCertificadoEvaluacions)
                .HasForeignKey(d => d.A011codigoCertificado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T011RL_CERTIFICADO_EVALUACION_CITEST_T001_CERTIFICADO");

            entity.HasOne(d => d.A011codigoEvaluacionNavigation)
                .WithMany(p => p.CitestT011RlCertificadoEvaluacions)
                .HasForeignKey(d => d.A011codigoEvaluacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T011RL_CERTIFICADO_EVALUACION_CITEST_T004_EVALUACION");
        }
    }
}
