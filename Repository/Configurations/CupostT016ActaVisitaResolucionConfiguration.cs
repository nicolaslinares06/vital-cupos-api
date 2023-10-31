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
    public class CupostT016ActaVisitaResolucionConfiguration : IEntityTypeConfiguration<CupostT016ActaVisitaResolucion>
    {
        public void Configure(EntityTypeBuilder<CupostT016ActaVisitaResolucion> entity)
        {
            entity.HasKey(e => e.PK_T016Codigo)
                  .HasName("PK_T016CODIGO");

            entity.ToTable("CUPOST_T016_ACTA_VISITA_RESOLIUCION_NUMERO");

            entity.Property(e => e.PK_T016Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T016CODIGO");

            entity.Property(e => e.A016CodigoActaVisita)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A016CODIGO_ACTA_VISITA");


            entity.Property(e => e.A016ResolucionNumero)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A016RESOLUCION_NUMERO");

            entity.Property(e => e.A016CodigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A016CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A016CodigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A016CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A016FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A016FECHA_CREACION");

            entity.Property(e => e.A016FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A016FECHA_MODIFICACION");


        }
    }
}
