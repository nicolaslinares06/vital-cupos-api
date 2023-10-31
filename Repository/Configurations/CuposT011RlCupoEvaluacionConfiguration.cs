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
    public class CuposT011RlCupoEvaluacionConfiguration : IEntityTypeConfiguration<CupostT011RlCupoEvaluacion>
    {
        public void Configure(EntityTypeBuilder<CupostT011RlCupoEvaluacion> entity)
        {
            entity.HasKey(e => e.PkT011codigo)
                  .HasName("PK_CITEST_T011RL_CUPO_EVALUACION");

            entity.ToTable("CUPOST_T011_RL_CUPO_EVALUACION");

            entity.Property(e => e.PkT011codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T011CODIGO");

            entity.Property(e => e.A011codigoCupo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A011CODIGO_CUPO");

            entity.Property(e => e.A011codigoEvaluacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
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
        }
    }
}
