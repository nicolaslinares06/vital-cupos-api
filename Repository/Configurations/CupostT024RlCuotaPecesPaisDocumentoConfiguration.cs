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
    public class CupostT024RlCuotaPecesPaisDocumentoConfiguration : IEntityTypeConfiguration<CupostT024RlCuotaPecesPaisDocumento>
    {
        public void Configure(EntityTypeBuilder<CupostT024RlCuotaPecesPaisDocumento> entity)
        {
            entity.HasKey(e => e.Pk_T024Codigo)
                  .HasName("PK_T024CODIGO");

            entity.ToTable("CUPOST_T024_RL_CUOTA_PECES_PAIS_DOCUMENTO");

            entity.Property(e => e.Pk_T024Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T024CODIGO");

            entity.Property(e => e.A024CodigoCuotaPecesPais)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A024CODIGO_CUOTA_PECES_PAIS");

            entity.Property(e => e.A024CodigoDocumento)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A024CODIGO_DOCUMENTO");

            entity.Property(e => e.A024FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A024FECHA_CREACION");

            entity.Property(e => e.A024FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A024FECHA_MODIFICACION");

            entity.Property(e => e.A024UsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A024CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A024UsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A024CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A024EstadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A024ESTADO_REGISTRO");

        }
    }
}
