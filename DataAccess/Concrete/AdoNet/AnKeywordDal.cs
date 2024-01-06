using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnKeywordDal : IKeywordDal
{
    private readonly string _connectionString;

    public AnKeywordDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "keywords";
    
    public Keyword GetById(int id)
    {
        Keyword keyword = null;

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
                        keyword = new Keyword
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString()
                        };
                    }
                }
            }
        }

        return keyword;
    }

    public IList<Keyword> GetAll()
    {
        List<Keyword> keywords = new List<Keyword>();

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
                        Keyword author = new Keyword
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                        };

                        keywords.Add(author);
                    }
                }
            }
        }

        return keywords;
    }

    public Keyword Add(Keyword entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"INSERT INTO {_tableName} (Name) VALUES (@Name) RETURNING Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Name", entity.Name);

                entity.Id = (int)command.ExecuteScalar();
            }
        }

        return entity;
    }

    public Keyword Update(Keyword entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"UPDATE {_tableName} SET Name = @Name, WHERE Id = @Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Id", entity.Id);

                command.ExecuteNonQuery();
            }
        }

        return entity;
    }

    public void Delete(Keyword entity)
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

    public IEnumerable<ThesisLookupDto> GetThesesByKeywordId(int id)
    {
        List<ThesisLookupDto> theses = new List<ThesisLookupDto>();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = @"SELECT t.Id, t.ThesisNo, t.Title, a.Id, a.Name, tt.Name
                                   FROM theses t
                                   INNER JOIN authors a ON t.AuthorId = a.Id
                                   INNER JOIN thesis_types tt ON t.ThesisTypeId = tt.Id
                                   INNER JOIN thesis_keywords tk ON t.Id = tk.ThesisId
                                   INNER JOIN keywords k ON tk.KeywordId = k.Id
                                   WHERE k.Id = @Id";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThesisLookupDto thesis = new ThesisLookupDto
                        {
                            Id = (int)reader[0],
                            ThesisNo = (int)reader[1],
                            Title = (string)reader[2],
                            AuthorId = (int)reader[3],
                            AuthorName = (string)reader[4],
                            ThesisType = (string)reader[5]
                        };

                        theses.Add(thesis);
                    }
                }
            }
        }
        
        return theses;
    }
}