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
    public class AdminT001PuertoConfiguration : IEntityTypeConfiguration<AdmintT001Puerto>
    {
        public void Configure(EntityTypeBuilder<AdmintT001Puerto> entity)
        {
                entity.HasKey(e => e.PkTcodigo)
                    .HasName("PK_ADMINT_T001_PUERTOS");

                entity.ToTable("ADMINT_T001_PUERTO");

                entity.Property(e => e.PkTcodigo)
                    .HasColumnType("numeric(20, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PK_TCODIGO");

                entity.Property(e => e.A001codigoCiudad)
                    .HasColumnType("numeric(20, 0)")
                    .HasColumnName("A001CODIGO_CIUDAD");

                entity.Property(e => e.A001codigoUsuarioCreacion)
                    .HasColumnType("numeric(20, 0)")
                    .HasColumnName("A001CODIGO_USUARIO_CREACION");

                entity.Property(e => e.A001codigoUsuarioModificacion)
                    .HasColumnType("numeric(20, 0)")
                    .HasColumnName("A001CODIGO_USUARIO_MODIFICACION");

                entity.Property(e => e.A001direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("A001DIRECCION");

                entity.Property(e => e.A001estadoEstrategia)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("A001ESTADO_ESTRATEGIA");

                entity.Property(e => e.A001fechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("A001FECHA_CREACION");

                entity.Property(e => e.A001fechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("A001FECHA_MODIFICACION");

                entity.Property(e => e.A001modoTransporte)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("A001MODO_TRANSPORTE");

                entity.Property(e => e.A001nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("A001NOMBRE");

                entity.HasOne(d => d.A001codigoCiudadNavigation)
                    .WithMany(p => p.AdmintT001Puertos)
                    .HasForeignKey(d => d.A001codigoCiudad)
                    .HasConstraintName("FK_ADMINT_T001_PUERTOS_ADMINT_T004_CIUDAD");
        }
    }
}
