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
    public class CitesV001AnalistasConfiguration : IEntityTypeConfiguration<CitestV001Analistas>
    {
        public void Configure(EntityTypeBuilder<CitestV001Analistas> entity)
        {
            entity.HasNoKey();

            entity.ToView("CITEST_V001_ANALISTAS");

            entity.Property(e => e.PkV001Codigo)
                .HasColumnName("PK_V001CODIGO");

            entity.Property(e => e.A001PrimerNombre)
                .HasMaxLength(50)
                .HasColumnName("A001PRIMER_NOMBRE");

            entity.Property(e => e.A001PrimerApellido)
                .HasMaxLength(50)
                .HasColumnName("A001PRIMER_APELLIDO");

            entity.Property(e => e.A001Descripcion)
                .HasMaxLength(200)
                .HasColumnName("A001DESCRIPCION");

            entity.Property(e => e.A001Asignados)
                 .HasColumnName("A001ASIGNADOS");
        }
    }
}
