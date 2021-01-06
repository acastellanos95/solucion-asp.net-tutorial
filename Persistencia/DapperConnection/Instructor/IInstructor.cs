using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConnection.Instructor
{
    public interface IInstructor
    {
        Task<List<InstructorModel>> GetInstructorList();
        Task<InstructorModel> GetById(Guid id);
        Task<int> New(InstructorModel instructor);
        Task<int> Update(InstructorModel instructor);
        Task<int> Delete(Guid id);
    }
}