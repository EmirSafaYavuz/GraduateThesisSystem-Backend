using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using DataAccess.Entities.Enums;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnSearchDal : ISearchDal
{
    private readonly string _connectionString;

    public AnSearchDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<ThesisLookupDto> SearchThesisTitle(string query, ThesisType? thesisType)
    {
        List<ThesisLookupDto> result = new List<ThesisLookupDto>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            
            string commandText =
                "SELECT t.id, t.thesis_no, t.title, t.author_id, a.name AS author_name, t.thesis_type " +
                "FROM theses t " +
                "LEFT JOIN authors a ON t.author_id = a.id " +
                "WHERE LOWER(t.title) LIKE LOWER(@Query)";
            
            if (thesisType.HasValue)
            {
                commandText += " AND t.thesis_type::text = @ThesisType";
            }
            
            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Query", $"%{query}%");
                
                if (thesisType.HasValue)
                {
                    command.Parameters.AddWithValue("@ThesisType", thesisType.Value.GetThesisTypeAsString());
                }

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThesisLookupDto thesisLookupDto = new ThesisLookupDto
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            ThesisNo = reader["thesis_no"] != DBNull.Value ? (int)reader["thesis_no"] : 0,
                            Title = reader["title"] != DBNull.Value
                                ? (string)reader["title"]
                                : string.Empty,
                            AuthorId = reader["author_id"] != DBNull.Value ? (int)reader["author_id"] : 0,
                            AuthorName = reader["author_name"] != DBNull.Value
                                ? (string)reader["author_name"]
                                : string.Empty,
                            ThesisType = reader["thesis_type"] != DBNull.Value
                                ? (string)reader["thesis_type"]
                                : string.Empty
                        };

                        result.Add(thesisLookupDto);
                    }
                }
            }
        }
        
        return result;
    }
    
    public IEnumerable<ThesisLookupDto> SearchThesisAbstract(string query, ThesisType? thesisType)
    {
        List<ThesisLookupDto> result = new List<ThesisLookupDto>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            
            string commandText =
                "SELECT t.id, t.thesis_no, t.title, t.author_id, a.name AS author_name, t.thesis_type " +
                "FROM theses t " +
                "LEFT JOIN authors a ON t.author_id = a.id " +
                "WHERE LOWER(t.abstract) LIKE LOWER(@Query)";
            
            if (thesisType.HasValue)
            {
                commandText += " AND t.thesis_type::text = @ThesisType";
            }
            
            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Query", $"%{query}%");
                
                if (thesisType.HasValue)
                {
                    command.Parameters.AddWithValue("@ThesisType", thesisType.Value.GetThesisTypeAsString());
                }

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThesisLookupDto thesisLookupDto = new ThesisLookupDto
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            ThesisNo = reader["thesis_no"] != DBNull.Value ? (int)reader["thesis_no"] : 0,
                            Title = reader["title"] != DBNull.Value
                                ? (string)reader["title"]
                                : string.Empty,
                            AuthorId = reader["author_id"] != DBNull.Value ? (int)reader["author_id"] : 0,
                            AuthorName = reader["author_name"] != DBNull.Value
                                ? (string)reader["author_name"]
                                : string.Empty,
                            ThesisType = reader["thesis_type"] != DBNull.Value
                                ? (string)reader["thesis_type"]
                                : string.Empty
                        };

                        result.Add(thesisLookupDto);
                    }
                }
            }
        }
        
        return result;
    }

    public IEnumerable<ThesisLookupDto> SearchThesisNo(string query, ThesisType? thesisType)
    {
        List<ThesisLookupDto> result = new List<ThesisLookupDto>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            
            string commandText =
                "SELECT t.id, t.thesis_no, t.title, t.author_id, a.name AS author_name, t.thesis_type " +
                "FROM theses t " +
                "LEFT JOIN authors a ON t.author_id = a.id " +
                "WHERE t.thesis_no::text LIKE @Query";
            
            if (thesisType.HasValue)
            {
                commandText += " AND t.thesis_type::text = @ThesisType";
            }
            
            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                // Add parameters to the query
                command.Parameters.AddWithValue("@Query", $"%{query}%");
                
                if (thesisType.HasValue)
                {
                    command.Parameters.AddWithValue("@ThesisType", thesisType.Value.GetThesisTypeAsString());
                }

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThesisLookupDto thesisLookupDto = new ThesisLookupDto
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            ThesisNo = reader["thesis_no"] != DBNull.Value ? (int)reader["thesis_no"] : 0,
                            Title = reader["title"] != DBNull.Value
                                ? (string)reader["title"]
                                : string.Empty,
                            AuthorId = reader["author_id"] != DBNull.Value ? (int)reader["author_id"] : 0,
                            AuthorName = reader["author_name"] != DBNull.Value
                                ? (string)reader["author_name"]
                                : string.Empty,
                            ThesisType = reader["thesis_type"] != DBNull.Value
                                ? (string)reader["thesis_type"]
                                : string.Empty
                        };

                        result.Add(thesisLookupDto);
                    }
                }
            }
        }
        
        return result;
    }

    public IEnumerable<Author> SearchAuthor(string query)
    {
        List<Author> result = new List<Author>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText =
                "SELECT a.id, a.name, a.email " +
                "FROM authors a " +
                "WHERE LOWER(a.name) LIKE LOWER(@Query)";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Query", $"%{query}%");

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Author author = new Author
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            Name = reader["name"] != DBNull.Value ? (string)reader["name"] : string.Empty,
                            Email = reader["email"] != DBNull.Value ? (string)reader["email"] : string.Empty,
                        };

                        result.Add(author);
                    }
                }
            }
        }
        
        return result;
    }

    public IEnumerable<Institute> SearchInstitute(string query)
    {
        List<Institute> result = new List<Institute>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText =
                "SELECT i.id, i.name, i.university_id " +
                "FROM institutes i " +
                "WHERE LOWER(i.name) LIKE LOWER(@Query)";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Query", $"%{query}%");

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Institute institute = new Institute
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            Name = reader["name"] != DBNull.Value ? (string)reader["name"] : string.Empty,
                            UniversityId = reader["university_id"] != DBNull.Value ? (int)reader["university_id"] : 0,
                        };
                        result.Add(institute);
                    }
                }
            }
        }
        
        return result;
    }

    public IEnumerable<Supervisor> SearchSupervisor(string query)
    {
        List<Supervisor> result = new List<Supervisor>();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText =
                "SELECT s.id, s.name, s.phone_number " +
                "FROM supervisors s " +
                "WHERE LOWER(s.name) LIKE LOWER(@Query)";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Query", $"%{query}%");

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Supervisor supervisor = new Supervisor
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            Name = reader["name"] != DBNull.Value ? (string)reader["name"] : string.Empty,
                            PhoneNumber = reader["phone_number"] != DBNull.Value
                                ? (string)reader["phone_number"]
                                : string.Empty,
                        };
                        result.Add(supervisor);
                    }
                }
            }
        }
        
        return result;
    }

    public IEnumerable<SubjectTopic> SearchSubjectTopic(string query)
    {
        List<SubjectTopic> result = new List<SubjectTopic>();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText =
                "SELECT st.id, st.name " +
                "FROM subject_topics st " +
                "WHERE LOWER(st.name) LIKE LOWER(@Query)";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Query", $"%{query}%");

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SubjectTopic subjectTopic = new SubjectTopic
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            Name = reader["name"] != DBNull.Value ? (string)reader["name"] : string.Empty,
                        };
                        result.Add(subjectTopic);
                    }
                }
            }
        }
        
        return result;
    }

    public IEnumerable<Keyword> SearchKeyword(string query)
    {
        List<Keyword> result = new List<Keyword>();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText =
                "SELECT k.id, k.name " +
                "FROM keywords k " +
                "WHERE LOWER(k.name) LIKE LOWER(@Query)";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Query", $"%{query}%");

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Keyword keyword = new Keyword
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            Name = reader["name"] != DBNull.Value ? (string)reader["name"] : string.Empty,
                        };
                        result.Add(keyword);
                    }
                }
            }
        }
        
        return result;
    }
}