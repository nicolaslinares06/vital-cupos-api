using Repository.Helpers;

namespace API.Helpers
{
    public static class ResponseManager
    {
        public static Responses generaRespuestaGenerica(string mensaje, object data, string token, bool error)
        {
            Responses respuesta = new Responses();

            if (error)
            {
                respuesta.Message = mensaje;
                respuesta.Error = true;
                if(token != "") { respuesta.Token = token; }
                return respuesta;
            }

            respuesta.Response = data;
            respuesta.Error = false;
            respuesta.Message = mensaje;
            respuesta.Token = token;

            return respuesta;
        }
    }
}
