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
    public class CitesT008EstadoConfiguration : IEntityTypeConfiguration<CitestT008Estado>
    {
        public void Configure(EntityTypeBuilder<CitestT008Estado> entity)
        {
            entity.HasKey(e => e.PkT008codigo)
                .HasName("PK_CITEST_T008_ESTADO");

            entity.ToTable("CITEST_T008_ESTADO");

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

            entity.Property(e => e.A008descripcion)
                .HasMaxLength(200)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A008DESCRIPCION");

            entity.Property(e => e.A008codigoParametricaEstado)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A008CODIGO_PARAMETRICA_ESTADO");

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

            entity.Property(e => e.A008posicion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A008POSICION");

            entity.Property(e => e.A008etapa)
                .HasMaxLength(50)
                .IsRequired(true)
                .HasColumnName("A008ETAPA");

            entity.Property(e => e.A008modulo)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("A008MODULO");
        }
    }
}
