using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConnection.Instructor
{
    public interface IInstructor
    {
        Task<List<InstructorModel>> GetInstructorList();
        Task<InstructorModel> GetById(Guid id);
        Task<int> New(string Nombre, string Apellidos, string Grado);
        Task<int> Update(InstructorModel instructor);
        Task<int> Delete(Guid id);
    }
}