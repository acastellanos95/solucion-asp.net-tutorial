using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListaCursos : IRequest<List<CursoDto>>
        {
            
        }

        public class ListaCursosHandler : IRequestHandler<ListaCursos, List<CursoDto>>
        {
            public readonly CursosOnlineContext _context;
            public ListaCursosHandler(CursosOnlineContext context)
            {
                _context = context;
            }
            public async Task<List<CursoDto>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                var cursos = await _context.Curso
                                .Include(x => x.InstructoresLista)
                                .ThenInclude(x => x.Instructor)
                                .ToListAsync();
                
                return cursos;
            }
        }
    }
}