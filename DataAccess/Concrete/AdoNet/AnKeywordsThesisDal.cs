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
        throw new NotImplementedException();
    }

    public KeywordsThesis Add(KeywordsThesis entity)
    {
        throw new NotImplementedException();
    }

    public KeywordsThesis Update(KeywordsThesis entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(KeywordsThesis entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }
}