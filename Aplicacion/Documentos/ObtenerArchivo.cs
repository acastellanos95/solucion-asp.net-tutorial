using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ErrorHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Documentos
{
    public class ObtenerArchivo
    {
        public class ObtenerArchivoRequest : IRequest<ArchivoGenerico>
        {
            public Guid Id { get; set; }
        }

        public class ObtenerArchivoRequestHandler : IRequestHandler<ObtenerArchivoRequest, ArchivoGenerico>
        {
            private readonly CursosOnlineContext _context;
            public ObtenerArchivoRequestHandler(CursosOnlineContext context)
            {
                _context = context;
            }
            public async Task<ArchivoGenerico> Handle(ObtenerArchivoRequest request, CancellationToken cancellationToken)
            {
                var archivo = await _context.Documento.Where(x => x.ObjetoReferencia == request.Id).FirstAsync();
                if(archivo == null)
                {
                    throw new ExceptionHandling(HttpStatusCode.NotFound, new {mensaje = "No se encontró la imagen"});
                }
                var archivoGenerico = new ArchivoGenerico
                {
                    Data = Convert.ToBase64String(archivo.Contenido),
                    Nombre = archivo.Nombre,
                    Extension = archivo.Extension
                };
                return archivoGenerico;
            }
        }
    }
}