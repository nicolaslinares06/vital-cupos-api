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
    public class CuposT008CortePielConfiguration : IEntityTypeConfiguration<CupostT008CortePiel>
    {
        public void Configure(EntityTypeBuilder<CupostT008CortePiel> entity)
        {
            entity.HasKey(e => e.A008codigo)
                  .HasName("PK_CUPOST_T008_PIELES");

            entity.ToTable("CUPOST_T008_CORTE_PIEL");

            entity.Property(e => e.A008codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("A008CODIGO");

            entity.Property(e => e.A008areaPromedio)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A008AREA_PROMEDIO");

            entity.Property(e => e.A008cantidad)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A008CANTIDAD");

            entity.Property(e => e.A008codigoActaVisita)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A008CODIGO_ACTA_VISITA");

            entity.Property(e => e.A008tipoCorte)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A008TIPO_CORTE");

            entity.Property(e => e.A008tipoParte)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A008TIPO_PARTE");

            entity.Property(e => e.A008tipoPiel)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A008TIPO_PIEL");

            entity.Property(e => e.A008total)
                .HasColumnType("numeric(18, 0)")
                .IsRequired(true)
                .HasColumnName("A008TOTAL");

            entity.Property(e => e.A008TipoCorteParteCode)
               .HasColumnType("numeric(20, 0)")
               .IsRequired(true)
               .HasColumnName("A008TIPO_CORTE_PARTE_CODIGO");
        }
    }
}
