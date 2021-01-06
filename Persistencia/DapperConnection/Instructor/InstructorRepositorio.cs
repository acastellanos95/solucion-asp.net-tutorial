using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Persistencia.DapperConnection.Instructor
{
    public class InstructorRepositorio : IInstructor
    {
        private readonly IFactoryConnection _factoryConnection;
        public InstructorRepositorio(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }
        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<InstructorModel> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InstructorModel>> GetInstructorList()
        {
            List<InstructorModel> instructorList = null;
            var storeProcedure = "usp_Obtener_Instructores";
            try
            {
                var connection = _factoryConnection.GetConnection();
                instructorList = (List<InstructorModel>)await connection.QueryAsync<InstructorModel>(storeProcedure, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw new Exception("Error en la consulta de datos", e);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return instructorList;
        }

        public async Task<int> New(string Nombre, string Apellidos, string Grado)
        {
            var storeProcedure = "usp_Nuevo_Instructor";
            try
            {
                var connection = _factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(storeProcedure, new
                {
                    InstructorId = Guid.NewGuid(),
                    Nombre = Nombre,
                    Apellidos = Apellidos,
                    Grado = Grado
                }, commandType: CommandType.StoredProcedure);

                _factoryConnection.CloseConnection();

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error en la creación de un Instructor", e);
            }
        }

        public Task<int> Update(InstructorModel instructor)
        {
            throw new NotImplementedException();
        }
    }
}