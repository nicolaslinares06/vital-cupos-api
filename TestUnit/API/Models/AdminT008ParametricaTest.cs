using Repository.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestUnit.API.Models
{
    public class AdminT008ParametricaTest
    {
        [Fact]
        public void AdminT008Parametrica()
        {
            AdmintT008Parametrica datos = new AdmintT008Parametrica();
            datos.PkT008codigo = 1;
            datos.A008codigoUsuarioCreacion = 1;
            datos.A008codigoUsuarioModificacion = 1;
            datos.A008estadoRegistro = 1;
            datos.A008fechaCreacion = DateTime.Now;
            datos.A008fechaModificacion = DateTime.Now;
            datos.A008modulo = "Módulo";
            datos.A008parametrica = "Paramétrica";
            datos.A008valor = "Valor";
            datos.A008descripcion = "Descripción";

            var type = Assert.IsType<AdmintT008Parametrica>(datos);
            Assert.NotNull(type);
        }
    }
}