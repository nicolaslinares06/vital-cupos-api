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
    public class CuposT010CantidadCuotaPecesPaisConfiguration : IEntityTypeConfiguration<CupostT010CantidadCuotaPecesPai>
    {
        public void Configure(EntityTypeBuilder<CupostT010CantidadCuotaPecesPai> entity)
        {
            entity.HasKey(e => e.PkT010codigo)
                  .HasName("PK_CUPOST_T010_CUOTA_PECES_PAIS");

            entity.ToTable("CUPOST_T010_CANTIDAD_CUOTA_PECES_PAIS");

            entity.Property(e => e.PkT010codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T010CODIGO");

            entity.Property(e => e.A0010codigoParametricaRegion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A0010CODIGO_PARAMETRICA_REGION");

            entity.Property(e => e.A010codigoCuotaPecesPais)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A010CODIGO_CUOTA_PECES_PAIS");

            entity.Property(e => e.A010codigoEspecimen)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A010CODIGO_ESPECIMEN");

            entity.Property(e => e.A010codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A010CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A010codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A010CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A010cuota)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A010CUOTA");

            entity.Property(e => e.A010estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A010ESTADO_REGISTRO");

            entity.Property(e => e.A010fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A010FECHA_CREACION");

            entity.Property(e => e.A010fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A010FECHA_MODIFICACION");

            entity.Property(e => e.A010total)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A010TOTAL");

            entity.HasOne(e => e.A010codigoCuotaPecesPaisNavigation)
                .WithMany(p => p.CupostT010CantidadCuotaPecesPai)
                .HasForeignKey(e => e.A010codigoCuotaPecesPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUPOST_T010_CANTIDAD_CUOTA_PECES_PAIS_CUPOST_T009_CUOTA_PECES_PAIS");
        }
    }
}
