using System;
using System.Net;

namespace Aplicacion.ErrorHandling
{
    public class ExceptionHandling : Exception
    {
        public HttpStatusCode Codigo { get; }
        public object Errores { get; }
        public ExceptionHandling(HttpStatusCode codigo, object errores = null)
        {
            Codigo = codigo;
            Errores = errores;
        }
    }
}