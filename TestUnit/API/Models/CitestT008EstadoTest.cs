using Repository.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestUnit.API.Models
{
    public class CitestT008EstadoTest
    {
        [Fact]
        public void CitestT008Estado()
        {
            CitestT008Estado datos = new CitestT008Estado();
            datos.PkT008codigo = 1;
            datos.A008codigoUsuarioCreacion = 1;
            datos.A008codigoUsuarioModificacion = 1;
            datos.A008posicion = 1;
            datos.A008codigoParametricaEstado = 1;
            datos.A008estadoRegistro = 1;
            datos.A008descripcion = "Descripción";
            datos.A008fechaCreacion = DateTime.Now;
            datos.A008fechaModificacion = DateTime.Now;
            datos.A008etapa = "Etapa";
            datos.A008modulo = "Módulo";

            var type = Assert.IsType<CitestT008Estado>(datos);
            Assert.NotNull(type);
        }
    }
}