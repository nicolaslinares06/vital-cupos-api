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
    public class CupostT026FacturaCompraCupoConfiguration : IEntityTypeConfiguration<CupostT026FacturaCompraCupo>
    {
        public void Configure(EntityTypeBuilder<CupostT026FacturaCompraCupo> entity)
        {
            entity.HasKey(e => e.Pk_T026Codigo)
                  .HasName("PK_T026CODIGO");

            entity.ToTable("CUPOST_T026_FACTURA_COMPRA_CUPO");

            entity.Property(e => e.Pk_T026Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T026CODIGO");

            entity.Property(e => e.A026CodigoFacturaCompra)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A026CODIGO_FACTURA_COMPRA");

            entity.Property(e => e.A026CodigoCupo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A026CODIGO_CUPO");

            entity.Property(e => e.A026NumeracionInicial)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A026NUMERACION_INICIAL");

            entity.Property(e => e.A026NumeracionFinal)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A026NUMERACION_FINAL");

            entity.Property(e => e.A026NumeracionInicialRepoblacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A026NUMERACION_INICIAL_REPOBLACION");

            entity.Property(e => e.A026NumeracionFinalRepoblacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A026NUMERACION_FINAL_REPOBLACION");

            entity.Property(e => e.A026NumeracionInicialPrecintos)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A026NUMERACION_INICIAL_PRECINTOS");

            entity.Property(e => e.A026NumeracionFinalPrecintos)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A026NUMERACION_FINAL_PRECINTOS");

            entity.Property(e => e.A026CuposDisponibles)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A026CUPOS_DISPONIBLES");

            entity.Property(e => e.A026CantidadEspecimenesComprados)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A026CANTIDAD_ESPECIMENES_COMPRADOS");
        }
    }
}
