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
    public class CuposV002ReportesPrecintosConfiguration : IEntityTypeConfiguration<CuposV002ReportesPrecintos>
    {
        public void Configure(EntityTypeBuilder<CuposV002ReportesPrecintos> entity)
        {
            entity.HasNoKey();

            entity.ToView("CUPOST_CU01_V002_REPORTESPRECINTOS");

            entity.Property(e => e.NUmeroRadicacion)
                .HasMaxLength(50)
                .HasColumnName("NUMERO_RADICACION");

            entity.Property(e => e.FechaRadicacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_RADICACION");

            entity.Property(e => e.CodigoCiudad)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("CODIGO_CIUDAD");

            entity.Property(e => e.DireccionEntrega)
              .HasMaxLength(50)
              .HasColumnName("DIRECCION_ENTREGA");

            entity.Property(e => e.LongMenor)
             .HasColumnType("numeric(20, 0)")
             .HasColumnName("LONGITUD_MENOR");

            entity.Property(e => e.LongMayor)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("LONGITUD_MAYOR");

            entity.Property(e => e.Cantidad)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("CANTIDAD");

            entity.Property(e => e.CodigoEmpresa)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("CODIGO_EMPRESA");

            entity.Property(e => e.ValorConsignacion)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("VALOR_CONSIGNACION");

            entity.Property(e => e.Analista)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("ANALISTA");

            entity.Property(e => e.PrimerNombreAnalista)
               .HasMaxLength(50)
               .HasColumnName("PRIMER_NOMBRE_ANALISTA");

            entity.Property(e => e.PrimerApellidoAnalista)
               .HasMaxLength(50)
               .HasColumnName("PRIMER_APELLIDO_ANALISTA");

            entity.Property(e => e.Especie)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("ESPECIE");

            entity.Property(e => e.NIT)
              .HasColumnType("numeric(20, 0)")
              .HasColumnName("NIT");

            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(50)
                .HasColumnName("NOMBRE_EMPRESA");

            entity.Property(e => e.Codigo_Solicitud)
              .HasColumnType("numeric(20, 0)")
              .HasColumnName("CODIGO_SOLICITUD");


        }
    }
}
