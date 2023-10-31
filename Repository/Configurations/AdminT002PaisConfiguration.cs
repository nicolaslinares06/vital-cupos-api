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
    public class AdminT002PaisConfiguration : IEntityTypeConfiguration<AdmintT002Pai>
    {
        public void Configure(EntityTypeBuilder<AdmintT002Pai> entity)
        {
                entity.HasKey(e => e.PkT002codigo);

                entity.ToTable("ADMINT_T002_PAIS");

                entity.Property(e => e.PkT002codigo)
                    .HasColumnType("numeric(20, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PK_T002CODIGO");

                entity.Property(e => e.A002codigoUsuarioCreacion)
                    .HasColumnType("numeric(20, 0)")
                    .HasColumnName("A002CODIGO_USUARIO_CREACION");

                entity.Property(e => e.A002codigoUsuarioModificacion)
                    .HasColumnType("numeric(20, 0)")
                    .HasColumnName("A002CODIGO_USUARIO_MODIFICACION");

                entity.Property(e => e.A002estadoRegistro)
                    .HasColumnType("numeric(20, 0)")
                    .IsUnicode(false)
                    .HasColumnName("A002ESTADO_REGISTRO");

                entity.Property(e => e.A002fechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("A002FECHA_CREACION");

                entity.Property(e => e.A002fechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("A002FECHA_MODIFICION");

                entity.Property(e => e.A002nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("A002NOMBRE");
        }
    }
}
