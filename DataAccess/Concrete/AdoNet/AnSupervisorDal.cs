using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnSupervisorDal : ISupervisorDal
{
    private readonly string _connectionString;

    public AnSupervisorDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "supervisors";
    
    public Supervisor GetById(int id)
    {
        Supervisor supervisor = null;

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
                        supervisor = new Supervisor
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            PhoneNumber = (string)reader["Phone_Number"],
                        };
                    }
                }
            }
        }

        return supervisor;
    }

    public IList<Supervisor> GetAll()
    {
        List<Supervisor> supervisors = new List<Supervisor>();

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
                        Supervisor supervisor = new Supervisor
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            PhoneNumber = (string)reader["Phone_Number"],
                        };

                        supervisors.Add(supervisor);
                    }
                }
            }
        }

        return supervisors;
    }

    public Supervisor Add(Supervisor entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"INSERT INTO {_tableName} (Name, Phone_Number) VALUES (@Name, @PhoneNumber) RETURNING Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);

                // ExecuteScalar is used to get the newly inserted Id
                entity.Id = (int)command.ExecuteScalar();
            }
        }

        return entity;
    }

    public Supervisor Update(Supervisor entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"UPDATE {_tableName} SET Name = @Name, Phone_Number = @PhoneNumber WHERE Id = @Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
                command.Parameters.AddWithValue("@Id", entity.Id);

                command.ExecuteNonQuery();
            }
        }

        return entity;
    }

    public void Delete(Supervisor entity)
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

    public IEnumerable<ThesisLookupDto> GetThesesBySupervisorId(int id)
    {
        
        /*
         * public int Id { get; set; }

           public int ThesisNo { get; set; }

           public string Title { get; set; } = null!;

           public int AuthorId { get; set; }
           public string AuthorName { get; set; } = null!;

           public string ThesisType { get; set; }
         */
        
        List<ThesisLookupDto> theses = new List<ThesisLookupDto>();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = @"SELECT t.Id, t.ThesisNo, t.Title, a.Id, a.Name, tt.Name
                                   FROM theses t
                                   INNER JOIN authors a ON t.Author_Id = a.Id
                                   INNER JOIN thesis_types tt ON t.Thesis_Type_Id = tt.Id
                                   INNER JOIN thesis_supervisors ts ON t.Id = ts.Thesis_Id
                                   INNER JOIN supervisors s ON ts.Supervisor_Id = s.Id
                                   WHERE s.Id = @Id";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThesisLookupDto thesis = new ThesisLookupDto
                        {
                            Id = (int)reader["Id"],
                            ThesisNo = (int)reader["ThesisNo"],
                            Title = (string)reader["Title"],
                            AuthorId = (int)reader["Id"],
                            AuthorName = (string)reader["Name"],
                            ThesisType = (string)reader["ThesisType"],
                        };

                        theses.Add(thesis);
                    }
                }
            }
        }
        
        return theses;
    }
}