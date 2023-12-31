using System.Collections.Generic;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Concrete.AdoNet
{
    public class AnAuthorDal : IAuthorDal
    {
        private readonly string _connectionString;

        public AnAuthorDal(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private readonly string _tableName = "authors";

        public Author GetById(int id)
        {
            Author author = null;

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
                            author = new Author
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Email = (string)reader["Email"]
                            };
                        }
                    }
                }
            }

            return author;
        }

        public IList<Author> GetAll()
        {
            List<Author> authors = new List<Author>();

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
                            Author author = new Author
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString()
                            };

                            authors.Add(author);
                        }
                    }
                }
            }

            return authors;
        }

        public Author Add(Author entity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var commandText = $"INSERT INTO {_tableName} (Name, Email) VALUES (@Name, @Email) RETURNING Id";
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Email", entity.Email);

                    // ExecuteScalar is used to get the newly inserted Id
                    entity.Id = (int)command.ExecuteScalar();
                }
            }

            return entity;
        }

        public Author Update(Author entity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var commandText = $"UPDATE {_tableName} SET Name = @Name, Email = @Email WHERE Id = @Id";
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Email", entity.Email);
                    command.Parameters.AddWithValue("@Id", entity.Id);

                    command.ExecuteNonQuery();
                }
            }

            return entity;
        }

        public void Delete(Author entity)
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

        public IEnumerable<ThesisDetailDto> GetThesesByAuthorId(int id)
        {
            //TODO Implement
            throw new NotImplementedException();
        }
    }
}
