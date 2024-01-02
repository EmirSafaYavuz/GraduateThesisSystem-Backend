using DataAccess.Abstract;
using DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnKeywordsThesisDal : IKeywordsThesisDal
{
    private readonly string _connectionString;

    public AnKeywordsThesisDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "keywords_theses";
    
    public KeywordsThesis GetById(int id)
    {
        KeywordsThesis keywordsThesis = null;

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
                        keywordsThesis = new KeywordsThesis
                        {
                            Id = (int)reader["Id"],
                            KeywordId = (int)reader["KeywordId"],
                            ThesisId = (int)reader["ThesisId"]
                        };
                    }
                }
            }
        }

        return keywordsThesis;
    }

    public IList<KeywordsThesis> GetAll()
    {
        List<KeywordsThesis> keywordsTheses = new List<KeywordsThesis>();

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
                        KeywordsThesis author = new KeywordsThesis
                        {
                            Id = (int)reader["Id"],
                            KeywordId = (int)reader["KeywordId"],
                            ThesisId = (int)reader["ThesisId"]
                        };

                        keywordsTheses.Add(author);
                    }
                }
            }
        }

        return keywordsTheses;
    }

    public KeywordsThesis Add(KeywordsThesis entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"INSERT INTO {_tableName} (Name, Keyword_Id, Thesis_Id) VALUES (@Name, @KeywordId, @ThesisId) RETURNING Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@KeywordId", entity.KeywordId);
                command.Parameters.AddWithValue("@ThesisId", entity.ThesisId);

                // ExecuteScalar is used to get the newly inserted Id
                entity.Id = (int)command.ExecuteScalar();
            }
        }

        return entity;
    }

    public KeywordsThesis Update(KeywordsThesis entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"UPDATE {_tableName} SET Keyword_Id = @KeywordId, ThesisId = @ThesisId WHERE Id = @Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@KeywordId", entity.KeywordId);
                command.Parameters.AddWithValue("@ThesisId", entity.ThesisId);
                command.Parameters.AddWithValue("@Id", entity.Id);

                command.ExecuteNonQuery();
            }
        }

        return entity;
    }

    public void Delete(KeywordsThesis entity)
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