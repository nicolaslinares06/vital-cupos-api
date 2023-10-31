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
    public class CupostT028SalvoconductosSolicitudConfiguration : IEntityTypeConfiguration<CupostT028SalvoconductosSolicitud>
    {
        public void Configure(EntityTypeBuilder<CupostT028SalvoconductosSolicitud> entity)
        {
            entity.HasKey(e => e.Pk_T028Codigo)
                  .HasName("PK_T028CODIGO");

            entity.ToTable("CUPOST_T028_SALVOCONDUCTOS_SOLICITUD");

            entity.Property(e => e.Pk_T028Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T028CODIGO");

            entity.Property(e => e.A028CodigoActaVisitaSalvoconducto)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A028CODIGO_ACTAVISITASALVOCONDUCTO");

            entity.Property(e => e.A028CodigoSolicitud)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A028CODIGO_SOLICITUD");

            entity.Property(e => e.A028CodigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A028CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A028CodigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A028CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A028FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A028FECHA_CREACION");

            entity.Property(e => e.A028FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A028FECHA_MODIFICACION");

            entity.Property(e => e.A028EstadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A028ESTADO_REGISTRO");
        }
    }
}
