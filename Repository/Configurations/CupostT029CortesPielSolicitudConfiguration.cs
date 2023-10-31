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
    public class CupostT029CortesPielSolicitudConfiguration : IEntityTypeConfiguration<CupostT029CortesPielSolicitud>
    {
        public void Configure(EntityTypeBuilder<CupostT029CortesPielSolicitud> entity)
        {
            entity.HasKey(e => e.Pk_T029Codigo)
                  .HasName("PK_T029CODIGO");

            entity.ToTable("CUPOST_T029_CORTESPIEL_SOLICITUD");

            entity.Property(e => e.Pk_T029Codigo)
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T028CODIGO");

            entity.Property(e => e.A029CodigoCortePiel)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A029CODIGO_CORTE_PIEL");
            
            entity.Property(e => e.A029CodigoSolicitud)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A029CODIGO_SOLICITUD");

            entity.Property(e => e.A029Cantidad)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A029CANTIDAD");

            entity.Property(e => e.A029AreaTotal)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A029AREA_TOTAL");

            entity.Property(e => e.A029CodigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A029CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A029CodigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A029CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A029FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A029FECHA_CREACION");

            entity.Property(e => e.A029FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A029FECHA_MODIFICACION");

            entity.Property(e => e.A029EstadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A029ESTADO_REGISTRO");
        }
    }
}
