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
    public class CitesT009ActaSeguimientoConfiguration : IEntityTypeConfiguration<CitestT009ActaSeguimiento>
    {
        public void Configure(EntityTypeBuilder<CitestT009ActaSeguimiento> entity)
        {
            entity.HasKey(e => e.A009codigo)
                  .HasName("PK_CITEST_T009_ACTAS");

            entity.ToTable("CITEST_T009_ACTA_SEGUIMIENTO");

            entity.Property(e => e.A009codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("A009CODIGO");

            entity.Property(e => e.A009codigoCertificado)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A009CODIGO_CERTIFICADO");

            entity.Property(e => e.A009codigoUsuarioCreacion)
                .HasColumnType("numeric(18, 0)")
                .IsRequired(true)
                .HasColumnName("A009CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A009codigoUsuarioEvaluador)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A009CODIGO_USUARIO_EVALUADOR");

            entity.Property(e => e.A009codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A009CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A009disposicionesFinales)
                .HasMaxLength(200)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A009DISPOSICIONES_FINALES");

            entity.Property(e => e.A009estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A009ESTADO_REGISTRO");

            entity.Property(e => e.A009fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A009FECHA_CREACION");

            entity.Property(e => e.A009fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A009FECHA_MODIFICACION");

            entity.Property(e => e.A009observaciones)
                .HasMaxLength(200)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A009OBSERVACIONES");

            entity.HasOne(d => d.A009codigoCertificadoNavigation)
                .WithMany(p => p.CitestT009ActaSeguimientos)
                .HasForeignKey(d => d.A009codigoCertificado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T009_ACTA_SEGUIMIENTO_CITEST_T001_CERTIFICADO");
        }
    }
}
