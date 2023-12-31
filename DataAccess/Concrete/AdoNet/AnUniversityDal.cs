using System.Linq.Expressions;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnUniversityDal : IUniversityDal
{
    private readonly string _connectionString;

    public AnUniversityDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "universities";
    
    public University GetById(int id)
    {
        University university = null;

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
                        university = new University
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            LocationId = (int)reader["LocationId"]
                        };
                    }
                }
            }
        }

        return university;
    }

    public IList<University> GetAll()
    {
        throw new NotImplementedException();
    }

    public University Add(University entity)
    {
        throw new NotImplementedException();
    }

    public University Update(University entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(University entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UniversityDetailDto> GetListDetailDto(Expression<Func<UniversityDetailDto, bool>> expression = null)
    {
        throw new NotImplementedException();
    }

    public UniversityDetailDto GetDetailDto(Expression<Func<UniversityDetailDto, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<InstituteDetailDto> GetInstitutesByUniversityId(int id)
    {
        throw new NotImplementedException();
    }
}