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
    public class AdminT012UsuarioConfiguration : IEntityTypeConfiguration<AdmintT012Usuario>
    {
        public void Configure(EntityTypeBuilder<AdmintT012Usuario> entity)
        {
                entity.HasKey(e => e.PkT012codigo)
                      .HasName("PK_ADMINT_T012_USUARIO");

                entity.ToTable("ADMINT_T012_USUARIO");

                entity.HasIndex(e => e.PkT012codigo, "IXFK_ADMINT_T012_USUARIO_CITEST_T001_CERTIFICADO");

                entity.Property(e => e.PkT012codigo)
                      .HasColumnType("numeric(20, 0)")
                      .ValueGeneratedOnAdd()
                      .HasColumnName("PK_T012CODIGO");

                entity.Property(e => e.A012codigoUsuarioCreacion)
                        .HasColumnType("numeric(20, 0)")
                        .IsRequired(true)
                        .HasColumnName("A012CODIGO_USUARIO_CREACION");

                entity.Property(e => e.A012codigoUsuarioModificacion)
                        .HasColumnType("numeric(20, 0)")
                        .IsRequired(false)
                        .HasColumnName("A012CODIGO_USUARIO_MODIFICACION");

                entity.Property(e => e.A012codigoCiudadDireccion)
                        .HasColumnType("numeric(20, 0)")
                        .IsRequired(false)
                        .HasColumnName("A012CODIGO_CIUDAD_DIRECCION");

                entity.Property(e => e.A012codigoParametricaTipoDocumento)
                        .HasColumnType("numeric(20, 0)")
                        .IsRequired(false)
                        .HasColumnName("A012CODIGO_PARAMETRICA_TIPO_DOCUMENTO");

                entity.Property(e => e.A012codigoParametricaTipousuario)
                        .HasColumnType("numeric(20, 0)")
                        .IsRequired(false)
                        .HasColumnName("A012CODIGO_PARAMETRICA_TIPOUSUARIO");

                entity.Property(e => e.A012dependencia)
                        .HasMaxLength(100)
                        .IsRequired(false)
                        .HasColumnName("A012DEPENDENCIA");

                entity.Property(e => e.A012aceptaTerminos)
                        .IsRequired(true)
                        .HasColumnName("A012ACEPTA_TERMINOS");

                entity.Property(e => e.A012aceptaTratamientoDatosPersonales)
                        .IsRequired(true)
                        .HasColumnName("A012ACEPTA_TRATAMIENTO_DATOS_PERSONALES");

                entity.Property(e => e.A012identificacion)
                        .HasColumnType("numeric(20, 0)")
                        .IsRequired(true)
                        .HasColumnName("A012IDENTIFICACION");

                entity.Property(e => e.A012primerNombre)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(false)
                        .HasColumnName("A012PRIMER_NOMBRE");

                entity.Property(e => e.A012segundoNombre)
                        .HasMaxLength(50)
                        .IsRequired(false)
                        .IsUnicode(false)
                        .HasColumnName("A012SEGUNDO_NOMBRE");

                entity.Property(e => e.A012primerApellido)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(false)
                        .HasColumnName("A012PRIMER_APELLIDO");

                entity.Property(e => e.A012segundoApellido)
                        .HasMaxLength(50)
                        .IsRequired(false)
                        .IsUnicode(false)
                        .HasColumnName("A012SEGUNDO_APELLIDO");

                entity.Property(e => e.A012direccion)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(false)
                        .HasColumnName("A012DIRECCION");

                entity.Property(e => e.A012telefono)
                        .HasColumnType("numeric(20, 0)")
                        .IsRequired(true)
                        .HasColumnName("A012TELEFONO");

                entity.Property(e => e.A012correoElectronico)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(false)
                        .HasColumnName("A012CORREO_ELECTRONICO");

                entity.Property(e => e.A012celular)
                        .HasMaxLength(50)
                        .IsRequired(false)
                        .IsUnicode(false)
                        .HasColumnName("A012CELULAR");

                entity.Property(e => e.A012login)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(false)
                        .HasColumnName("A012LOGIN");

                entity.Property(e => e.A012contrasena)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(false)
                        .HasColumnName("A012CONTRASENA");

                entity.Property(e => e.A012firmaDigital)
                        .HasMaxLength(50)
                        .IsRequired(false)
                        .IsUnicode(false)
                        .HasColumnName("A012FIRMA_DIGITAL");

                entity.Property(e => e.A012estadoRegistro)
                        .HasColumnType("numeric(20, 0)")
                        .IsRequired(true)
                        .IsUnicode(false)
                        .HasColumnName("A012ESTADO_REGISTRO");

                entity.Property(e => e.A012estadoSolicitud)
                        .HasColumnType("numeric(20, 0)")
                        .IsRequired(false)
                        .HasColumnName("A012ESTADO_SOLICITUD");

                entity.Property(e => e.A012fechaCreacion)
                        .HasColumnType("datetime")
                        .IsRequired(true)
                        .HasColumnName("A012FECHA_CREACION");

                entity.Property(e => e.A012fechaModificacion)
                        .HasColumnType("datetime")
                        .IsRequired(false)
                        .HasColumnName("A012FECHA_MODIFICACION");

                entity.Property(e => e.A012fechaExpiracontraseña)
                        .HasColumnType("datetime")
                        .IsRequired(false)
                        .HasColumnName("A012FECHA_EXPIRACONTRASEÑA");

                entity.Property(e => e.A012fechaModificacionContrasena)
                        .HasColumnType("datetime")
                        .IsRequired(false)
                        .HasColumnName("A012FECHA_MODIFICACION_CONTRASENA");

                entity.Property(e => e.A012cantidadIntentosIngresoincorrecto)
                        .HasColumnType("numeric(18, 0)")
                        .IsRequired(false)
                        .HasColumnName("A012CANTIDAD_INTENTOS_INGRESOINCORRECTO");

                entity.Property(e => e.A012fechaInicioContrato)
                        .HasColumnType("datetime")
                        .IsRequired(false)
                        .HasColumnName("A012FECHA_INICIO_CONTRATO");

                entity.Property(e => e.A012fechaFinContrato)
                        .HasColumnType("datetime")
                        .IsRequired(false)
                        .HasColumnName("A012FECHA_FIN_CONTRATO");

                entity.Property(e => e.A012tokenTemporal)
                        .IsRequired(false)
                        .HasMaxLength(50)
                        .HasColumnName("A012TOKEN_TEMPORAL");

                entity.Property(e => e.A012CodigoEmpresa)
                    .HasColumnType("numeric(20, 0)")
                    .HasColumnName("A012CODIGO_EMPRESA");

                entity.Property(e => e.A012Modulo)
                    .HasMaxLength(50)
                    .IsRequired(false)
                    .IsUnicode(false)
                    .HasColumnName("A012MODULO");

                entity.Property(e => e.A012fechaDesbloqueo)
                    .HasColumnType("datetime")
                    .IsRequired(false)
                    .HasColumnName("A012FECHA_DESBLOQUEO");

                entity.HasOne(d => d.A012codigoCiudadDireccionNavigation)
                        .WithMany(p => p.AdmintT012Usuarios)
                        .HasForeignKey(d => d.A012codigoCiudadDireccion)
                        .HasConstraintName("FK_ADMINT_T012_USUARIO_ADMINT_T004_CIUDAD");

                entity.HasOne(d => d.A012codigoParametricaTipoDocumentoNavigation)
                        .WithMany(p => p.AdmintT012Usuarios)
                        .HasForeignKey(d => d.A012codigoParametricaTipoDocumento)
                        .HasConstraintName("FK_ADMINT_T012_USUARIO_ADMINT_T008_PARAMETRICA");
        }
    }
}
