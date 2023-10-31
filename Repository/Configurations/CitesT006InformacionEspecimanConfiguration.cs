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
    public class CitesT006InformacionEspecimanConfiguration : IEntityTypeConfiguration<CitestT006Informacionespeciman>
    {
        public void Configure(EntityTypeBuilder<CitestT006Informacionespeciman> entity)
        {
            entity.HasKey(e => e.PkT006codigo)
                .HasName("PK_CITEST_T006_INFORMACIONESPECIMEN");

            entity.ToTable("CITEST_T006_INFORMACIONESPECIMEN");

            entity.Property(e => e.PkT006codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T006CODIGO");

            entity.Property(e => e.A006cantidad)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A006CANTIDAD");

            entity.Property(e => e.A006cantidadRealExportada)
                .HasMaxLength(20)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A006CANTIDAD_REAL_EXPORTADA");

            entity.Property(e => e.A006codigoCertificado)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A006CODIGO_CERTIFICADO");

            entity.Property(e => e.A006codigoDocumentoPermisoPaisOrigen)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A006CODIGO_DOCUMENTO_PERMISO_PAIS_ORIGEN");

            entity.Property(e => e.A006codigoDocumentoProcedencia)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A006CODIGO_DOCUMENTO_PROCEDENCIA");

            entity.Property(e => e.A006codigoEspecimen)
                //.HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A006CODIGO_ESPECIMEN");

            entity.Property(e => e.A006codigoParametricaUnidadMedida)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A006CODIGO_PARAMETRICA_UNIDAD_MEDIDA");

            entity.Property(e => e.A006codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A006CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A006codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A006CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A006estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A006ESTADO_REGISTRO");

            entity.Property(e => e.A006fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A006FECHA_CREACION");

            entity.Property(e => e.A006fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A006FECHA_MODIFICACION");

            entity.Property(e => e.A006fechaProcedencia)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A006FECHA_PROCEDENCIA");

            entity.Property(e => e.A006observaciones)
                .HasMaxLength(500)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A006OBSERVACIONES");

            entity.Property(e => e.A006sexo)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A006SEXO");

            entity.Property(e => e.A007talla)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A007TALLA");

            entity.HasOne(d => d.A006codigoCertificadoNavigation)
                .WithMany(p => p.CitestT006Informacionespecimen)
                .HasForeignKey(d => d.A006codigoCertificado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T006_INFORMACIONESPECIMEN_CITEST_T001_CERTIFICADO");

            entity.HasOne(d => d.A006codigoDocumentoPermisoPaisOrigenNavigation)
                .WithMany(p => p.CitestT006InformacionespecimanA006codigoDocumentoPermisoPaisOrigenNavigations)
                .HasForeignKey(d => d.A006codigoDocumentoPermisoPaisOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T006_INFORMACIONESPECIMEN_ADMINT_T009_DOCUMENTO_03");

            entity.HasOne(d => d.A006codigoDocumentoProcedenciaNavigation)
                .WithMany(p => p.CitestT006InformacionespecimanA006codigoDocumentoProcedenciaNavigations)
                .HasForeignKey(d => d.A006codigoDocumentoProcedencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T006_INFORMACIONESPECIMEN_ADMINT_T009_DOCUMENTO");

        }
    }
}
