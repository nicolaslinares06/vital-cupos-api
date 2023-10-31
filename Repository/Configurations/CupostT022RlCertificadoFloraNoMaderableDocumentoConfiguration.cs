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
    public class CupostT022RlCertificadoFloraNoMaderableDocumentoConfiguration : IEntityTypeConfiguration<CupostT022RlCertificadoFloraNoMaderableDocumento>
    {
        public void Configure(EntityTypeBuilder<CupostT022RlCertificadoFloraNoMaderableDocumento> entity)
        {
            entity.HasKey(e => e.Pk_T022Codigo)
                  .HasName("PK_T022CODIGO");

            entity.ToTable("CUPOST_T022_RL_CERTIFICADO_FLORA_NOMADERABLE_DOCUMENTO");

            entity.Property(e => e.Pk_T022Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T022CODIGO");

            entity.Property(e => e.A022CodigoCertificadoFloraNoMaderable)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A022CODIGO_CERTIFICADO_FLORA_NOMADERABLE");

            entity.Property(e => e.A022CodigoDocuemento)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A022CODIGO_DOCUMENTO");

            entity.Property(e => e.A022FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A022FECHA_CREACION");

            entity.Property(e => e.A022FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A022FECHA_MODIFICACION");

            entity.Property(e => e.A022UsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A022CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A022UsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A022CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A022EstadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A022ESTADO_REGISTRO");

        }
    }
}
