using DataAccess.Abstract;
using DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnSubjectTopicDal : ISubjectTopicDal
{
    private readonly string _connectionString;

    public AnSubjectTopicDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "subject_topics";
    
    public SubjectTopic GetById(int id)
    {
        SubjectTopic subjectTopic = null;

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
                        subjectTopic = new SubjectTopic
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString()
                        };
                    }
                }
            }
        }

        return subjectTopic;
    }

    public IList<SubjectTopic> GetAll()
    {
        throw new NotImplementedException();
    }

    public SubjectTopic Add(SubjectTopic entity)
    {
        throw new NotImplementedException();
    }

    public SubjectTopic Update(SubjectTopic entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(SubjectTopic entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }
}