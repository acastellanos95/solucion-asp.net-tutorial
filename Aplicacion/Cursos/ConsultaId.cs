using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ErrorHandling;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDto>
        {
            public Guid Id { get; set; }
        }

    public class CursoUnicoHandler : IRequestHandler<CursoUnico, CursoDto>
    {
        private readonly CursosOnlineContext _context;
        private readonly IMapper _mapper;
        public CursoUnicoHandler(CursosOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CursoDto> Handle(CursoUnico request, CancellationToken cancellationToken)
        {
            var curso = await _context.Curso
            .Include(x => x.Precio)
            .Include(x => x.ComentarioLista)
            .Include(x => x.InstructoresLista)
            .Include(x => x.InstructoresLista).ThenInclude(y => y.Instructor).FirstOrDefaultAsync(a => a.CursoId == request.Id) ?? throw new ExceptionHandling(HttpStatusCode.NotFound, new { message = "No se encontró el curso"});
            var cursoDto = _mapper.Map<Curso, CursoDto>(curso);
            return cursoDto;
        }
    }
  }
}