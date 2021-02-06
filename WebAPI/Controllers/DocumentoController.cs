using System;
using System.Threading.Tasks;
using Aplicacion.Documentos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class DocumentoController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> GuardarArchivo(SubirArchivo.SubirArchivoRequest request)
        {
            return await Mediator.Send(request);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ArchivoGenerico>> ObtenerArchivo(Guid id)
        {
            return await Mediator.Send(new ObtenerArchivo.ObtenerArchivoRequest {Id = id});
        }
    }
}