using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using DataAccess.Entities.Enums;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

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
                $"INSERT INTO {_tableName} (thesis_no, Title, Abstract, Year, num_of_pages, submission_date, author_id, language_id, co_supervisor_id, institute_id, supervisor_id, thesis_type) VALUES (@ThesisNo, @Title, @Abstract, @Year, @NumOfPages, @SubmissionDate, @AuthorId, @LanguageId, @CoSupervisorId, @InstituteId, @SupervisorId, @ThesisType::thesis_type) RETURNING Id";

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

                command.Parameters.Add(new NpgsqlParameter("@ThesisType", NpgsqlDbType.Varchar)
                {
                    Value = entity.ThesisType.GetThesisTypeAsString(),
                });

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

    public IEnumerable<ThesisLookupDto> GetAllLookupDto()
    {
        
        List<ThesisLookupDto> thesisLookupDtos = new List<ThesisLookupDto>();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = @"
            SELECT
                t.id, t.thesis_no, t.title, t.author_id, a.name AS author_name, t.thesis_type
            FROM
                theses t
            LEFT JOIN
                authors a ON t.author_id = a.id";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThesisLookupDto thesisLookupDto = new ThesisLookupDto
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            ThesisNo = reader["thesis_no"] != DBNull.Value ? (int)reader["thesis_no"] : 0,
                            Title = reader["title"] != DBNull.Value ? (string)reader["title"] : string.Empty,
                            AuthorId = reader["author_id"] != DBNull.Value ? (int)reader["author_id"] : 0,
                            AuthorName = reader["author_name"] != DBNull.Value
                                ? (string)reader["author_name"]
                                : string.Empty,
                            ThesisType = reader["thesis_type"] != DBNull.Value
                                ? (string)reader["thesis_type"]
                                : string.Empty
                        };

                        thesisLookupDtos.Add(thesisLookupDto);
                    }
                }
            }

            return thesisLookupDtos;
        }
    }

    public ThesisDetailDto GetDetailDtoById(int id)
    {
        ThesisDetailDto thesisDetailDto = null;

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            string commandText = @"
            SELECT
                t.id, t.thesis_no, t.title, t.abstract, t.year, t.num_of_pages, t.submission_date,
                t.author_id, t.language_id, t.institute_id,
                a.name AS author_name, a.email AS author_email,
                l.name AS language_name,
                t.co_supervisor_id, cs.name AS co_supervisor_name,
                t.supervisor_id, su.name AS supervisor_name,
                i.name AS institute_name,
                t.thesis_type
            FROM
                theses t
            LEFT JOIN
                authors a ON t.author_id = a.id
            LEFT JOIN
                languages l ON t.language_id = l.id
            LEFT JOIN
                institutes i ON t.institute_id = i.id
            LEFT JOIN
                supervisors su ON t.supervisor_id = su.id
            LEFT JOIN
                supervisors cs ON t.co_supervisor_id = cs.id
            WHERE
                t.id = @Id";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        thesisDetailDto = new ThesisDetailDto
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            ThesisNo = reader["thesis_no"] != DBNull.Value ? (int)reader["thesis_no"] : 0,
                            Title = reader["title"] != DBNull.Value ? (string)reader["title"] : string.Empty,
                            Abstract = reader["abstract"] != DBNull.Value ? (string)reader["abstract"] : string.Empty,
                            Year = reader["year"] != DBNull.Value ? (int)reader["year"] : 0,
                            NumOfPages = reader["num_of_pages"] != DBNull.Value ? (int)reader["num_of_pages"] : 0,
                            SubmissionDate = reader["submission_date"] != DBNull.Value
                                ? (DateTime)reader["submission_date"]
                                : DateTime.MinValue,
                            AuthorId = reader["author_id"] != DBNull.Value ? (int)reader["author_id"] : 0,
                            LanguageId = reader["language_id"] != DBNull.Value ? (int)reader["language_id"] : 0,
                            InstituteId = reader["institute_id"] != DBNull.Value ? (int)reader["institute_id"] : 0,
                            AuthorName = reader["author_name"] != DBNull.Value
                                ? (string)reader["author_name"]
                                : string.Empty,
                            LanguageName = reader["language_name"] != DBNull.Value
                                ? (string)reader["language_name"]
                                : string.Empty,
                            CoSupervisorId = reader["co_supervisor_id"] != DBNull.Value
                                ? (int)reader["co_supervisor_id"]
                                : null,
                            CoSupervisorName = reader["co_supervisor_name"] != DBNull.Value
                                ? (string)reader["co_supervisor_name"]
                                : string.Empty,
                            SupervisorId = reader["supervisor_id"] != DBNull.Value ? (int)reader["supervisor_id"] : 0,
                            SupervisorName = reader["supervisor_name"] != DBNull.Value
                                ? (string)reader["supervisor_name"]
                                : string.Empty,
                            InstituteName = reader["institute_name"] != DBNull.Value
                                ? (string)reader["institute_name"]
                                : string.Empty,
                            ThesisType = reader["thesis_type"] != DBNull.Value
                                ? (string)reader["thesis_type"]
                                : string.Empty
                        };
                    }
                }
            }

            return thesisDetailDto;
        }
    }
}