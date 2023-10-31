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
    public class CuposT012RlNovedadDocumentoConfiguration : IEntityTypeConfiguration<CupostT012RlNovedadDocumento>
    {
        public void Configure(EntityTypeBuilder<CupostT012RlNovedadDocumento> entity)
        {
            entity.HasKey(e => e.PkT012codigo)
                  .HasName("PK_CUPOST_T009_CANTIDADFLORANOMADERABLE");

            entity.ToTable("CUPOST_T012_RL_NOVEDAD_DOCUMENTO");

            entity.Property(e => e.PkT012codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T012CODIGO");

            entity.Property(e => e.A012codigoDocumento)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A012CODIGO_DOCUMENTO");

            entity.Property(e => e.A012codigoNovedad)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A012CODIGO_NOVEDAD");

            entity.Property(e => e.A012codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A012CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A012codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A012CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A012estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A012ESTADO_REGISTRO");

            entity.Property(e => e.A012fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A012FECHA_CREACION");

            entity.Property(e => e.A012fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A012FECHA_MODIFICACION");
        }
    }
}
