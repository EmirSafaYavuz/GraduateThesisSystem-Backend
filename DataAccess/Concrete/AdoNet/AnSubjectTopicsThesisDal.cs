using DataAccess.Abstract;
using DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnSubjectTopicsThesisDal : ISubjectTopicsThesisDal
{
    private readonly string _connectionString;

    public AnSubjectTopicsThesisDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "subject_topics_theses";
    
    public SubjectTopicsThesis GetById(int id)
    {
        SubjectTopicsThesis subjectTopicsThesis = null;

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
                        subjectTopicsThesis = new SubjectTopicsThesis
                        {
                            Id = (int)reader["Id"],
                            SubjectTopicId = (int)reader["SubjectTopicId"],
                            ThesisId = (int)reader["ThesisId"]
                        };
                    }
                }
            }
        }

        return subjectTopicsThesis;
    }

    public IList<SubjectTopicsThesis> GetAll()
    {
        throw new NotImplementedException();
    }

    public SubjectTopicsThesis Add(SubjectTopicsThesis entity)
    {
        throw new NotImplementedException();
    }

    public SubjectTopicsThesis Update(SubjectTopicsThesis entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(SubjectTopicsThesis entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }
}