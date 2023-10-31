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
    public class CitesT003PersonaConfiguration : IEntityTypeConfiguration<CitestT003Persona>
    {
        public void Configure(EntityTypeBuilder<CitestT003Persona> entity)
        {
            entity.HasKey(e => e.PkT003codigo)
                .HasName("PK_CITEST_T003_PERSONA");

            entity.ToTable("CITEST_T003_PERSONA");

            entity.Property(e => e.PkT003codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T003CODIGO");

            entity.Property(e => e.A003aceptaTerminosycondiciones)
                .IsRequired(true)
                .HasColumnName("A003ACEPTA_TERMINOSYCONDICIONES");

            entity.Property(e => e.A003aceptaTratamientoDatosPersonales)
                .IsRequired(true)
                .HasColumnName("A003ACEPTA_TRATAMIENTO_DATOS_PERSONALES");

            entity.Property(e => e.A003apellidos)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A003APELLIDOS");

            entity.Property(e => e.A003codigoCiudad)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A003CODIGO_CIUDAD");

            entity.Property(e => e.A003codigoParametricaTipoIdentificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A003CODIGO_PARAMETRICA_TIPO_IDENTIFICACION");

            entity.Property(e => e.A003codigoPuertoEntrada)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A003CODIGO_PUERTO_ENTRADA");

            entity.Property(e => e.A003codigoPuertoSalida)
                .HasColumnType("numeric(18, 0)")
                .IsRequired(true)
                .HasColumnName("A003CODIGO_PUERTO_SALIDA");

            entity.Property(e => e.A003codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A003CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A003codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A003CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A003correoElectronico)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A003CORREO_ELECTRONICO");

            entity.Property(e => e.A003direccion)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A003DIRECCION");

            entity.Property(e => e.A003estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A003ESTADO_REGISTRO");

            entity.Property(e => e.A003fax)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A003FAX");

            entity.Property(e => e.A003fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A003FECHA_CREACION");

            entity.Property(e => e.A003fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A003FECHA_MODIFICACION");

            entity.Property(e => e.A003identificacion)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A003IDENTIFICACION");

            entity.Property(e => e.A003nombres)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A003NOMBRES");

            entity.Property(e => e.A003telefono)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A003TELEFONO");

            entity.Property(e => e.A003segundoNombre)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("A003SEGUNDO_NOMBRE");

            entity.Property(e => e.A003segundoApellido)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("A003SEGUNDO_APELLIDO");

            entity.HasOne(d => d.A003codigoCiudadNavigation)
                .WithMany(p => p.CitestT003Personas)
                .HasForeignKey(d => d.A003codigoCiudad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T003_PERSONA_ADMINT_T004_CIUDAD");

            entity.HasOne(d => d.A003codigoParametricaTipoIdentificacionNavigation)
                .WithMany(p => p.CitestT003Personas)
                .HasForeignKey(d => d.A003codigoParametricaTipoIdentificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T003_PERSONA_ADMINT_T008_PARAMETRICA");

            entity.HasOne(d => d.A003codigoPuertoEntradaNavigation)
                .WithMany(p => p.CitestT003Personas)
                .HasForeignKey(d => d.A003codigoPuertoEntrada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T003_PERSONA_ADMINT_T001_PUERTOS");
        }
    }
}
