using Repository.Models;
using Xunit;

namespace TestUnit.API.Models
{
    public class CupostT021CertificadoFloraNoMaderableTest
    {
        [Fact]
        public void CupostT021CertificadoFloraNoMaderable()
        {
            CupostT021CertificadoFloraNoMaderable datos = new CupostT021CertificadoFloraNoMaderable();
            datos.Pk_T021Codigo = 1;
            datos.A021FechaCertificacion = DateTime.Now;
            datos.A021VigenciaCertificacion = DateTime.Now.AddYears(1);
            datos.A021FechaRegistroCertificado = DateTime.Now;
            datos.A021TipoCertificado = 1;
            datos.A021AutoridadEmiteCertificado = "Autoridad emisora del certificado";
            datos.A021NumeroCertificado = "123456789";
            datos.A021TipoPermiso = "Tipo de permiso";
            datos.A021CodigoEmpresa = 1;
            datos.A021TipoEspecimenProductoImpExp = "Tipo de espécimen o producto a importar o exportar";
            datos.A021Observaciones = "Observaciones";
            datos.A021FechaCreacion = DateTime.Now;
            datos.A021FechaModificacion = null;
            datos.A021UsuarioCreacion = 1;
            datos.A021UsuarioModificacion = null;
            datos.A021EstadoRegistro = 1;

            var type = Assert.IsType<CupostT021CertificadoFloraNoMaderable>(datos);
            Assert.NotNull(type);
        }
    }
}
