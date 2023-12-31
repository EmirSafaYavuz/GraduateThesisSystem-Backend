using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnSupervisorDal : ISupervisorDal
{
    private readonly string _connectionString;

    public AnSupervisorDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "supervisors";
    
    public Supervisor GetById(int id)
    {
        Supervisor supervisor = null;

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
                        supervisor = new Supervisor
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                        };
                    }
                }
            }
        }

        return supervisor;
    }

    public IList<Supervisor> GetAll()
    {
        throw new NotImplementedException();
    }

    public Supervisor Add(Supervisor entity)
    {
        throw new NotImplementedException();
    }

    public Supervisor Update(Supervisor entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Supervisor entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ThesisDetailDto> GetThesesBySupervisorId(int id)
    {
        throw new NotImplementedException();
    }
}