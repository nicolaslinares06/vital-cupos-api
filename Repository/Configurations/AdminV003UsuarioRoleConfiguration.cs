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
    public class AdminV003UsuarioRoleConfiguration : IEntityTypeConfiguration<AdmintV003UsuarioRole>
    {
        public void Configure(EntityTypeBuilder<AdmintV003UsuarioRole> entity)
        {
            entity.HasNoKey();

            entity.ToView("ADMINT_V003_USUARIO_ROL");

            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");

            entity.Property(e => e.a012CodigoParametricaTipoUsuario)
                .IsUnicode(false)
                .HasColumnName("A012CODIGO_PARAMETRICA_TIPOUSUARIO");

            entity.Property(e => e.a012segundoNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A012SEGUNDO_NOMBRE");
            
            entity.Property(e => e.a012segundoApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A012SEGUNDO_APELLIDO");

            entity.Property(e => e.a012identificacion)
                .IsUnicode(false)
                .HasColumnName("A012IDENTIFICACION");

            entity.Property(e => e.a012correoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A012CORREO_ELECTRONICO");

            entity.Property(e => e.codigoUsuario)
                .IsUnicode(false)
                .HasColumnName("CODIGO_USUARIO");

            entity.Property(e => e.codigoRol)
                .IsUnicode(false)
                .HasColumnName("CODIGO_ROL");

            entity.Property(e => e.nombreRol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_ROL");

            entity.Property(e => e.pkT0015codigo)
                .IsUnicode(false)
                .HasColumnName("PK_T0015CODIGO");

            entity.Property(e => e.a015estadoSolicitud)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A015ESTADO_SOLICITUD");
        }
    }
}
