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
    public class CupostT023RlCupoDocumentoConfiguration : IEntityTypeConfiguration<CupostT023RlCupoDocumento>
    {
        public void Configure(EntityTypeBuilder<CupostT023RlCupoDocumento> entity)
        {
            entity.HasKey(e => e.Pk_T023Codigo)
                  .HasName("PK_T023CODIGO");

            entity.ToTable("CUPOST_T023_RL_CUPO_DOCUMENTO");

            entity.Property(e => e.Pk_T023Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T023CODIGO");

            entity.Property(e => e.A023CodigoCupo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A023CODIGO_CUPO");

            entity.Property(e => e.A023CodigoDocuemento)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A023CODIGO_DOCUMENTO");

            entity.Property(e => e.A023FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A023FECHA_CREACION");

            entity.Property(e => e.A023FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A023FECHA_MODIFICACION");

            entity.Property(e => e.A023UsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A023CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A023UsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A023CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A023EstadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A023ESTADO_REGISTRO");

        }
    }
}
