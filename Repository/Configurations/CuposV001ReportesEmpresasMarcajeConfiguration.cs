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
    public class CuposV001ReportesEmpresasMarcajeConfiguration : IEntityTypeConfiguration<CuposV001ReportesEmpresasMarcaje>
    {

        public void Configure(EntityTypeBuilder<CuposV001ReportesEmpresasMarcaje> entity)
        {
            entity.HasNoKey();

            entity.ToView("CUPOST_V001_REPORTESEMPRESASYCUPOS");

            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(50)
                .HasColumnName("NOMBREEMPRESA");

            entity.Property(e => e.NIT)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("NIT_EMPRESA");

            entity.Property(e => e.TipoEmpresa)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("CODIGO_TIPO_ENTIDAD");

            entity.Property(e => e.Estado)
             .HasColumnType("numeric(20, 0)")
             .HasColumnName("ESTADO");

            entity.Property(e => e.EstadoEmisionCITES)
             .HasColumnType("numeric(20, 0)")
             .HasColumnName("ESTADO_CITES");


            entity.Property(e => e.NumeroResolucion)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("NUMERO_RESOLUCION");



            entity.Property(e => e.FechaResolucion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_RESOLUCION");

            entity.Property(e => e.Especies)
               .HasMaxLength(200)               
               .HasColumnName("CODIGO_ESPECIE");


            entity.Property(e => e.Machos)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("POBLACION_MACHO");  
            
            entity.Property(e => e.Hembras)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("POBLACION_HEMBRA");
            
            entity.Property(e => e.PoblacionTotalParental)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("POBLACION_TOTAL");      
            
            entity.Property(e => e.AnioProduccion)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("ANIO_PRODUCCION");    
            
            entity.Property(e => e.CuposComercializacion)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("CUPOS_COMERCIALIZACION");

            entity.Property(e => e.CuotaRepoblacion)
               .HasMaxLength(50)
               .HasColumnName("CUOTA_REPOBLACION"); 
            
            entity.Property(e => e.CupoDisponible)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("CUPOS_DISPONIBLES"); 
            
            
            entity.Property(e => e.SoportesRepoblacion)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("CODIGO_SOPORTE"); 
            
            entity.Property(e => e.CupoUtilizado)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("CUPOS_UTILIZADOS"); 
            
            
            entity.Property(e => e.CuposAsignadosTotal)
               .HasColumnType("numeric(20, 0)")
               .HasColumnName("CUPOS_TOTAL");

            entity.Property(e => e.FechaRadicacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_RADICADO");
        }
    }
}
