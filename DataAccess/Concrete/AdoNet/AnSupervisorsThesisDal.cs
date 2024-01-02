using DataAccess.Abstract;
using DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SupervisorsThesis = DataAccess.Entities.SupervisorsThesis;

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
        List<SupervisorsThesis> supervisorsTheses = new List<SupervisorsThesis>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = $"SELECT * FROM {_tableName}";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SupervisorsThesis supervisorsThesis = new SupervisorsThesis
                        {
                            Id = (int)reader["Id"],
                            SupervisorId = (int)reader["SupervisorId"],
                            ThesisId = (int)reader["ThesisId"]
                        };

                        supervisorsTheses.Add(supervisorsThesis);
                    }
                }
            }
        }

        return supervisorsTheses;
    }

    public SupervisorsThesis Add(SupervisorsThesis entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"INSERT INTO {_tableName} (Supervisor_Id, Thesis_Id) VALUES (@SupervisorId, @ThesisId) RETURNING Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@SupervisorId", entity.SupervisorId);
                command.Parameters.AddWithValue("@ThesisId", entity.ThesisId);

                // ExecuteScalar is used to get the newly inserted Id
                entity.Id = (int)command.ExecuteScalar();
            }
        }

        return entity;
    }

    public SupervisorsThesis Update(SupervisorsThesis entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"UPDATE {_tableName} SET Supervisor_Id = @SupervisorId, Thesis_Id = @ThesisId WHERE Id = @Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@SupervisorId", entity.SupervisorId);
                command.Parameters.AddWithValue("@ThesisId", entity.ThesisId);
                command.Parameters.AddWithValue("@Id", entity.Id);

                command.ExecuteNonQuery();
            }
        }

        return entity;
    }

    public void Delete(SupervisorsThesis entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"DELETE FROM {_tableName} WHERE Id = @Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Id", entity.Id);
                command.ExecuteNonQuery();
            }
        }
    }

    public long GetCount()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = $"SELECT COUNT(*) FROM {_tableName}";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt64(result) : 0;
            }
        }
    }
}