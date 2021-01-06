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
    public async Task<int> Delete(Guid id)
    {
      var storeProcedure = "usp_Eliminar_Instructor";
      try
      {
        var connection = _factoryConnection.GetConnection();
        var result = await connection.ExecuteAsync(storeProcedure, new
        {
          InstructorId = id
        }, commandType: CommandType.StoredProcedure);

        _factoryConnection.CloseConnection();

        return result;
      }
      catch (Exception e)
      {
        throw new Exception("Error en la eliminación de un Instructor", e);
      }
    }

    public async Task<InstructorModel> GetById(Guid id)
    {
      InstructorModel instructor = null;
      var storeProcedure = "usp_Obtener_por_Id_Instructor";
      try
      {
        var connection = _factoryConnection.GetConnection();
        instructor = await connection.QueryFirstOrDefaultAsync<InstructorModel>(storeProcedure, new {InstructorId = id}, commandType: CommandType.StoredProcedure);
      }
      catch (Exception e)
      {
        throw new Exception("Error en la consulta de datos", e);
      }
      finally
      {
        _factoryConnection.CloseConnection();
      }
      return instructor;
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

    public async Task<int> Update(Guid InstructorId, string Nombre, string Apellidos, string Grado)
    {
      var storeProcedure = "usp_Editar_Instructor";
      try
      {
        var connection = _factoryConnection.GetConnection();
        var result = await connection.ExecuteAsync(storeProcedure, new
        {
          InstructorId = InstructorId,
          Nombre = Nombre,
          Apellidos = Apellidos,
          Grado = Grado
        }, commandType: CommandType.StoredProcedure);

        _factoryConnection.CloseConnection();

        return result;
      }
      catch (Exception e)
      {
        throw new Exception("Error en la edición de un Instructor", e);
      }
    }
  }
}