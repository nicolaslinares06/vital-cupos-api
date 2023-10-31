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
    public class CitesT004EvaluacionConfiguration : IEntityTypeConfiguration<CitestT004Evaluacion>
    {
        public void Configure(EntityTypeBuilder<CitestT004Evaluacion> entity)
        {
            entity.HasKey(e => e.PkT004codigo)
                .HasName("PK_CITEST_T004_EVALUACION");

            entity.ToTable("CITEST_T004_EVALUACION");

            entity.Property(e => e.PkT004codigo)
                .HasColumnType("numeric(18, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T004CODIGO");

            entity.Property(e => e.A004codigoCertificado)
                .HasMaxLength(20)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A004CODIGO_CERTIFICADO");

            entity.Property(e => e.A004codigoDocumento)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004CODIGO_DOCUMENTO");

            entity.Property(e => e.A004codigoUsuarioAsigna)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004CODIGO_USUARIO_ASIGNA");

            entity.Property(e => e.A004codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A004codigoUsuarioEvaluadopor)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A004CODIGO_USUARIO_EVALUADOPOR");

            entity.Property(e => e.A004codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A004CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A004estadoCertificado)
                .HasMaxLength(20)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A004ESTADO_CERTIFICADO");

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

            entity.Property(e => e.A004fechaVencimiento)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A004FECHA_VENCIMIENTO");

            entity.Property(e => e.A004notas)
                .HasMaxLength(200)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A004NOTAS");

            entity.Property(e => e.A004observacion)
                .HasMaxLength(200)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A004OBSERVACION");

            entity.Property(e => e.A004fechaCambioEstado)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A004FECHA_CAMBIO_ESTADO");

            entity.HasOne(d => d.A004codigoUsuarioAsignaNavigation)
                .WithMany(p => p.CitestT004EvaluacionA004codigoUsuarioAsignaNavigations)
                .HasForeignKey(d => d.A004codigoUsuarioAsigna)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T004_EVALUACION_ADMINT_T012_USUARIO_03");

            entity.HasOne(d => d.A004codigoUsuarioEvaluadoporNavigation)
                .WithMany(p => p.CitestT004EvaluacionA004codigoUsuarioEvaluadoporNavigations)
                .HasForeignKey(d => d.A004codigoUsuarioEvaluadopor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T004_EVALUACION_ADMINT_T012_USUARIO_04");
        }
    }
}
