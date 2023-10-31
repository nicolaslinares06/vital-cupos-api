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
    public class CupostT019SolicitudesConfiguration : IEntityTypeConfiguration<CupostT019Solicitudes>
    {
        public void Configure(EntityTypeBuilder<CupostT019Solicitudes> entity)
        {
            entity.HasKey(e => e.Pk_T019Codigo)
                  .HasName("PK_T019CODIGO");

            entity.ToTable("CUPOST_T019_SOLICITUDES");

            entity.Property(e => e.Pk_T019Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T019CODIGO");

            entity.Property(e => e.A019CodigoCiudad)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A019CODIGO_CIUDAD");

            entity.Property(e => e.A019DireccionEntrega)
                .HasMaxLength(100)
                .IsRequired(true)
                .HasColumnName("A019DIRECCION_ENTREGA");

            entity.Property(e => e.A019FechaSolicitud)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A019FECHA_SOLICITUD");

            entity.Property(e => e.A019FechaConsignacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A019FECHA_CONSIGNACION");

            entity.Property(e => e.A019CodigoEspecieExportar)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A019CODIGO_ESPECIEEXPORTAR");

            entity.Property(e => e.A019Cantidad)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A019CANTIDAD");

            entity.Property(e => e.A019LongitudMenor)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A019LONGITUD_MENOR");

            entity.Property(e => e.A019LongitudMayor)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A019LONGITUD_MARYOR");

            entity.Property(e => e.A019CodigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A019CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A019CodigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A019CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A019FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A019FECHA_CREACION");

            entity.Property(e => e.A019FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A019FECHA_MODIFICACION");

            entity.Property(e => e.A019EstadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A019ESTADO_REGISTRO");

            entity.Property(e => e.A019EstadoSolicitud)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A019ESTADO_SOLICITUD");

            entity.Property(e => e.A019NumeroRadicacion)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("A019NUMERO_RADICACION");

            entity.Property(e => e.A019FechaRadicacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A019FECHA_RADICACION");

            entity.Property(e => e.A019Observaciones)
                .HasMaxLength(200)
                .IsRequired(false)
                .HasColumnName("A019OBSERVACIONES");

            entity.Property(e => e.A019Respuesta)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("A019RESPUESTA");

            entity.Property(e => e.A019CodigoEmpresa)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A019CODIGO_EMPRESA");

            entity.Property(e => e.A019FechaCambioEstado)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A019FECHA_CAMBIO_ESTADO_SOLICITUD");

            entity.Property(e => e.A019ObservacionesDesistimiento)
                .HasMaxLength(200)
                .IsRequired(false)
                .HasColumnName("A019OBSERVACIONES_DESISTIMIENTO");

           entity.Property(e => e.A019AnalistaAsignacion)
            .HasColumnType("numeric(20, 0)")
            .IsRequired(false)
            .HasColumnName("A019ANALISTA_ASIGNACION");

            entity.Property(e => e.A019ValorConsignacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A019VALOR_CONSIGNACION");

            entity.Property(e => e.A019NumeroRadicacionSalida)
               .HasMaxLength(50)
                .IsRequired(false)
               .HasColumnName("A019NUMERO_RADICACION_SALIDA");

            entity.Property(e => e.A019FechaRadicacionSalida)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A019FECHA_RADICACION_SALIDA");

            entity.Property(e => e.A019TipoSolicitud)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("A019TIPO_SOLICITUD");

        }
    }
}
