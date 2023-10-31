using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CuposV001ResolucionCuposTest
	{
		[Fact]
		public void CuposV001ResolucionCupos()
		{
			CuposV001ResolucionCupos datos = new CuposV001ResolucionCupos();
			datos.codigoCupo = 1;
			datos.autoridadEmiteResolucion = "1";
			datos.codigoZoocriadero = "1";
			datos.numeroResolucion = 1;
			datos.fechaResolucion = DateTime.Now;
			datos.fechaRegistroResolucion = DateTime.Now;
			datos.fechaRadicado = DateTime.Now;
			datos.cuposOtorgados = 1;
			datos.cuposPorAnio = 1;
			datos.fechaProduccion = DateTime.Now;
			datos.cuposAprovechamientoComercializacion = "1";
			datos.cuposTotal = 1;
			datos.cuotaRepoblacion = "1";
			datos.cuposDisponibles = 1;
			datos.observaciones = "1";
			datos.codigoEmpresa = 1;
			datos.codigoEspecie = "1";
			datos.numeroInternoFinalCuotaRepoblacion = 1;
			datos.numeroInternoFinal = 1;
			datos.NombreEspecieExportar = "1";
			datos.NumeroInternoInicial = 1;
			datos.numeroInternoInicialCuotaRepoblacion = 1;

			var type = Assert.IsType<CuposV001ResolucionCupos>(datos);
			Assert.NotNull(type);
		}
	}
}