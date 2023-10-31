using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CuposV001ReportesEmpresasMarcajeTest
	{
		[Fact]
		public void CuposV001ReportesEmpresasMarcaje()
		{
			CuposV001ReportesEmpresasMarcaje datos = new CuposV001ReportesEmpresasMarcaje();
			datos.NombreEmpresa = "1";
			datos.NIT = 1;
			datos.TipoEmpresa= 1;
			datos.Estado = 1;
			datos.EstadoEmisionCITES = 1;
			datos.NumeroResolucion = 1;
			datos.FechaResolucion = DateTime.Now;
			datos.Especies = "1";
			datos.Machos = 1;
			datos.Hembras = 1;
			datos.PoblacionTotalParental = 1;
			datos.AnioProduccion = 1;
			datos.CuposComercializacion = 1;
			datos.CuotaRepoblacion = "1";
			datos.CuposAsignadosTotal = 1;
			datos.SoportesRepoblacion = 1;
			datos.CupoUtilizado = 1;
			datos.CupoDisponible = 1;

			var type = Assert.IsType<CuposV001ReportesEmpresasMarcaje>(datos);
			Assert.NotNull(type);
		}
	}
}
