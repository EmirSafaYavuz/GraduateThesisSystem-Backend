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

    public IEnumerable<ThesisDetailDto> SearchThesisTitle(string query, ThesisType? thesisType)
    {
        List<ThesisDetailDto> result = new List<ThesisDetailDto>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText =
                "SELECT t.id, t.thesis_no, t.title, t.abstract, t.year, t.num_of_pages, t.submission_date, " +
                "t.author_id, t.language_id, t.co_supervisor_id, t.institute_id, t.supervisor_id, t.thesis_type, " +
                "a.name AS authorname, a.email AS authoremail, l.name AS languagename, c.name AS cosupervisorname, " +
                "i.name AS institutename, s.name AS supervisorname FROM theses t " +
                "LEFT JOIN authors a ON t.author_id = a.id " +
                "LEFT JOIN languages l ON t.language_id = l.id " +
                "LEFT JOIN authors c ON t.co_supervisor_id = c.id " +
                "LEFT JOIN institutes i ON t.institute_id = i.id " +
                "LEFT JOIN authors s ON t.supervisor_id = s.id " +
                "WHERE LOWER(t.title) LIKE LOWER(@Query)";

            // Optionally add thesis type filter
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
                        ThesisDetailDto thesisDetailDto = new ThesisDetailDto
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            ThesisNo = reader["thesis_no"] != DBNull.Value ? (int)reader["thesis_no"] : 0,
                            Title = reader["title"] != DBNull.Value
                                ? (string)reader["title"]
                                : string.Empty, // Provide a default string value
                            Abstract = reader["abstract"] != DBNull.Value ? (string)reader["abstract"] : string.Empty,
                            Year = reader["year"] != DBNull.Value ? (int)reader["year"] : 0,
                            NumOfPages = reader["num_of_pages"] != DBNull.Value ? (int)reader["num_of_pages"] : 0,
                            SubmissionDate = reader["submission_date"] != DBNull.Value
                                ? (DateTime)reader["submission_date"]
                                : DateTime.MinValue,
                            AuthorId = reader["author_id"] != DBNull.Value ? (int)reader["author_id"] : 0,
                            LanguageId = reader["language_id"] != DBNull.Value ? (int)reader["language_id"] : 0,
                            CoSupervisorId = reader["co_supervisor_id"] != DBNull.Value
                                ? (int)reader["co_supervisor_id"]
                                : (int?)null,
                            InstituteId = reader["institute_id"] != DBNull.Value ? (int)reader["institute_id"] : 0,
                            SupervisorId = reader["supervisor_id"] != DBNull.Value ? (int)reader["supervisor_id"] : 0,
                            AuthorName = reader["authorname"] != DBNull.Value
                                ? (string)reader["authorname"]
                                : string.Empty,
                            LanguageName = reader["languagename"] != DBNull.Value
                                ? (string)reader["languagename"]
                                : string.Empty,
                            CoSupervisorName = reader["cosupervisorname"] != DBNull.Value
                                ? (string)reader["cosupervisorname"]
                                : string.Empty,
                            InstituteName = reader["institutename"] != DBNull.Value
                                ? (string)reader["institutename"]
                                : string.Empty,
                            SupervisorName = reader["supervisorname"] != DBNull.Value
                                ? (string)reader["supervisorname"]
                                : string.Empty,
                            ThesisType = reader["thesis_type"] != DBNull.Value
                                ? (string)reader["thesis_type"]
                                : string.Empty
                        };
                        result.Add(thesisDetailDto);
                    }
                }
            }
        }

        return result;
    }
    
    public IEnumerable<ThesisDetailDto> SearchThesisAbstract(string query, ThesisType? thesisType)
    {
        List<ThesisDetailDto> result = new List<ThesisDetailDto>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText =
                "SELECT t.id, t.thesis_no, t.title, t.abstract, t.year, t.num_of_pages, t.submission_date, " +
                "t.author_id, t.language_id, t.co_supervisor_id, t.institute_id, t.supervisor_id, t.thesis_type, " +
                "a.name AS authorname, a.email AS authoremail, l.name AS languagename, c.name AS cosupervisorname, " +
                "i.name AS institutename, s.name AS supervisorname FROM theses t " +
                "LEFT JOIN authors a ON t.author_id = a.id " +
                "LEFT JOIN languages l ON t.language_id = l.id " +
                "LEFT JOIN authors c ON t.co_supervisor_id = c.id " +
                "LEFT JOIN institutes i ON t.institute_id = i.id " +
                "LEFT JOIN authors s ON t.supervisor_id = s.id " +
                "WHERE LOWER(t.abstract) LIKE LOWER(@Query)";

            // Optionally add thesis type filter
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
                        ThesisDetailDto thesisDetailDto = new ThesisDetailDto
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            ThesisNo = reader["thesis_no"] != DBNull.Value ? (int)reader["thesis_no"] : 0,
                            Title = reader["title"] != DBNull.Value
                                ? (string)reader["title"]
                                : string.Empty, // Provide a default string value
                            Abstract = reader["abstract"] != DBNull.Value ? (string)reader["abstract"] : string.Empty,
                            Year = reader["year"] != DBNull.Value ? (int)reader["year"] : 0,
                            NumOfPages = reader["num_of_pages"] != DBNull.Value ? (int)reader["num_of_pages"] : 0,
                            SubmissionDate = reader["submission_date"] != DBNull.Value
                                ? (DateTime)reader["submission_date"]
                                : DateTime.MinValue,
                            AuthorId = reader["author_id"] != DBNull.Value ? (int)reader["author_id"] : 0,
                            LanguageId = reader["language_id"] != DBNull.Value ? (int)reader["language_id"] : 0,
                            CoSupervisorId = reader["co_supervisor_id"] != DBNull.Value
                                ? (int)reader["co_supervisor_id"]
                                : (int?)null,
                            InstituteId = reader["institute_id"] != DBNull.Value ? (int)reader["institute_id"] : 0,
                            SupervisorId = reader["supervisor_id"] != DBNull.Value ? (int)reader["supervisor_id"] : 0,
                            AuthorName = reader["authorname"] != DBNull.Value
                                ? (string)reader["authorname"]
                                : string.Empty,
                            LanguageName = reader["languagename"] != DBNull.Value
                                ? (string)reader["languagename"]
                                : string.Empty,
                            CoSupervisorName = reader["cosupervisorname"] != DBNull.Value
                                ? (string)reader["cosupervisorname"]
                                : string.Empty,
                            InstituteName = reader["institutename"] != DBNull.Value
                                ? (string)reader["institutename"]
                                : string.Empty,
                            SupervisorName = reader["supervisorname"] != DBNull.Value
                                ? (string)reader["supervisorname"]
                                : string.Empty,
                            ThesisType = reader["thesis_type"] != DBNull.Value
                                ? (string)reader["thesis_type"]
                                : string.Empty
                        };

                        result.Add(thesisDetailDto);
                    }
                }
            }
        }

        return result;
    }

    public IEnumerable<ThesisDetailDto> SearchThesisNo(string query, ThesisType? thesisType)
    {
        List<ThesisDetailDto> result = new List<ThesisDetailDto>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText =
                "SELECT t.id, t.thesis_no, t.title, t.abstract, t.year, t.num_of_pages, t.submission_date, " +
                "t.author_id, t.language_id, t.co_supervisor_id, t.institute_id, t.supervisor_id, t.thesis_type, " +
                "a.name AS authorname, a.email AS authoremail, l.name AS languagename, c.name AS cosupervisorname, " +
                "i.name AS institutename, s.name AS supervisorname FROM theses t " +
                "LEFT JOIN authors a ON t.author_id = a.id " +
                "LEFT JOIN languages l ON t.language_id = l.id " +
                "LEFT JOIN authors c ON t.co_supervisor_id = c.id " +
                "LEFT JOIN institutes i ON t.institute_id = i.id " +
                "LEFT JOIN authors s ON t.supervisor_id = s.id " +
                "WHERE t.thesis_no::text LIKE @Query";

            // Optionally add thesis type filter
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
                        ThesisDetailDto thesisDetailDto = new ThesisDetailDto
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            ThesisNo = reader["thesis_no"] != DBNull.Value ? (int)reader["thesis_no"] : 0,
                            Title = reader["title"] != DBNull.Value
                                ? (string)reader["title"]
                                : string.Empty, // Provide a default string value
                            Abstract = reader["abstract"] != DBNull.Value ? (string)reader["abstract"] : string.Empty,
                            Year = reader["year"] != DBNull.Value ? (int)reader["year"] : 0,
                            NumOfPages = reader["num_of_pages"] != DBNull.Value ? (int)reader["num_of_pages"] : 0,
                            SubmissionDate = reader["submission_date"] != DBNull.Value
                                ? (DateTime)reader["submission_date"]
                                : DateTime.MinValue,
                            AuthorId = reader["author_id"] != DBNull.Value ? (int)reader["author_id"] : 0,
                            LanguageId = reader["language_id"] != DBNull.Value ? (int)reader["language_id"] : 0,
                            CoSupervisorId = reader["co_supervisor_id"] != DBNull.Value
                                ? (int)reader["co_supervisor_id"]
                                : (int?)null,
                            InstituteId = reader["institute_id"] != DBNull.Value ? (int)reader["institute_id"] : 0,
                            SupervisorId = reader["supervisor_id"] != DBNull.Value ? (int)reader["supervisor_id"] : 0,
                            AuthorName = reader["authorname"] != DBNull.Value
                                ? (string)reader["authorname"]
                                : string.Empty,
                            LanguageName = reader["languagename"] != DBNull.Value
                                ? (string)reader["languagename"]
                                : string.Empty,
                            CoSupervisorName = reader["cosupervisorname"] != DBNull.Value
                                ? (string)reader["cosupervisorname"]
                                : string.Empty,
                            InstituteName = reader["institutename"] != DBNull.Value
                                ? (string)reader["institutename"]
                                : string.Empty,
                            SupervisorName = reader["supervisorname"] != DBNull.Value
                                ? (string)reader["supervisorname"]
                                : string.Empty,
                            ThesisType = reader["thesis_type"] != DBNull.Value
                                ? (string)reader["thesis_type"]
                                : string.Empty
                        };

                        result.Add(thesisDetailDto);
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
                "SELECT a.id, a.name, a.email, a.affiliation, a.affiliation_url, a.photo_url, a.biography, " +
                "a.interests, a.thesis_id, t.thesis_no, t.title, t.abstract, t.year, t.num_of_pages, t.submission_date, " +
                "t.author_id, t.language_id, t.co_supervisor_id, t.institute_id, t.supervisor_id, t.thesis_type, " +
                "l.name AS languagename, c.name AS cosupervisorname, i.name AS institutename, s.name AS supervisorname " +
                "FROM authors a " +
                "LEFT JOIN theses t ON a.thesis_id = t.id " +
                "LEFT JOIN languages l ON t.language_id = l.id " +
                "LEFT JOIN authors c ON t.co_supervisor_id = c.id " +
                "LEFT JOIN institutes i ON t.institute_id = i.id " +
                "LEFT JOIN authors s ON t.supervisor_id = s.id " +
                "WHERE LOWER(a.name) LIKE LOWER(@Query)";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                // Add parameters to the query
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
                "SELECT i.id, i.name, i.url, i.address, i.phone, i.fax, i.email, i.photo_url, i.thesis_id, " +
                "t.thesis_no, t.title, t.abstract, t.year, t.num_of_pages, t.submission_date, t.author_id, " +
                "t.language_id, t.co_supervisor_id, t.institute_id, t.supervisor_id, t.thesis_type, " +
                "a.name AS authorname, a.email AS authoremail, l.name AS languagename, c.name AS cosupervisorname, " +
                "s.name AS supervisorname FROM institutes i " +
                "LEFT JOIN theses t ON i.thesis_id = t.id " +
                "LEFT JOIN authors a ON t.author_id = a.id " +
                "LEFT JOIN languages l ON t.language_id = l.id " +
                "LEFT JOIN authors c ON t.co_supervisor_id = c.id " +
                "LEFT JOIN authors s ON t.supervisor_id = s.id " +
                "WHERE LOWER(i.name) LIKE LOWER(@Query)";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                // Add parameters to the query
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

    public Task<IEnumerable<SupervisorDetailDto>> SearchSupervisor(string query)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<SubjectTopic> SearchSubjectTopic(string query)
    {
        List<SubjectTopic> result = new List<SubjectTopic>();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText =
                "SELECT st.id, st.name, st.thesis_id, t.thesis_no, t.title, t.abstract, t.year, t.num_of_pages, " +
                "t.submission_date, t.author_id, t.language_id, t.co_supervisor_id, t.institute_id, t.supervisor_id, " +
                "t.thesis_type, a.name AS authorname, a.email AS authoremail, l.name AS languagename, " +
                "c.name AS cosupervisorname, i.name AS institutename, s.name AS supervisorname FROM subject_topics st " +
                "LEFT JOIN theses t ON st.thesis_id = t.id " +
                "LEFT JOIN authors a ON t.author_id = a.id " +
                "LEFT JOIN languages l ON t.language_id = l.id " +
                "LEFT JOIN authors c ON t.co_supervisor_id = c.id " +
                "LEFT JOIN institutes i ON t.institute_id = i.id " +
                "LEFT JOIN authors s ON t.supervisor_id = s.id " +
                "WHERE LOWER(st.name) LIKE LOWER(@Query)";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                // Add parameters to the query
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
                "SELECT k.id, k.name, k.thesis_id, t.thesis_no, t.title, t.abstract, t.year, t.num_of_pages, " +
                "t.submission_date, t.author_id, t.language_id, t.co_supervisor_id, t.institute_id, t.supervisor_id, " +
                "t.thesis_type, a.name AS authorname, a.email AS authoremail, l.name AS languagename, " +
                "c.name AS cosupervisorname, i.name AS institutename, s.name AS supervisorname FROM keywords k " +
                "LEFT JOIN theses t ON k.thesis_id = t.id " +
                "LEFT JOIN authors a ON t.author_id = a.id " +
                "LEFT JOIN languages l ON t.language_id = l.id " +
                "LEFT JOIN authors c ON t.co_supervisor_id = c.id " +
                "LEFT JOIN institutes i ON t.institute_id = i.id " +
                "LEFT JOIN authors s ON t.supervisor_id = s.id " +
                "WHERE LOWER(k.name) LIKE LOWER(@Query)";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                // Add parameters to the query
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