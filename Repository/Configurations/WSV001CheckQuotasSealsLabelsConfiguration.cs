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
    public class Wsv001CheckQuotasSealsLabelsConfiguration : IEntityTypeConfiguration<Wsv001CheckQuotasSealsLabels>
    {
        public void Configure(EntityTypeBuilder<Wsv001CheckQuotasSealsLabels> entity)
        {
            entity.HasNoKey();

            entity.ToView("WS_V001_CHECKQUOTASSEALSLABELS");

            entity.Property(e => e.ID)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");

            entity.Property(e => e.NIT)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("NIT");

            entity.Property(e => e.NOMBRECIENTIFICO)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("NOMBRE_CIENTIFICO");

            entity.Property(e => e.NOMBRECOMUN)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("NOMBRE_COMUN");

            entity.Property(e => e.CUPOS)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("CUPOS");
            
            entity.Property(e => e.SALDO)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("SALDO");

            entity.Property(e => e.VIGENCIA)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("VIGENCIA");

            entity.Property(e => e.NUMERACIONINICIALPRECINTOS)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("NUMERACION_INICIAL_PRECINTOS");

            entity.Property(e => e.NUMERACIONFINALPRECINTOS)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("NUMERACION_FINAL_PRECINTOS");

            entity.Property(e => e.NUMERACIONINICIALMARQUILLA)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("NUMERACION_INICIAL_MARQUILLA");

            entity.Property(e => e.NUMERACIONFINALMARQUILLA)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("NUMERACION_FINAL_MARQUILLA");
        }
    }
}
