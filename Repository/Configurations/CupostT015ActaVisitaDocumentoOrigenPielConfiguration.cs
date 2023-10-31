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
    public class CupostT015ActaVisitaDocumentoOrigenPielConfiguration : IEntityTypeConfiguration<CupostT015ActaVisitaDocumentoOrigenPiel>
    {
        public void Configure(EntityTypeBuilder<CupostT015ActaVisitaDocumentoOrigenPiel> entity)
        {
            entity.HasKey(e => e.PK_T015Codigo)
                  .HasName("PK_T015CODIGO");

            entity.ToTable("CUPOST_T015_ACTA_VISITA_DOCUMENTO_ORIGEN_PIEL");

            entity.Property(e => e.PK_T015Codigo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T015CODIGO");

            entity.Property(e => e.A015CodigoActaVisita)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)                
                .HasColumnName("A015CODIGO_ACTA_VISITA"); 
            
            
            entity.Property(e => e.A015DocumentoOrigenPielNumero)  
                .HasMaxLength(50)
                .IsRequired(true)
                .HasColumnName("A015DOCUMENTO_ORIGEN_PIEL_NUMERO");

            entity.Property(e => e.A015CodigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A015CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A015CodigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A015CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A015FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A015FECHA_CREACION");

            entity.Property(e => e.A015FechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A015FECHA_MODIFICACION");

     
        }

    }
}
