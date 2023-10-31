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
    internal class AdminT005EspecimanConfiguration : IEntityTypeConfiguration<AdmintT005Especiman>
    {
        public void Configure(EntityTypeBuilder<AdmintT005Especiman> entity)
        {
            entity.HasKey(e => e.PkT005codigo)
                  .HasName("PK_ADMINT_T005_ESPECIMEN");

            entity.ToTable("ADMINT_T005_ESPECIMEN");

            entity.Property(e => e.PkT005codigo)
                  .HasColumnType("numeric(20, 0)")
                  .ValueGeneratedOnAdd()
                  .HasColumnName("PK_T005CODIGO");

            entity.Property(e => e.A005codigoUsuarioCreacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A005CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A005codigoUsuarioModificacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A005CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A005codigoTipoEspecimen)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A005CODIGO_TIPO_ESPECIMEN");

            entity.Property(e => e.A005fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A005FECHA_CREACION");

            entity.Property(e => e.A005fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A005FECHA_MODIFICACION");

            entity.Property(e => e.A005estadoRegistro)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A005ESTADO_REGISTRO");

            entity.Property(e => e.A005apendice)
                  .HasMaxLength(50)
                  .IsRequired(false)
                  .IsUnicode(false)
                  .HasColumnName("A005APENDICE");

            entity.Property(e => e.A005nombreCientifico)
                  .HasMaxLength(500)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A005NOMBRE_CIENTIFICO");

            entity.Property(e => e.A005nombre)
                  .HasMaxLength(500)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A005NOMBRE");

            entity.Property(e => e.A005nombreComun)
                  .IsUnicode(false)
                  .IsRequired(true)
                  .HasColumnName("A005NOMBRE_COMUN");

            entity.Property(e => e.A005familia)
                  .HasMaxLength(200)
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A005FAMILIA");

            entity.Property(e => e.A005reino)
                  .HasMaxLength(50)
                  .IsUnicode(false)
                  .IsRequired(false)
                  .HasColumnName("A005REINO");

            entity.Property(e => e.A005clase)
                  .HasMaxLength(50)
                  .IsUnicode(false)
                  .IsRequired(false)
                  .HasColumnName("A005CLASE");
        }
    }
}
