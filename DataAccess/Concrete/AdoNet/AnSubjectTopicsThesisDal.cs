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
        List<SubjectTopicsThesis> subjectTopicsTheses = new List<SubjectTopicsThesis>();

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
                        SubjectTopicsThesis subjectTopicsThesis = new SubjectTopicsThesis
                        {
                            Id = (int)reader["Id"],
                            SubjectTopicId = (int)reader["SubjectTopicId"],
                            ThesisId = (int)reader["ThesisId"]
                        };

                        subjectTopicsTheses.Add(subjectTopicsThesis);
                    }
                }
            }
        }

        return subjectTopicsTheses;
    }

    public SubjectTopicsThesis Add(SubjectTopicsThesis entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"INSERT INTO {_tableName} (Subject_Topic_Id, Thesis_Id) VALUES (@SubjectTopicId, @ThesisId) RETURNING Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@SubjectTopicId", entity.SubjectTopicId);
                command.Parameters.AddWithValue("@ThesisId", entity.ThesisId);

                // ExecuteScalar is used to get the newly inserted Id
                entity.Id = (int)command.ExecuteScalar();
            }
        }

        return entity;
    }

    public SubjectTopicsThesis Update(SubjectTopicsThesis entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var commandText = $"UPDATE {_tableName} SET Subject_Topic_Id = @SubjectTopicId, Thesis_Id = @ThesisId WHERE Id = @Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@SubjectTopicId", entity.SubjectTopicId);
                command.Parameters.AddWithValue("@ThesisId", entity.ThesisId);
                command.Parameters.AddWithValue("@Id", entity.Id);

                command.ExecuteNonQuery();
            }
        }

        return entity;
    }

    public void Delete(SubjectTopicsThesis entity)
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

    public IEnumerable<SubjectTopic> GetSubjectTopicsByThesisId(int id)
    {
        List<SubjectTopic> subjectTopics = new List<SubjectTopic>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = $"SELECT * FROM subject_topics WHERE Id IN (SELECT Subject_Topic_Id FROM {_tableName} WHERE Thesis_Id = @Id)";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SubjectTopic subjectTopic = new SubjectTopic
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"]
                        };

                        subjectTopics.Add(subjectTopic);
                    }
                }
            }
        }

        return subjectTopics;
    }
}