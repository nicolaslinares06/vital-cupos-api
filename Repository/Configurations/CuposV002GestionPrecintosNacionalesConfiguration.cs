using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Models;

namespace Repository.Configurations
{
    public class CuposV002GestionPrecintosNacionalesConfiguration : IEntityTypeConfiguration<CuposV002GestionPrecintosNacionales>
    {
        public void Configure(EntityTypeBuilder<CuposV002GestionPrecintosNacionales> entity)
        {
            entity.HasNoKey();

            entity.ToView("CUPOST_V002_GESTIONPRECINTOSNACIONALES");

            entity.Property(e => e.ID)
                .HasColumnName("ID");

            entity.Property(e => e.NUMERORADICADO)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO_RADICADO");

            entity.Property(e => e.FECHARADICADO)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_RADICADO");

            entity.Property(e => e.PRECINTOSNACIONALES)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PRECINTOS_NACIONALES");

            entity.Property(e => e.ENTIDAD)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ENTIDAD");

            entity.Property(e => e.FECHASOLICITUD)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_SOLICITUD");
                      
            entity.Property(e => e.ESTADO)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ESTADO");

            entity.Property(e => e.ANALISTA)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ANALISTA");

            entity.Property(e => e.NUMERORADICADOSALIDA)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO_RADICADO_SALIDA");

            entity.Property(e => e.FECHARADICADOSALIDA)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_RADICADO_SALIDA");

            entity.Property(e => e.TIPOSOLICITUD)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TIPO_SOLICITUD");
        }
    }
}
