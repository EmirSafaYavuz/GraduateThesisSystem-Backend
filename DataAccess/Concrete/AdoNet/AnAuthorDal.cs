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
                                Name = (string)reader["Name"],
                                Email = (string)reader["Email"]
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

        public IEnumerable<ThesisLookupDto> GetThesesByAuthorId(int id)
        {
            List<ThesisLookupDto> theses = new List<ThesisLookupDto>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string commandText = $"SELECT t.Id, t.ThesisNo, t.Title, t.AuthorId, a.Name AS AuthorName, t.ThesisType FROM theses t INNER JOIN authors a ON t.AuthorId = a.Id WHERE t.AuthorId = @Id";

                using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThesisLookupDto thesis = new ThesisLookupDto
                            {
                                Id = (int)reader["Id"],
                                ThesisNo = (int)reader["ThesisNo"],
                                Title = (string)reader["Title"],
                                AuthorId = (int)reader["AuthorId"],
                                AuthorName = (string)reader["AuthorName"],
                                ThesisType = reader["thesis_type"] != DBNull.Value
                                    ? (string)reader["thesis_type"]
                                    : string.Empty
                            };

                            theses.Add(thesis);
                        }
                    }
                }
                
                return theses;
            }
        }
    }
}
