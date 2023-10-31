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
    public class CupostT020RlSolicitudesDocumentoConfiguration : IEntityTypeConfiguration<CupostT020RlSolicitudesDocumento>
    {
        public void Configure(EntityTypeBuilder<CupostT020RlSolicitudesDocumento> entity)
        {
            entity.HasKey(e => e.Pk_T020Codigo)
                  .HasName("PK_T020CODIGO");

            entity.ToTable("CUPOST_T020_RL_SOLICITUDES_DOCUMENTO");

            entity.Property(e => e.Pk_T020Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T020CODIGO");

            entity.Property(e => e.A020CodigoSolicitud)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A020CODIGO_SOLICITUD");


            entity.Property(e => e.A020CodigoDocumento)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A020CODIGO_DOCUMENTO");

            entity.Property(e => e.A020CodigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A020CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A020CodigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A020CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A020FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A020FECHA_CREACION");

            entity.Property(e => e.A020FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A020FECHA_MODIFICACION");

            entity.Property(e => e.A020EstadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A020ESTADO_REGISTRO");

        }
    }
}
