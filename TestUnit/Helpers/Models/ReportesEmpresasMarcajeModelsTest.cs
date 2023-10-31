using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.ReportesEmpresasMarcajeModels;

namespace TestUnit.Helpers.Models
{
	public class ReportesEmpresasMarcajeModelsTest
	{
		[Fact]
		public void DatosEmpresasModel()
		{
			DatosEmpresasModel datos = new DatosEmpresasModel();
			datos.TipoEmpresa = "1";
			datos.NombreEmpresa = "1";
			datos.NIT = 1;
			datos.Estado = "1";
			datos.EstadoEmisionCITES = "1";
			datos.NumeroResolucion = "1";
			datos.FechaEmisionResolucion = "1";
			datos.Especies = "1";
			datos.Machos = 1;
			datos.Hembras = 1;
			datos.PoblacionTotalParental = 1;
			datos.AnioProduccion = 1;
			datos.CuposComercializacion = 1;
			datos.CuotaRepoblacion = "1";
			datos.CuposAsignadosTotal = 1;
			datos.SoportesRepoblacion = true;
			datos.CupoUtilizado = 1;
			datos.CupoDisponible = 1;

			var type = Assert.IsType<DatosEmpresasModel>(datos);
			Assert.NotNull(type);
		}
	}
}
