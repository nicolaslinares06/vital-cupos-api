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
    public class AdminV001UsuarioConfiguration : IEntityTypeConfiguration<AdmintV001Usuario>
    {
        public void Configure(EntityTypeBuilder<AdmintV001Usuario> entity)
        {
            entity.HasNoKey();

            entity.ToView("ADMINT_V001_USUARIOS");

            entity.Property(e => e.A012correoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A012CORREO_ELECTRONICO");

            entity.Property(e => e.A012direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A012DIRECCION");

            entity.Property(e => e.A012estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsUnicode(false)
                .HasColumnName("A012ESTADO_REGISTRO");

            entity.Property(e => e.A012Celular)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A012CELULAR");

            entity.Property(e => e.A012fechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("A012FECHA_CREACION");

            entity.Property(e => e.A012fechaExpiracontraseña)
                .HasColumnType("datetime")
                .HasColumnName("A012FECHA_EXPIRACONTRASEÑA");

            entity.Property(e => e.A012fechaFinContrato)
                .HasColumnType("datetime")
                .HasColumnName("A012FECHA_FIN_CONTRATO");

            entity.Property(e => e.A012fechaInicioContrato)
                .HasColumnType("datetime")
                .HasColumnName("A012FECHA_INICIO_CONTRATO");

            entity.Property(e => e.A012fechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("A012FECHA_MODIFICACION");

            entity.Property(e => e.A012identificacion)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("A012IDENTIFICACION");

            entity.Property(e => e.A012login)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A012LOGIN");

            entity.Property(e => e.A012primerApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A012PRIMER_APELLIDO");

            entity.Property(e => e.A012primerNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A012PRIMER_NOMBRE");

            entity.Property(e => e.A012segundoApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A012SEGUNDO_APELLIDO");

            entity.Property(e => e.A012segundoNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A012SEGUNDO_NOMBRE");

            entity.Property(e => e.A012telefono)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("A012TELEFONO");

            entity.Property(e => e.PkT012codigo)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("PK_T012CODIGO");

            entity.Property(e => e.Roles)
                .HasMaxLength(8000)
                .IsUnicode(false);
        }
    }
}
