using DataAccess.Abstract;
using DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnSupervisorsThesisDal : ISupervisorsThesisDal
{
    private readonly string _connectionString;

    public AnSupervisorsThesisDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "supervisors_theses";
    
    public SupervisorsThesis GetById(int id)
    {
        SupervisorsThesis supervisorThesis = null;

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = $"SELECT * FROM {_tableName} WHERE Id = @Id";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        supervisorThesis = new SupervisorsThesis
                        {
                            Id = (int)reader["Id"],
                            SupervisorId = (int)reader["SupervisorId"],
                            ThesisId = (int)reader["ThesisId"]
                        };
                    }
                }
            }
        }

        return supervisorThesis;
    }

    public IList<SupervisorsThesis> GetAll()
    {
        throw new NotImplementedException();
    }

    public SupervisorsThesis Add(SupervisorsThesis entity)
    {
        throw new NotImplementedException();
    }

    public SupervisorsThesis Update(SupervisorsThesis entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(SupervisorsThesis entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }
}