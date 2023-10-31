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
    public class AdminT018NotificacionConfiguration : IEntityTypeConfiguration<AdmintT018Notificacion>
    {
        public void Configure(EntityTypeBuilder<AdmintT018Notificacion> entity)
        {
            entity.HasKey(e => e.PkT018Codigo)
                  .HasName("PK_ADMINT_T017_ADMIN_TECNICA1");

            entity.ToTable("ADMINT_T018_NOTIFICACION");

            entity.Property(e => e.PkT018Codigo)
                  .HasColumnType("numeric(18, 0)")
                  .ValueGeneratedOnAdd()
                  .HasColumnName("PK_T018_CODIGO");

            entity.Property(e => e.A018codigoUsuarioCreacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A018CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A018codigoUsuarioModificacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A018CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A018correoEnvioNotificacion)
                  .HasMaxLength(100)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A018CORREO_ENVIO_NOTIFICACION");

            entity.Property(e => e.A018estadoRegistro)
                  .HasColumnType("numeric(18, 0)")
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A018ESTADO_REGISTRO");

            entity.Property(e => e.A018fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A018FECHA_CREACION");

            entity.Property(e => e.A018fechaEnvioNotificacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A018FECHA_ENVIO_NOTIFICACION");

            entity.Property(e => e.A018fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A018FECHA_MODIFICACION");

            entity.Property(e => e.A018notificacionAsunto)
                  .HasMaxLength(50)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A018NOTIFICACION_ASUNTO");

            entity.Property(e => e.A018notificacionMensaje)
                  .HasMaxLength(200)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A018NOTIFICACION_MENSAJE");
        }
    }
}
