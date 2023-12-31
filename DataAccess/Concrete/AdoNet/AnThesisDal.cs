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
        throw new NotImplementedException();
    }

    public Thesis Add(Thesis entity)
    {
        throw new NotImplementedException();
    }

    public Thesis Update(Thesis entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Thesis entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ThesisDetailDto> GetListDetailDto()
    {
        throw new NotImplementedException();
    }
}