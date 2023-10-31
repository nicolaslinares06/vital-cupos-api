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
    public class AdminV002RoleConfiguration : IEntityTypeConfiguration<AdmintV002Role>
    {
        public void Configure(EntityTypeBuilder<AdmintV002Role> entity)
        {
            entity.HasNoKey();

            entity.ToView("ADMINT_V002_ROLES");

            entity.Property(e => e.A010descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("A010DESCRIPCION");

            entity.Property(e => e.A011cargo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A011CARGO");

            entity.Property(e => e.A011estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsUnicode(false)
                .HasColumnName("A011ESTADO_REGISTRO");

            entity.Property(e => e.A014actualizar).HasColumnName("A014ACTUALIZAR");

            entity.Property(e => e.A014codigoRol)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("A014CODIGO_ROL");

            entity.Property(e => e.A014consultar).HasColumnName("A014CONSULTAR");

            entity.Property(e => e.A014crear).HasColumnName("A014CREAR");

            entity.Property(e => e.A014eliminar).HasColumnName("A014ELIMINAR");

            entity.Property(e => e.A014verDetalle).HasColumnName("A014VER_DETALLE");

            entity.Property(e => e.PkT010codigo)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("PK_T010CODIGO");
        }
    }
}
