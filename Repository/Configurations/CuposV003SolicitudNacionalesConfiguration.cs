using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Models;

namespace Repository.Configurations
{
    public class CuposV003SolicitudPrecintosNacionalesConfiguration : IEntityTypeConfiguration<CuposV003SolicitudPrecintosNacionales>
    {
        public void Configure(EntityTypeBuilder<CuposV003SolicitudPrecintosNacionales> entity)
        {
            entity.HasNoKey();

            entity.ToView("CUPOST_V003_SOLICITUDPRECINTOSNACIONALES");

            entity.Property(e => e.ID)
                .HasColumnName("ID");

            entity.Property(e => e.CIUDAD)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CIUDAD");

            entity.Property(e => e.DEPARTAMENTO)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DEPARTAMENTO");

            entity.Property(e => e.FECHA)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");

            entity.Property(e => e.ESTABLECIMIENTO)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ESTABLECIMIENTO");

            entity.Property(e => e.PRIMERNOMBRE)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PRIMER_NOMBRE");

            entity.Property(e => e.SEGUNDONOMBRE)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SEGUNDO_NOMBRE");

            entity.Property(e => e.PRIMERAPELLIDO)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PRIMER_APELLIDO");

            entity.Property(e => e.SEGUNDOAPELLIDO)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SEGUNDO_APELLIDO");

            entity.Property(e => e.TIPOIDENTIFICACION)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TIPO_IDENTIFICACION");
            
            entity.Property(e => e.NUMEROIDENTIFICACION)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO_IDENTIFICACION");

            entity.Property(e => e.DIRECCIONENTREGA)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DIRECCION_ENTREGA");

            entity.Property(e => e.CIUDADENTREGA)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CIUDAD_ENTREGA");

            entity.Property(e => e.TELEFONO)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");

            entity.Property(e => e.CORREOELECTRONICO)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CORREO_ELECTRONICO");

            entity.Property(e => e.CANTIDAD)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CANTIDAD");

            entity.Property(e => e.ESPECIE)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ESPECIE");

            entity.Property(e => e.CODIGOINICIAL)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGO_INICIAL");

            entity.Property(e => e.CODIGOFINAL)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGO_FINAL");

            entity.Property(e => e.LONGITUDMENOR)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LONGITUD_MENOR");

            entity.Property(e => e.LONGITUDMAYOR)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LONGITUD_MAYOR");

            entity.Property(e => e.FECHALEGAL)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_LEGAL");

            entity.Property(e => e.OBSERVACIONES)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OBSERVACIONES");

            entity.Property(e => e.RESPUESTA)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RESPUESTA");

            entity.Property(e => e.FECHADESISTIMIENTO)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_DESISTIMIENTO");

            entity.Property(e => e.NUMERORADICACION)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO_RADICACION");

            entity.Property(e => e.FECHARADICACION)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_RADICACION");

            entity.Property(e => e.FECHAESTADO)
               .HasColumnType("datetime")
               .HasColumnName("FECHA_ESTADO");

            entity.Property(e => e.OBSERVACIONESDESISTIMIENTO)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OBSERVACIONES_DESISTIMIENTO");

            entity.Property(e => e.ANALISTA)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ANALISTA");

            entity.Property(e => e.CODIGOESPECIE)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGO_ESPECIE");

            entity.Property(e => e.NUMERORADICACIONSALIDA)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO_RADICACION_SALIDA");

            entity.Property(e => e.FECHARADICACIONSALIDA)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_RADICACION_SALIDA");

            entity.Property(e => e.VALORCONSIGNACION)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VALOR_CONSIGNACION");

            entity.Property(e => e.TIPOSOLICITUD)
                .IsUnicode(false)
                .HasColumnName("TIPO_SOLICITUD");

            entity.Property(e => e.CODIGONUMERACIONES)
                .HasColumnName("CODIGO_NUMERACIONES");

            entity.Property(e => e.TYPEREQUESTCODE)
                .HasColumnName("TIPO_SOLICITUD_CODIGO");

            entity.Property(e => e.CODIGOCORTEPIELSOLICITUD)
                .HasColumnName("CODIGO_CORTEPIEL_SOLICITUD");

            entity.Property(e => e.NITEMPRESA)
                .HasColumnName("NIT_EMPRESA");

            entity.Property(e => e.CODIGOZOOCRIADERO)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGO_ZOOCRIADERO");
        }
    }
}
