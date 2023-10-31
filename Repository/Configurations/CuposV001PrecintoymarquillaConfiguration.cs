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
    public class CuposV001PrecintoymarquillaConfiguration : IEntityTypeConfiguration<CuposV001Precintoymarquilla>
    {
        public void Configure(EntityTypeBuilder<CuposV001Precintoymarquilla> entity)
        {
            entity.HasNoKey();

            entity.ToView("CUPOST_V001_PRECINTOSYMARQUILLAS");

            entity.Property(e => e.PKV001CODIGO)
                .HasColumnName("PK_V001CODIGO");

            entity.Property(e => e.TIPODOCUMENTO)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TIPO_DOCUMENTO");

            entity.Property(e => e.NUMERO)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("NUMERO");

            entity.Property(e => e.NOMBRE)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");

            entity.Property(e => e.NUMERORADICADO)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO_RADICADO");

            entity.Property(e => e.NUMEROPERMISOCITES)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO_PERMISO_CITES");

            entity.Property(e => e.FECHAINICIAL)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_INICIAL");

            entity.Property(e => e.FECHAFINAL)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_FINAL");

            entity.Property(e => e.NUMEROINICIAL)
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("NUMERO_INICIAL");

            entity.Property(e => e.NUMEROFINAL)
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("NUMERO_FINAL");

            entity.Property(e => e.NUMEROINTERNOINICIAL)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("NUMERO_INTERNO_INICIAL");

            entity.Property(e => e.NUMEROINTERNOFINAL)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("NUMERO_INTERNO_FINAL");

            entity.Property(e => e.VIGENCIA)
                .HasColumnType("datetime")
                .HasColumnName("VIGENCIA");

            entity.Property(e => e.CANTIDAD)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("CANTIDAD");

            entity.Property(e => e.COLOR)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COLOR");

            entity.Property(e => e.ESPECIE)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ESPECIE");

            entity.Property(e => e.CUPOSDISPONIBLES)
                .HasColumnType("int")
                .IsRequired(false)
                .HasColumnName("CUPOS_DISPONIBLES");

            entity.Property(e => e.CUPOSTOTAL)
                .HasColumnType("int")
                .IsRequired(false)
                .HasColumnName("CUPOS_TOTAL");
        }
    }
}
