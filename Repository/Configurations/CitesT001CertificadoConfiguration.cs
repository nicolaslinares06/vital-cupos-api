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
    public class CitesT001CertificadoConfiguration : IEntityTypeConfiguration<CitestT001Certificado>
    {
        public void Configure(EntityTypeBuilder<CitestT001Certificado> entity)
        {
            entity.HasKey(e => e.PkT001codigo)
                  .HasName("PK_CITEST_T001_CERTIFICADO");

            entity.ToTable("CITEST_T001_CERTIFICADO");

            entity.HasIndex(e => e.A001codigoCiudadDestino, "IXFK_CITEST_T001_CERTIFICADO_ADMINT_T004_CIUDAD");

            entity.Property(e => e.PkT001codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T001CODIGO");

            entity.Property(e => e.A001codigoCiudadDestino)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_CIUDAD_DESTINO");

            entity.Property(e => e.A001codigoCiudadEmbarque)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_CIUDAD_EMBARQUE");

            entity.Property(e => e.A001codigoDocumentoActoadministrativoProcedencia)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_DOCUMENTO_ACTOADMINISTRATIVO_PROCEDENCIA");

            entity.Property(e => e.A001codigoDocumentoPermiso)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_DOCUMENTO_PERMISO");

            entity.Property(e => e.A001codigoEmpresa)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A001CODIGO_EMPRESA");

            entity.Property(e => e.A001codigoEntidadExportador)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_ENTIDAD_EXPORTADOR");

            entity.Property(e => e.A001codigoPaisDestino)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_PAIS_DESTINO");

            entity.Property(e => e.A001codigoParametricaTipoEmbarque)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_PARAMETRICA_TIPO_EMBARQUE");

            entity.Property(e => e.A001codigoParametricaTipoPermiso)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A001CODIGO_PARAMETRICA_TIPO_PERMISO");

            entity.Property(e => e.A001codigoParametricaTipoSolictud)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_PARAMETRICA_TIPO_SOLICTUD");

            entity.Property(e => e.A001codigoPersonaApoderado)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_PERSONA_APODERADO");

            entity.Property(e => e.A001codigoPersonaDestinatario)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_PERSONA_DESTINATARIO");

            entity.Property(e => e.A001codigoPersonaTitular)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_PERSONA_TITULAR");

            entity.Property(e => e.A001codigoPuertoentrada)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_PUERTOENTRADA");

            entity.Property(e => e.A001codigoPuertosalida)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_PUERTOSALIDA");

            entity.Property(e => e.A001codigoPuertosalidaOtro)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A001CODIGO_PUERTOSALIDA_OTRO");

            entity.Property(e => e.A001codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A001CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A001codigoUsuarioEvaluadorpuerto)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_USUARIO_EVALUADORPUERTO");

            entity.Property(e => e.A001codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A001direccionDestino)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A001DIRECCION_DESTINO");

            entity.Property(e => e.A001estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A001ESTADO_REGISTRO");

            entity.Property(e => e.A001fechaArribo)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A001FECHA_ARRIBO");

            entity.Property(e => e.A001fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A001FECHA_CREACION");

            entity.Property(e => e.A001fechaEmbarque)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A001FECHA_EMBARQUE");

            entity.Property(e => e.A001fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A001FECHA_MODIFICACION");

            entity.Property(e => e.A001fechaRadicacionRespuesta)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A001FECHA_RADICACION_RESPUESTA");

            entity.Property(e => e.A001fechaRadicacionSolicitud)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A001FECHA_RADICACION_SOLICITUD");

            entity.Property(e => e.A001fechaRespuesta)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A001FECHA_RESPUESTA");

            entity.Property(e => e.A001fechaSolicitud)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A001FECHA_SOLICITUD");

            entity.Property(e => e.A001fechaVencimiento)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A001FECHA_VENCIMIENTO");

            entity.Property(e => e.A001finalidad)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A001FINALIDAD");

            entity.Property(e => e.A001firmaAprobado)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A001FIRMA_APROBADO");

            entity.Property(e => e.A001modotransporte)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A001MODOTRANSPORTE");

            entity.Property(e => e.A001numeroRadicacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001NUMERO_RADICACION");

            entity.Property(e => e.A001observaciones)
                .HasMaxLength(200)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A001OBSERVACIONES");

            entity.Property(e => e.A001NumeroCertificado)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001NUMERO_CERTIFICADO");

            entity.Property(e => e.A001codigoParametricaTipoCertificado)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A001CODIGO_PARAMETRICA_TIPO_CERTIFICADO");

            entity.Property(e => e.A001AutoridadEmiteCertificado)
               .HasMaxLength(100)
               .IsRequired(false)
               .IsUnicode(false)
               .HasColumnName("A001AUTORIDAD_EMITE_CERTIFICADO");

            entity.HasOne(d => d.A001codigoCiudadDestinoNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoCiudadDestinoNavigations)
                .HasForeignKey(d => d.A001codigoCiudadDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_ADMINT_T004_CIUDAD");

            entity.HasOne(d => d.A001codigoCiudadEmbarqueNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoCiudadEmbarqueNavigations)
                .HasForeignKey(d => d.A001codigoCiudadEmbarque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_ADMINT_T004_CIUDAD_02");

            entity.HasOne(d => d.A001codigoDocumentoPermisoNavigation)
                .WithMany(p => p.CitestT001Certificados)
                .HasForeignKey(d => d.A001codigoDocumentoPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_ADMINT_T009_DOCUMENTO");

            entity.HasOne(d => d.A001codigoEmpresaNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoEmpresaNavigations)
                .HasForeignKey(d => d.A001codigoEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_CUPOST_T001_ENTIDAD_02");

            entity.HasOne(d => d.A001codigoEntidadExportadorNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoEntidadExportadorNavigations)
                .HasForeignKey(d => d.A001codigoEntidadExportador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_CUPOST_T001_ENTIDAD");

            entity.HasOne(d => d.A001codigoPaisDestinoNavigation)
                .WithMany(p => p.CitestT001Certificados)
                .HasForeignKey(d => d.A001codigoPaisDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_ADMINT_T002_PAIS");

            entity.HasOne(d => d.A001codigoParametricaTipoEmbarqueNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoParametricaTipoEmbarqueNavigations)
                .HasForeignKey(d => d.A001codigoParametricaTipoEmbarque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_ADMINT_T008_PARAMETRICA");

            entity.HasOne(d => d.A001codigoParametricaTipoPermisoNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoParametricaTipoPermisoNavigations)
                .HasForeignKey(d => d.A001codigoParametricaTipoPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_ADMINT_T008_PARAMETRICA_02");

            entity.HasOne(d => d.A001codigoParametricaTipoSolictudNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoParametricaTipoSolictudNavigations)
                .HasForeignKey(d => d.A001codigoParametricaTipoSolictud)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_ADMINT_T008_PARAMETRICA_03");

            entity.HasOne(d => d.A001codigoParametricaTipoCertificadoNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoParametricaTipoCertificadoNavigations)
                .HasForeignKey(d => d.A001codigoParametricaTipoCertificado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_ADMINT_T008_PARAMETRICA_04");

            entity.HasOne(d => d.A001codigoPersonaApoderadoNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoPersonaApoderadoNavigations)
                .HasForeignKey(d => d.A001codigoPersonaApoderado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_CITEST_T003_PERSONA_02");

            entity.HasOne(d => d.A001codigoPersonaDestinatarioNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoPersonaDestinatarioNavigations)
                .HasForeignKey(d => d.A001codigoPersonaDestinatario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_CITEST_T003_PERSONA");

            entity.HasOne(d => d.A001codigoPersonaTitularNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoPersonaTitularNavigations)
                .HasForeignKey(d => d.A001codigoPersonaTitular)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_CITEST_T003_PERSONA_03");

            entity.HasOne(d => d.A001codigoPuertoentradaNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoPuertoentradaNavigations)
                .HasForeignKey(d => d.A001codigoPuertoentrada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_ADMINT_T001_PUERTOS");

            entity.HasOne(d => d.A001codigoPuertosalidaNavigation)
                .WithMany(p => p.CitestT001CertificadoA001codigoPuertosalidaNavigations)
                .HasForeignKey(d => d.A001codigoPuertosalida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T001_CERTIFICADO_ADMINT_T001_PUERTOS_02");
        }
    }
}
