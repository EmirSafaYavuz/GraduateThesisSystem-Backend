using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using DataAccess.Entities.Enums;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnThesisDal : IThesisDal
{
    private readonly string _connectionString;

    public AnThesisDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "theses";

    public Thesis GetById(int id)
    {
        Thesis thesis = null;

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
                        thesis = new Thesis
                        {
                            Id = (int)reader["Id"],
                            ThesisNo = (int)reader["ThesisNo"],
                            Title = (string)reader["Title"],
                            Abstract = (string)reader["Abstract"],
                            Year = (int)reader["Year"],
                            NumOfPages = (int)reader["NumOfPages"],
                            SubmissionDate = (DateTime)reader["SubmissionDate"],
                            AuthorId = (int)reader["AuthorId"],
                            LanguageId = (int)reader["LanguageId"],
                            CoSupervisorId = (int)reader["CoSupervisorId"],
                            InstituteId = (int)reader["InstituteId"],
                            SupervisorId = (int)reader["SupervisorId"],
                            ThesisType = (ThesisType)reader["thesis_type"]
                        };
                    }
                }
            }
        }

        return thesis;
    }

    public IList<Thesis> GetAll()
    {
        List<Thesis> theses = new List<Thesis>();

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
                        Thesis thesis = new Thesis
                        {
                            Id = (int)reader["Id"],
                            ThesisNo = (int)reader["ThesisNo"],
                            Title = (string)reader["Title"],
                            Abstract = (string)reader["Abstract"],
                            Year = (int)reader["Year"],
                            NumOfPages = (int)reader["NumOfPages"],
                            SubmissionDate = (DateTime)reader["SubmissionDate"],
                            AuthorId = (int)reader["AuthorId"],
                            LanguageId = (int)reader["LanguageId"],
                            CoSupervisorId = (int)reader["CoSupervisorId"],
                            InstituteId = (int)reader["InstituteId"],
                            SupervisorId = (int)reader["SupervisorId"]
                        };

                        theses.Add(thesis);
                    }
                }
            }
        }

        return theses;
    }

    public Thesis Add(Thesis entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText =
                $"INSERT INTO {_tableName} (ThesisNo, Title, Abstract, Year, NumOfPages, SubmissionDate, AuthorId, LanguageId, CoSupervisorId, InstituteId, SupervisorId, thesis_type) VALUES (@ThesisNo, @Title, @Abstract, @Year, @NumOfPages, @SubmissionDate, @AuthorId, @LanguageId, @CoSupervisorId, @InstituteId, @SupervisorId, @ThesisType) RETURNING Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@ThesisNo", entity.ThesisNo);
                command.Parameters.AddWithValue("@Title", entity.Title);
                command.Parameters.AddWithValue("@Abstract", entity.Abstract);
                command.Parameters.AddWithValue("@Year", entity.Year);
                command.Parameters.AddWithValue("@NumOfPages", entity.NumOfPages);
                command.Parameters.AddWithValue("@SubmissionDate", entity.SubmissionDate);
                command.Parameters.AddWithValue("@AuthorId", entity.AuthorId);
                command.Parameters.AddWithValue("@LanguageId", entity.LanguageId);
                command.Parameters.AddWithValue("@CoSupervisorId", entity.CoSupervisorId);
                command.Parameters.AddWithValue("@InstituteId", entity.InstituteId);
                command.Parameters.AddWithValue("@SupervisorId", entity.SupervisorId);
                command.Parameters.AddWithValue("@ThesisType", entity.ThesisType);

                entity.Id = (int)command.ExecuteScalar();
            }
        }

        return entity;
    }

    public Thesis Update(Thesis entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText =
                $"UPDATE {_tableName} SET ThesisNo = @ThesisNo, Title = @Title, Abstract = @Abstract, Year = @Year, NumOfPages = @NumOfPages, SubmissionDate = @SubmissionDate, AuthorId = @AuthorId, LanguageId = @LanguageId, CoSupervisorId = @CoSupervisorId, InstituteId = @InstituteId, SupervisorId = @SupervisorId, thesis_type = @ThesisType WHERE Id = @Id";
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@ThesisNo", entity.ThesisNo);
                command.Parameters.AddWithValue("@Title", entity.Title);
                command.Parameters.AddWithValue("@Abstract", entity.Abstract);
                command.Parameters.AddWithValue("@Year", entity.Year);
                command.Parameters.AddWithValue("@NumOfPages", entity.NumOfPages);
                command.Parameters.AddWithValue("@SubmissionDate", entity.SubmissionDate);
                command.Parameters.AddWithValue("@AuthorId", entity.AuthorId);
                command.Parameters.AddWithValue("@LanguageId", entity.LanguageId);
                command.Parameters.AddWithValue("@CoSupervisorId", entity.CoSupervisorId);
                command.Parameters.AddWithValue("@InstituteId", entity.InstituteId);
                command.Parameters.AddWithValue("@SupervisorId", entity.SupervisorId);
                command.Parameters.AddWithValue("@ThesisType", entity.ThesisType);
                command.Parameters.AddWithValue("@Id", entity.Id);

                command.ExecuteNonQuery();
            }
        }

        return entity;
    }

    public void Delete(Thesis entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = $"DELETE FROM {_tableName} WHERE Id = @Id";
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

    public IEnumerable<ThesisDetailDto> GetAllDetailDto()
    {
        List<ThesisDetailDto> thesisDetailDtos = new List<ThesisDetailDto>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = $"SELECT t.id, t.thesis_no, t.title, t.abstract, t.year, t.num_of_pages, t.submission_date, t.author_id, t.language_id, t.co_supervisor_id, t.institute_id, t.supervisor_id, t.thesis_type, a.name AS authorname, a.email AS authoremail, l.name AS languagename, c.name AS cosupervisorname, i.name AS institutename, s.name AS supervisorname FROM theses t LEFT JOIN authors a ON t.author_id = a.id LEFT JOIN languages l ON t.language_id = l.id LEFT JOIN authors c ON t.co_supervisor_id = c.id LEFT JOIN institutes i ON t.institute_id = i.id LEFT JOIN authors s ON t.supervisor_id = s.id";
            
            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThesisDetailDto thesisDetailDto = new ThesisDetailDto
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            ThesisNo = reader["thesis_no"] != DBNull.Value ? (int)reader["thesis_no"] : 0,
                            Title = reader["title"] != DBNull.Value ? (string)reader["title"] : string.Empty, // Provide a default string value
                            Abstract = reader["abstract"] != DBNull.Value ? (string)reader["abstract"] : string.Empty,
                            Year = reader["year"] != DBNull.Value ? (int)reader["year"] : 0,
                            NumOfPages = reader["num_of_pages"] != DBNull.Value ? (int)reader["num_of_pages"] : 0,
                            SubmissionDate = reader["submission_date"] != DBNull.Value ? (DateTime)reader["submission_date"] : DateTime.MinValue,
                            AuthorId = reader["author_id"] != DBNull.Value ? (int)reader["author_id"] : 0,
                            LanguageId = reader["language_id"] != DBNull.Value ? (int)reader["language_id"] : 0,
                            CoSupervisorId = reader["co_supervisor_id"] != DBNull.Value ? (int)reader["co_supervisor_id"] : (int?)null,
                            InstituteId = reader["institute_id"] != DBNull.Value ? (int)reader["institute_id"] : 0,
                            SupervisorId = reader["supervisor_id"] != DBNull.Value ? (int)reader["supervisor_id"] : 0,
                            AuthorName = reader["authorname"] != DBNull.Value ? (string)reader["authorname"] : string.Empty,
                            LanguageName = reader["languagename"] != DBNull.Value ? (string)reader["languagename"] : string.Empty,
                            CoSupervisorName = reader["cosupervisorname"] != DBNull.Value ? (string)reader["cosupervisorname"] : string.Empty,
                            InstituteName = reader["institutename"] != DBNull.Value ? (string)reader["institutename"] : string.Empty,
                            SupervisorName = reader["supervisorname"] != DBNull.Value ? (string)reader["supervisorname"] : string.Empty,
                            ThesisType = reader["thesis_type"] != DBNull.Value ? (string)reader["thesis_type"] : string.Empty
                        };

                        thesisDetailDtos.Add(thesisDetailDto);
                    }
                }
            }
        }
        
        return thesisDetailDtos;
    }
}