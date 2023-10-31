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
    public class CuposT001EmpresaConfiguration : IEntityTypeConfiguration<CupostT001Empresa>
    {
        public void Configure(EntityTypeBuilder<CupostT001Empresa> entity)
        {
            entity.HasKey(e => e.PkT001codigo)
                  .HasName("PK_CUPOST_T001_EMPRESA");

            entity.ToTable("CUPOST_T001_EMPRESA");

            entity.Property(e => e.PkT001codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T001CODIGO");

            entity.Property(e => e.A001codigoCiudad)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A001CODIGO_CIUDAD");

            entity.Property(e => e.A001codigoParametricaTipoEntidad)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A001CODIGO_PARAMETRICA_TIPO_ENTIDAD");

            entity.Property(e => e.A001codigoPersonaRepresentantelegal)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A001CODIGO_PERSONA_REPRESENTANTELEGAL");

            entity.Property(e => e.A001codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A001CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A001codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A001correo)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A001CORREO");

            entity.Property(e => e.A001direccion)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A001DIRECCION");

            entity.Property(e => e.A001estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A001ESTADO_REGISTRO");

            entity.Property(e => e.A001fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A001FECHA_CREACION");

            entity.Property(e => e.A001fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A001FECHA_MODIFICACION");

            entity.Property(e => e.A001nit)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A001NIT");

            entity.Property(e => e.A001nombre)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A001NOMBRE");

            entity.Property(e => e.A001telefono)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A001TELEFONO");

            entity.Property(e => e.A001matriculaMercantil)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A001MATRICULA_MERCANTIL");

            entity.Property(e => e.A001numeroInternoInicial)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001NUMERO_INTERNO_INICIAL");

            entity.Property(e => e.A001numeroInternoFinal)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001NUMERO_INTERNO_FINAL");

            entity.HasOne(d => d.A001codigoCiudadNavigation)
                .WithMany(p => p.CupostT001Empresas)
                .HasForeignKey(d => d.A001codigoCiudad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUPOST_T001_EMPRESA_ADMINT_T004_CIUDAD");

            entity.HasOne(d => d.A001codigoParametricaTipoEntidadNavigation)
                .WithMany(p => p.CupostT001Empresas)
                .HasForeignKey(d => d.A001codigoParametricaTipoEntidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUPOST_T001_EMPRESA_ADMINT_T008_PARAMETRICA");
        }
    }
}
