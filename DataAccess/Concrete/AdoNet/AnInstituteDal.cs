using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnInstituteDal : IInstituteDal
{
    private readonly string _connectionString;

    public AnInstituteDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "institutes";
    
    public Institute GetById(int id)
    {
        Institute institute = null;

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
                        institute = new Institute
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            UniversityId = (int)reader["UniversityId"]
                        };
                    }
                }
            }
        }

        return institute;
    }

    public IList<Institute> GetAll()
    {
        List<Institute> institutes = new List<Institute>();

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
                        Institute author = new Institute
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            UniversityId = (int)reader["UniversityId"]
                        };

                        institutes.Add(author);
                    }
                }
            }
        }

        return institutes;
    }

    public Institute Add(Institute entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"INSERT INTO {_tableName} (Name, University_Id) VALUES (@Name, @UniversityId) RETURNING Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@UniversityId", entity.UniversityId);

                entity.Id = (int)command.ExecuteScalar();
            }
        }

        return entity;
    }

    public Institute Update(Institute entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"UPDATE {_tableName} SET Name = @Name, University_Id = @UniversityId WHERE Id = @Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@UniversityId", entity.UniversityId);
                command.Parameters.AddWithValue("@Id", entity.Id);

                command.ExecuteNonQuery();
            }
        }

        return entity;
    }

    public void Delete(Institute entity)
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

    public IEnumerable<InstituteDetailDto> GetAllDetailDto()
    {
        List<InstituteDetailDto> institutes = new List<InstituteDetailDto>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = $@"
            SELECT i.id AS institute_id,
                   i.name AS institute_name,
                   i.university_id AS university_id,
                   u.name AS university_name
            FROM institutes i
            JOIN universities u ON i.university_id = u.id";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InstituteDetailDto institute = new InstituteDetailDto
                        {
                            Id = (int)reader["institute_id"],
                            Name = (string)reader["institute_name"],
                            UniversityId = (int)reader["university_id"],
                            UniversityName = (string)reader["university_name"]
                        };

                        institutes.Add(institute);
                    }
                }
            }
        }

        return institutes;
    }

    public IEnumerable<InstituteDetailDto> GetAllDetailDtoByUniversityId(int universityId)
    {
        List<InstituteDetailDto> institutes = new List<InstituteDetailDto>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = $@"
            SELECT i.id AS institute_id,
                   i.name AS institute_name,
                   i.university_id AS university_id,
                   u.name AS university_name
            FROM institutes i
            JOIN universities u ON i.university_id = u.id
            WHERE i.university_id = @UniversityId";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@UniversityId", universityId);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InstituteDetailDto institute = new InstituteDetailDto
                        {
                            Id = (int)reader["institute_id"],
                            Name = (string)reader["institute_name"],
                            UniversityId = (int)reader["university_id"],
                            UniversityName = (string)reader["university_name"]
                        };

                        institutes.Add(institute);
                    }
                }
            }
        }

        return institutes;
    }


    public InstituteDetailDto GetDetailDtoById(int id)
    {
        InstituteDetailDto institute = null;
    
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = $@"
            SELECT i.id AS institute_id,
                   i.name AS institute_name,
                   i.university_id AS university_id,
                   u.name AS university_name
            FROM institutes i
            JOIN universities u ON i.university_id = u.id
            WHERE i.id = @Id";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        institute = new InstituteDetailDto
                        {
                            Id = (int)reader["institute_id"],
                            Name = (string)reader["institute_name"],
                            UniversityId = (int)reader["university_id"],
                            UniversityName = (string)reader["university_name"]
                        };
                    }
                }
            }
        }

        return institute;
    }


}