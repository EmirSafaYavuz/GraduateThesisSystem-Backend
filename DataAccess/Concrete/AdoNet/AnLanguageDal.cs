using DataAccess.Abstract;
using DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnLanguageDal : ILanguageDal
{
    private readonly string _connectionString;

    public AnLanguageDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "languages";
    
    public Language GetById(int id)
    {
        Language language = null;

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
                        language = new Language
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString()
                        };
                    }
                }
            }
        }

        return language;
    }

    public IList<Language> GetAll()
    {
        throw new NotImplementedException();
    }

    public Language Add(Language entity)
    {
        throw new NotImplementedException();
    }

    public Language Update(Language entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Language entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }
}