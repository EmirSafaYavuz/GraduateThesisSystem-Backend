using System.Linq.Expressions;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnUniversityDal : IUniversityDal
{
    private readonly string _connectionString;

    public AnUniversityDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "universities";

    public University GetById(int id)
    {
        University university = null;

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
                        university = new University
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            LocationId = (int)reader["LocationId"]
                        };
                    }
                }
            }
        }

        return university;
    }

    public IList<University> GetAll()
    {
        List<University> universities = new List<University>();

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
                        University university = new University
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            LocationId = (int)reader["LocationId"]
                        };

                        universities.Add(university);
                    }
                }
            }
        }

        return universities;
    }

    public University Add(University entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"INSERT INTO {_tableName} (Name, Location_Id) VALUES (@Name, @LocationId) RETURNING Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@LocationId", entity.LocationId);

                // ExecuteScalar is used to get the newly inserted Id
                entity.Id = (int)command.ExecuteScalar();
            }
        }

        return entity;
    }

    public University Update(University entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"UPDATE {_tableName} SET Name = @Name, Location_Id = @LocationId WHERE Id = @Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@LocationId", entity.LocationId);
                command.Parameters.AddWithValue("@Id", entity.Id);

                command.ExecuteNonQuery();
            }
        }

        return entity;
    }

    public void Delete(University entity)
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

    public IEnumerable<UniversityDetailDto> GetAllDetailDto()
    {
        List<UniversityDetailDto> result = new List<UniversityDetailDto>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = $"SELECT u.Id, u.Name, u.Location_Id, l.City, l.Country, i.Name AS InstituteName " +
                                 "FROM universities u " +
                                 "INNER JOIN locations l ON u.location_id = l.id " +
                                 "LEFT JOIN institutes i ON u.id = i.university_id";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int universityId = (int)reader["Id"];

                        // Check if the result already contains the university
                        UniversityDetailDto universityDetail = result.FirstOrDefault(u => u.Id == universityId);

                        if (universityDetail == null)
                        {
                            // University not found, create a new one
                            universityDetail = new UniversityDetailDto
                            {
                                Id = universityId,
                                Name = (string)reader["Name"],
                                LocationId = (int)reader["Location_Id"],
                                City = (string)reader["City"],
                                Country = (string)reader["Country"],
                                Institutes = new List<Institute>()
                            };

                            result.Add(universityDetail);
                        }

                        // Check if the InstituteName is not null (there might be cases where there is no related institute)
                        if (reader["InstituteName"] != DBNull.Value)
                        {
                            // Add the Institute to the university's Institutes collection
                            ((List<Institute>)universityDetail.Institutes).Add(new Institute
                            {
                                Name = (string)reader["InstituteName"]
                            });
                        }
                    }
                }
            }
        }

        return result;
    }


    public UniversityDetailDto GetDetailDto()
    {
        throw new NotImplementedException();
    }
}