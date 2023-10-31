using Repository.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestUnit.API.Models
{
    public class CupostT001EmpresaTest
    {
        [Fact]
        public void CupostT001Empresa()
        {
            CupostT001Empresa datos = new CupostT001Empresa();
            datos.PkT001codigo = 1;
            datos.A001codigoUsuarioCreacion = 1;
            datos.A001codigoUsuarioModificacion = 1;
            datos.A001codigoParametricaTipoEntidad = 1;
            datos.A001codigoPersonaRepresentantelegal = 1;
            datos.A001codigoCiudad = 1;
            datos.A001estadoRegistro = 1;
            datos.A001fechaCreacion = DateTime.Now;
            datos.A001fechaModificacion = DateTime.Now;
            datos.A001nit = 123456789;
            datos.A001telefono = 987654321;
            datos.A001nombre = "Nombre de la empresa";
            datos.A001correo = "correo@empresa.com";
            datos.A001direccion = "Dirección de la empresa";
            datos.A001matriculaMercantil = "123456789";
            datos.A001numeroInternoInicial = 1;
            datos.A001numeroInternoFinal = 100;

            var type = Assert.IsType<CupostT001Empresa>(datos);
            Assert.NotNull(type);
        }
    }
}
