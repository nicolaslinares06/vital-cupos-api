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
    public class CupostT017ActaVisitaSalvoConductoConfiguration : IEntityTypeConfiguration<CupostT017ActaVisitaSalvoConducto>
    {
        public void Configure(EntityTypeBuilder<CupostT017ActaVisitaSalvoConducto> entity)
        {
            entity.HasKey(e => e.PK_T017Codigo)
                  .HasName("PK_T017CODIGO");

            entity.ToTable("CUPOST_T017_ACTA_VISITA_SALVOCONDUCTO");

            entity.Property(e => e.PK_T017Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T017CODIGO");

            entity.Property(e => e.A017CodigoActaVisita)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A017CODIGO_ACTA_VISITA");


            entity.Property(e => e.A017SalvoConductoNumero)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A017SALVOCONDUCTO_NUMERO");

            entity.Property(e => e.A017CodigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A017CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A017CodigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A017CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A017FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A017FECHA_CREACION");

            entity.Property(e => e.A017FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A017FECHA_MODIFICACION");


        }

    }
}
