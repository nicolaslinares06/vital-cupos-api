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
    public class CuposT007ActaVisitumConfiguration : IEntityTypeConfiguration<CupostT007ActaVisitum>
    {
        public void Configure(EntityTypeBuilder<CupostT007ActaVisitum> entity)
        {
            entity.HasKey(e => e.PkT007codigo)
                  .HasName("PK_CUPOST_T007_ACTA_VISITA");

            entity.ToTable("CUPOST_T007_ACTA_VISITA");

            entity.Property(e => e.PkT007codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T007CODIGO");

            entity.Property(e => e.A007cantidadPielesAcortar)
                .HasColumnType("numeric(18, 0)")
                .IsRequired(true)
                .HasColumnName("A007CANTIDAD_PIELES_ACORTAR");

            entity.Property(e => e.A007codigoDocumentoOrigenPieles)
                .HasColumnType("numeric(18, 0)")
                .IsRequired(true)
                .HasColumnName("A007CODIGO_DOCUMENTO_ORIGEN_PIELES");

            entity.Property(e => e.A007codigoEntidad)
                .HasColumnType("numeric(18, 0)")
                .IsRequired(true)
                .HasColumnName("A007CODIGO_ENTIDAD");

            entity.Property(e => e.A007codigoPrecintoymarquilla)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A007CODIGO_PRECINTOYMARQUILLA");

            entity.Property(e => e.A007codigoUsuarioAutoridadCites)
                .HasColumnType("numeric(18, 0)")
                .IsRequired(true)
                .HasColumnName("A007CODIGO_USUARIO_AUTORIDAD_CITES");

            entity.Property(e => e.A007codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A007CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A007codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A007CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A007estadoPieles)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A007ESTADO_PIELES");

            entity.Property(e => e.A007estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A007ESTADO_REGISTRO");

            entity.Property(e => e.A007fechaActa)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A007FECHA_ACTA");

            entity.Property(e => e.A007fechaCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A007FECHA_CREACION");

            entity.Property(e => e.A007fechaModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A007FECHA_MODIFICACION");

            entity.Property(e => e.A007firmaUsuarioAutoridadCites)
                .HasColumnType("image")
                .IsRequired(true)
                .HasColumnName("A007FIRMA_USUARIO_AUTORIDAD_CITES");

            entity.Property(e => e.A007observaciones)
                .HasMaxLength(200)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A007OBSERVACIONES");

            entity.Property(e => e.A007TipoActa)
                .HasColumnType("numeric(2, 0)")
                .IsRequired(true)
                .HasColumnName("A007TIPOACTAVISITA");

            entity.Property(e => e.A007RepresentanteIdentificacion)
               .HasColumnType("numeric(20, 0)")
               .IsRequired(true)
               .HasColumnName("A007IDENTIFICACION_REPRESENTANTE");

            entity.Property(e => e.A007RepresentanteNombre)
              .HasMaxLength(100)
              .IsRequired(true)
              .IsUnicode(false)
              .HasColumnName("A007NOMBRE_REPRESENTANTE");     
            
            
            entity.Property(e => e.A007CiudadDepartamento)
              .HasColumnType("numeric(20, 0)")
              .IsRequired(true)
              .HasColumnName("A007CIUDAD_DEPARTAMENTO");

            entity.Property(e => e.A007VisitaNumero)
               .HasColumnType("numeric(2, 0)")
               .IsRequired(true)
               .HasColumnName("A007NUMERO_VISITA");

            entity.Property(e => e.A007VisitaNumero1)
               .HasColumnType("bit")
               .IsRequired(true)
               .HasColumnName("A007NUMERO_PRIMERAVISITA");
            

            entity.Property(e => e.A007VisitaNumero2)
               .HasColumnType("bit")
               .IsRequired(true)
               .HasColumnName("A007NUMERO_SEGUNDAVISITA");


            entity.Property(e => e.A007PrecintoAdjunto)
              .HasMaxLength(200)
              .IsRequired(true)
              .IsUnicode(false)
              .HasColumnName("A007PRECINTO_ADJUNTO");

        }
    }
}
