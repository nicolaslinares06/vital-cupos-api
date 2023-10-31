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
    public class AdminT015RLUsuarioRolConfiguration : IEntityTypeConfiguration<AdmintT015RlUsuarioRol>
    {
        public void Configure(EntityTypeBuilder<AdmintT015RlUsuarioRol> entity)
        {
            entity.HasKey(e => e.PkT0015codigo)
                    .HasName("PK_RL_USUARIO_ROL");

            entity.ToTable("ADMINT_T015_RL_USUARIO_ROL");

            entity.Property(e => e.PkT0015codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T0015CODIGO");

            entity.Property(e => e.A015codigoRol)
                  .IsRequired(true)
                  .HasColumnName("A015CODIGO_ROL");

            entity.Property(e => e.A015codigoUsuario)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A015CODIGO_USUARIO");

            entity.Property(e => e.A015codigoUsuarioCreacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .HasColumnName("A015CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A015codigoUsuarioModificacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A015CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A015estadoRegistro)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(true)
                  .IsUnicode(false)
                  .HasColumnName("A015ESTADO_REGISTRO");

            entity.Property(e => e.A015fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A015FECHA_CREACION")
                  .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.A015fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A015FECHA_MODIFICACION");

            entity.HasOne(d => d.A015codigoUsuarioNavigation)
                .WithMany(p => p.AdmintT015RlUsuarioRols)
                .HasForeignKey(d => d.A015codigoUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RL_USUARIO_ROL_ADMINT_T012_USUARIO");

            entity.Property(e => e.A015estadoSolicitud)
                .HasMaxLength(20)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A015ESTADO_SOLICITUD");
        }
    }
}
