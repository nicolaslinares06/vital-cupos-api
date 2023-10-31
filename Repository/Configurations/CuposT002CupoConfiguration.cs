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
    public class CuposT002CupoConfiguration : IEntityTypeConfiguration<CupostT002Cupo>
    {
        public void Configure(EntityTypeBuilder<CupostT002Cupo> entity)
        {
            entity.HasKey(e => e.PkT002codigo)
                  .HasName("PK_CUPOST_T002_CUPO");

            entity.ToTable("CUPOST_T002_CUPO");

            entity.Property(e => e.PkT002codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T002CODIGO");

            entity.Property(e => e.A002codigoDocumentoCarta)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A002CODIGO_DOCUMENTO_CARTA");

            entity.Property(e => e.A002codigoDocumentoConsignacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A002CODIGO_DOCUMENTO_CONSIGNACION");

            entity.Property(e => e.A002codigoDocumentoResolucion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A002CODIGO_DOCUMENTO_RESOLUCION");

            entity.Property(e => e.A002codigoEmpresa)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A002CODIGO_EMPRESA");

            entity.Property(e => e.A002codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A002CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A002codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A002CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A002cuotaRepoblacion)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A002CUOTA_REPOBLACION");

            entity.Property(e => e.A002cuposAsignados)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A002CUPOS_ASIGNADOS");

            entity.Property(e => e.A002cuposDisponibles)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A002CUPOS_DISPONIBLES");

            entity.Property(e => e.A002cuposTotal)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A002CUPOS_TOTAL");

            entity.Property(e => e.A002estadoCupo)
                .HasMaxLength(10)
                .IsRequired(false)
                .HasColumnName("A002ESTADO_CUPO")
                .IsFixedLength();

            entity.Property(e => e.A002estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A002ESTADO_REGISTRO");

            entity.Property(e => e.A002fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A002FECHA_CREACION");

            entity.Property(e => e.A002fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A002FECHA_MODIFICACION");

            entity.Property(e => e.A002fechaProduccion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A002FECHA_PRODUCCION");

            entity.Property(e => e.A002fechaRadicadoRespuesta)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A002FECHA_RADICADO_RESPUESTA");

            entity.Property(e => e.A002fechaRadicadoSolicitud)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A002FECHA_RADICADO_SOLICITUD");

            entity.Property(e => e.A002fechaResolucion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A002FECHA_RESOLUCION");

            entity.Property(e => e.A002fechaRegistroResolucion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A002FECHA_REGISTRO_RESOLUCION");

            entity.Property(e => e.A002fechaVigencia)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A002FECHA_VIGENCIA");

            entity.Property(e => e.A002numeroResolucion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A002NUMERO_RESOLUCION");

            entity.Property(e => e.A002observaciones)
                .HasMaxLength(200)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A002OBSERVACIONES");

            entity.Property(e => e.A002pielLongitudMayor)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A002PIEL_LONGITUD_MAYOR");

            entity.Property(e => e.A002pielLongitudMenor)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A002PIEL_LONGITUD_MENOR");

            entity.Property(e => e.A002precintosymarquillasValorpago)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A002PRECINTOSYMARQUILLAS_VALORPAGO");

            entity.Property(e => e.A002rangoCodigoFin)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A002RANGO_CODIGO_FIN");

            entity.Property(e => e.A002rangoCodigoInicial)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A002RANGO_CODIGO_INICIAL");

            entity.Property(e => e.A002AutoridadEmiteResolucion)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("A002AUTORIDAD_EMITE_RESOLUCION"); 

            entity.Property(e => e.A002CodigoZoocriadero)
                .HasMaxLength(20)
                .IsRequired(false)
                .HasColumnName("A002CODIGO_ZOOCRIADERO");

            entity.Property(e => e.A002NumeracionInicialPrecintos)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A002_NUMERACION_INICIAL_PRECINTOS");

            entity.Property(e => e.A002NumeracionFinalPrecintos)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A002_NUMERACION_FINAL_PRECINTOS");

            entity.HasOne(d => d.A002codigoEmpresaNavigation)
                .WithMany(p => p.CupostT002Cupos)
                .HasForeignKey(d => d.A002codigoEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUPOST_T002_CUPO_CUPOST_T001_ENTIDAD");
        }
    }
}
