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
    public class AdminT014RlRolModuloPermisoConfiguration : IEntityTypeConfiguration<AdmintT014RlRolModuloPermiso>
    {
        public void Configure(EntityTypeBuilder<AdmintT014RlRolModuloPermiso> entity)
        {
            entity.HasKey(e => e.PkT014codigo)
                    .HasName("PK_RL_ROL_MODULO");

            entity.ToTable("ADMINT_T014_RL_ROL_MODULO_PERMISOS");

            entity.Property(e => e.PkT014codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T014CODIGO");

            entity.Property(e => e.A014actualizar)
                .IsRequired(true)
                .HasColumnName("A014ACTUALIZAR");

            entity.Property(e => e.A014codigoModulo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A014CODIGO_MODULO");

            entity.Property(e => e.A014codigoRol)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A014CODIGO_ROL");

            entity.Property(e => e.A014codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A014CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A014codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A014CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A014consultar)
                .IsRequired(true)
                .HasColumnName("A014CONSULTAR");

            entity.Property(e => e.A014crear)
                .IsRequired(true)
                .HasColumnName("A014CREAR");

            entity.Property(e => e.A014eliminar)
                .IsRequired(true)
                .HasColumnName("A014ELIMINAR");

            entity.Property(e => e.A014estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A014ESTADO_REGISTRO");

            entity.Property(e => e.A014fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A014FECHA_CREACION");

            entity.Property(e => e.A014fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A014FECHA_MODIFICACION");

            entity.Property(e => e.A014verDetalle)
                .IsRequired(true)
                .HasColumnName("A014VER_DETALLE");
        }
    }
}
