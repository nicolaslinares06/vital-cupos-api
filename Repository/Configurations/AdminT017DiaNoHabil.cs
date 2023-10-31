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
    public class AdminT017DiaNoHabil : IEntityTypeConfiguration<AdmintT017DiaNoHabil>
    {
        public void Configure(EntityTypeBuilder<AdmintT017DiaNoHabil> entity)
        {
            entity.HasKey(e => e.PkT017Codigo)     
                 .HasName("PK_ADMINT_T017_DIA_NO_HABIL");

            entity.ToTable("ADMINT_T017_DIA_NO_HABIL");

            entity.Property(e => e.PkT017Codigo)
                  .ValueGeneratedOnAdd()
                  .HasColumnName("A017CODIGO_DIA_NO_HABIL");

            entity.Property(e => e.A017anio)
                  .HasColumnName("A017ANIO");

            entity.Property(e => e.A017fechaNoHabil)
                  .HasColumnName("A017FECHA_NO_HABIL");

            entity.Property(e => e.A017codigoUsuarioCreacion)
                 .HasColumnType("numeric(20, 0)")
                 .IsRequired(true)
                 .HasColumnName("A017CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A017codigoUsuarioModificacion)
                  .HasColumnType("numeric(20, 0)")
                  .IsRequired(false)
                  .HasColumnName("A017CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A017fechaCreacion)
                  .HasColumnType("datetime")
                  .IsRequired(true)
                  .HasColumnName("A017FECHA_CREACION");

            entity.Property(e => e.A017fechaModificacion)
                  .HasColumnType("datetime")
                  .IsRequired(false)
                  .HasColumnName("A017FECHA_MODIFICACION");

        }
    }
}
