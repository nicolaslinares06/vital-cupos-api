using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configurations
{
    public class CuposV001ResolucionCuposConfiguration : IEntityTypeConfiguration<CuposV001ResolucionCupos>
    {
        public void Configure(EntityTypeBuilder<CuposV001ResolucionCupos> entity)
        {
            entity.HasNoKey();

            entity.ToView("CUPOST_V001_RESOLUCION_CUPOS");

            entity.Property(e => e.codigoCupo)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("CODIGO_CUPO");

            entity.Property(e => e.numeroResolucion)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("NUMERO_RESOLUCION");

            entity.Property(e => e.fechaResolucion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_RESOLUCION");

            entity.Property(e => e.fechaRadicado)
                .IsRequired(false)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_RADICADO");

            entity.Property(e => e.cuposOtorgados)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("CUPOS_OTORGADOS");

            entity.Property(e => e.cuposPorAnio)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("CUPOS_POR_AÑO");

            entity.Property(e => e.fechaProduccion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_PRODUCCION");

            entity.Property(e => e.cuposAprovechamientoComercializacion)
                .HasMaxLength(50)
                .HasColumnName("CUPOS_APROVECHAMIENTO_COMERCIALIZACION"); 

            entity.Property(e => e.cuposTotal)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("CUPOS_TOTAL");

            entity.Property(e => e.cuotaRepoblacion)
                .HasColumnName("CUOTA_REPOBLACION");

            entity.Property(e => e.cuposDisponibles)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("CUPOS_DISPONIBLES");

            entity.Property(e => e.observaciones)
                .HasColumnName("OBSERVACIONES");

            entity.Property(e => e.codigoEmpresa)
                .HasColumnName("CODIGO_EMPRESA");

            entity.Property(e => e.autoridadEmiteResolucion)
                .IsRequired(false)
                .HasColumnName("AUTORIDAD_EMITE_RESOLUCION"); 

            entity.Property(e => e.codigoZoocriadero)
                .IsRequired(false)
                .HasColumnName("CODIGO_ZOOCRIADERO");
        entity.Property(e => e.fechaRegistroResolucion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("FECHA_REGISTRO_RESOLUCION");

        entity.Property(e => e.codigoEspecie)
                .IsRequired(false)
                .HasColumnName("CODIGO_ESPECIE");
        
        entity.Property(e => e.numeroInternoFinalCuotaRepoblacion)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("NUMERO_INTERNO_FINAL_CUOTA_REPOBLACION");

        entity.Property(e => e.numeroInternoFinal)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("NUMERO_INTERNO_FINAL");

        entity.Property(e => e.NombreEspecieExportar)
            .HasMaxLength(50)
            .HasColumnName("NOMBRE_ESPECIE_EXPORTAR");

        entity.Property(e => e.NumeroInternoInicial)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("NUMERO_INTERNO_INICIAL");

        entity.Property(e => e.numeroInternoInicialCuotaRepoblacion)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("NUMERO_INTERNO_INICIAL_CUOTA_REPOBLACION");

        entity.Property(e => e.PagoCuotaRepoblacion)
                .HasMaxLength(50)
                .HasColumnName("PAGO_CUOTA_REPOBLACION");

        entity.Property(e => e.NombreEmpresa)
            .HasMaxLength(50)
            .HasColumnName("NOMBRE_EMPRESA");

        entity.Property(e => e.NitEmpresa)
            .HasColumnType("numeric(20, 0)")
            .HasColumnName("NIT_EMPRESA");

        entity.Property(e => e.TipoEntidadEmpresa)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("TIPO_ENTIDAD_EMPRESA");
        }
    }
}
