using DataAccess.Abstract;
using DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet;

public class AnLocationDal : ILocationDal
{
    private readonly string _connectionString;

    public AnLocationDal(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private readonly string _tableName = "locations";
    
    public Location GetById(int id)
    {
        Location location = null;

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
                        location = new Location
                        {
                            Id = (int)reader["Id"],
                            City = reader["City"].ToString(),
                            Country = reader["Country"].ToString()
                        };
                    }
                }
            }
        }

        return location;
    }

    public IList<Location> GetAll()
    {
        throw new NotImplementedException();
    }

    public Location Add(Location entity)
    {
        throw new NotImplementedException();
    }

    public Location Update(Location entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Location entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }
}