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
    public class AdminT008ParametricaConfiguration : IEntityTypeConfiguration<AdmintT008Parametrica>
    {
        public void Configure(EntityTypeBuilder<AdmintT008Parametrica> entity)
        {
                entity.HasKey(e => e.PkT008codigo)
                    .HasName("PK_ADMINT_T008_PLANTILLADOCUMENTO");

                entity.ToTable("ADMINT_T008_PARAMETRICA");

                entity.Property(e => e.PkT008codigo)
                    .HasColumnType("numeric(20, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PK_T008CODIGO");

                entity.Property(e => e.A008codigoUsuarioCreacion)
                    .HasColumnType("numeric(20, 0)")
                    .IsRequired(true)
                    .HasColumnName("A008CODIGO_USUARIO_CREACION");

                entity.Property(e => e.A008codigoUsuarioModificacion)
                    .HasColumnType("numeric(20, 0)")
                    .IsRequired(false)
                    .HasColumnName("A008CODIGO_USUARIO_MODIFICACION");

                entity.Property(e => e.A008estadoRegistro)
                        .HasColumnType("numeric(20, 0)")
                        .IsRequired(true)
                        .IsUnicode(false)
                        .HasColumnName("A008ESTADO_REGISTRO");

                entity.Property(e => e.A008fechaCreacion)
                        .HasColumnType("datetime")
                        .IsRequired(true)
                        .HasColumnName("A008FECHA_CREACION");

                entity.Property(e => e.A008fechaModificacion)
                        .HasColumnType("datetime")
                        .IsRequired(false)
                        .HasColumnName("A008FECHA_MODIFICACION");

                entity.Property(e => e.A008modulo)
                        .HasMaxLength(6)
                        .IsRequired(false)
                        .IsUnicode(false)
                        .HasColumnName("A008MODULO");

                entity.Property(e => e.A008parametrica)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(false)
                        .HasColumnName("A008PARAMETRICA");

                entity.Property(e => e.A008valor)
                        .HasMaxLength(1024)
                        .IsRequired(true)
                        .IsUnicode(false)
                        .HasColumnName("A008VALOR");

                entity.Property(e => e.A008descripcion)
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnName("A008DESCRIPCION");
        }
    }
}
