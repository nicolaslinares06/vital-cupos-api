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
    public class CupostT021CertificadoFloraNoMaderableConfiguration : IEntityTypeConfiguration<CupostT021CertificadoFloraNoMaderable>
    {
        public void Configure(EntityTypeBuilder<CupostT021CertificadoFloraNoMaderable> entity)
        {
            entity.HasKey(e => e.Pk_T021Codigo)
                  .HasName("PK_T021CODIGO");

            entity.ToTable("CUPOST_T021_CERTIFICADO_FLORA_NOMADERABLE");

            entity.Property(e => e.Pk_T021Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T021CODIGO");

            entity.Property(e => e.A021FechaCertificacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A021FECHA_CERTIFICACION");

            entity.Property(e => e.A021FechaRegistroCertificado)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A021FECHA_REGISTRO_CERTIFICADO");

            entity.Property(e => e.A021VigenciaCertificacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A021VIGENCIA_CERTIFICACION");

            entity.Property(e => e.A021TipoCertificado)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A021TIPO_CERTIFICADO");

            entity.Property(e => e.A021AutoridadEmiteCertificado)
                .HasMaxLength(100)
                .IsRequired(true)
                .HasColumnName("A021AUTORIDAD_EMITE_CERTIFICADO");

            entity.Property(e => e.A021NumeroCertificado)
                .HasMaxLength(20)
                .IsRequired(true)
                .HasColumnName("A021NUMERO_CERTIFICADO");

            entity.Property(e => e.A021TipoPermiso)
                .HasMaxLength(10)
                .IsRequired(true)
                .HasColumnName("A021TIPO_PERMISO");

            entity.Property(e => e.A021CodigoEmpresa)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A021CODIGO_EMPRESA");

            entity.Property(e => e.A021TipoEspecimenProductoImpExp)
                .HasMaxLength(50)
                .IsRequired(true)
                .HasColumnName("A021TIPO_ESPECIMEN_PRODUCTO_IMP_EXP");

            entity.Property(e => e.A021Observaciones)
                .HasMaxLength(200)
                .IsRequired(false)
                .HasColumnName("A021OBSERVACIONES");

            entity.Property(e => e.A021FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A021FECHA_CREACION");

            entity.Property(e => e.A021FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A021FECHA_MODIFICACION");

            entity.Property(e => e.A021UsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A021CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A021UsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A021CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A021EstadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A021ESTADO_REGISTRO");

        }
    }
}
