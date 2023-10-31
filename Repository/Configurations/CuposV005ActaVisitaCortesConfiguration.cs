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
    public class CuposV005ActaVisitaCortesConfiguration : IEntityTypeConfiguration<CuposV005ActaVisitaCortes>
    {
        public void Configure(EntityTypeBuilder<CuposV005ActaVisitaCortes> entity)
        {
            entity.HasNoKey();

            entity.ToView("CUPOST_V005_ACTAVISITACORTES");

            entity.Property(e => e.ActaVisitaId)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("ACTA_VISITA_ID");

            entity.Property(e => e.TipoActaId)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("TIPO_ACTA_ID");

            entity.Property(e => e.VisitaNumero)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("VISITA_NUMERO");

            entity.Property(e => e.Establecimiento)
                .HasColumnType("varchar(50)")
                .HasColumnName("ESTABLECIMIENTO");

            entity.Property(e => e.TipoEstablecimiento)
                .HasColumnType("varchar(256)")
                .HasColumnName("TIPO_ESTABLECIMIENTO");

            entity.Property(e => e.FechaActaVisita)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_ACTA_VISITA");

            entity.Property(e => e.VisitaUno)
               .HasColumnType("bit")
               .HasColumnName("VISITA_UNO");

            entity.Property(e => e.VisitaDos)
                .HasColumnType("bit")
                .HasColumnName("VISITA_DOS");


            entity.Property(e => e.EstadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("ESTADO_REGISTRO");


            entity.Property(e => e.FechaCreacionDecimal)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("FECHA_CREACION_DECIMAL");

            entity.Property(e => e.EntidadId)
               .HasColumnType("numeric(18, 0)")
               .HasColumnName("ENTIDAD_ID");

            entity.Property(e => e.TipoEstablecimientoId)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("TIPO_ESTABLECIMIENTO_ID");

        }
    }
}
