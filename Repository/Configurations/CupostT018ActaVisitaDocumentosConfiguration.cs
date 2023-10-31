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
    public class CupostT018ActaVisitaDocumentosConfiguration : IEntityTypeConfiguration<CupostT018ActaVisitaDocumentos>
    {
        public void Configure(EntityTypeBuilder<CupostT018ActaVisitaDocumentos> entity)
        {
            entity.HasKey(e => e.Pk_T018Codigo)
                  .HasName("PK_T018CODIGO");

            entity.ToTable("CUPOST_T018_ACTA_VISITA_DOCUMENTOS");

            entity.Property(e => e.Pk_T018Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T018CODIGO");

            entity.Property(e => e.A018CodigoActaVisita)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A018CODIGO_ACTA_VISITA");

            entity.Property(e => e.A018RutaDocumento)
             .HasMaxLength(200)
             .IsRequired(true)
             .IsUnicode(false)
             .HasColumnName("A018RUTA_DOCUMENTO");


            entity.Property(e => e.A018CodigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A018CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A018CodigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A018CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A018FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A018FECHA_CREACION");

            entity.Property(e => e.A018FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A018FECHA_MODIFICACION");

            entity.Property(e => e.A018NombreArchivo)
              .HasMaxLength(100)
              .IsRequired(true)
              .IsUnicode(false)
              .HasColumnName("A018NOMBRE_ARCHIVO");


        }
    }
}
