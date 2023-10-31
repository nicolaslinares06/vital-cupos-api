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
    public class CuposV004NumeracionesPrecintosConfiguration : IEntityTypeConfiguration<CuposV004NumeracionesPrecintos>
    {
        public void Configure(EntityTypeBuilder<CuposV004NumeracionesPrecintos> entity)
        {
            entity.HasNoKey();

            entity.ToView("CUPOST_V004_NUMERACIONESPRECINTOS");

            entity.Property(e => e.ID)
                .HasColumnName("ID");

            entity.Property(e => e.IDNUMERACION)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("ID_NUMERACION");

            entity.Property(e => e.NUMEROINTERNOINICIAL)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("NUMERO_INTERNO_INICIAL");

            entity.Property(e => e.NUMEROINTERNOFINAL)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("NUMERO_INTERNO_FINAL");

            entity.Property(e => e.A027CODIGO_EMPRESA_ORIGEN_NUMERACIONES)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A027CODIGO_EMPRESA_ORIGEN_NUMERACIONES");

            entity.Property(e => e.A027CODIGO_PARAMETRICA_ORIGEN_SOLICITUD)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A027CODIGO_PARAMETRICA_ORIGEN_SOLICITUD");

        }
    }
}
