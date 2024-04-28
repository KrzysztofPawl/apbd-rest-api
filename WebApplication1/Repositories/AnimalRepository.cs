using System.Data.SqlClient;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Animal> getAnimals(string orderBy)
    {
        List<Animal> animals = new List<Animal>();

        var query = $"SELECT * FROM Animal";
        var allowedOrderBys = new List<string> { "IdAnimal", "Name", "Description", "Category", "Area" };
        if (allowedOrderBys.Contains(orderBy))
        {
            query += $" ORDER BY {orderBy} ASC;";
        }
        else
        {
            Console.Out.WriteLine("Column does not exist, returning default orderBy Name");
            query += " ORDER BY Name ASC;";
        }
        
        var connectionString = _configuration.GetConnectionString("AnimalDatabase");
        
        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                var command = new SqlCommand(query, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var animal = new Animal
                        {
                            IdAnimal = reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Description = (reader.IsDBNull(reader.GetOrdinal("Description"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("Description"))) ?? string.Empty,
                            Category = reader.GetString(reader.GetOrdinal("Category")),
                            Area = reader.GetString(reader.GetOrdinal("Area"))
                        };
                        animals.Add(animal);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.Out.WriteLine("Request exception");
                return animals;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Other exception");
                return animals;
            }
        }
        return animals;
    }

    public bool createAnimal(Animal animal)
    {
        var query = "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area);";
        var connectionString = _configuration.GetConnectionString("AnimalDatabase");

        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@Description", animal.Description);
                command.Parameters.AddWithValue("@Category", animal.Category);
                command.Parameters.AddWithValue("@Area", animal.Area);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch (SqlException e)
            {
                Console.Out.WriteLine("Request exception");
                return false;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Other exception");
                return false;
            }
        }
    }

    public bool updateAnimal(int animalId, Animal animal)
    {
        var query = "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal;";
        var connectionString = _configuration.GetConnectionString("AnimalDatabase");
        
        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@Description", animal.Description);
                command.Parameters.AddWithValue("@Category", animal.Category);
                command.Parameters.AddWithValue("@Area", animal.Area);
                command.Parameters.AddWithValue("@IdAnimal", animalId);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch (SqlException e)
            {
                Console.Out.WriteLine("Request exception");
                return false;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Other exception");
                return false;
            }
        }
    }

    public bool deleteAnimal(int animalId)
    {
        var query = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal;";
        var connectionString = _configuration.GetConnectionString("AnimalDatabase");

        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdAnimal", animalId);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch (SqlException e)
            {
                Console.Out.WriteLine("Request exception");
                return false;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Other exception");
                return false;
            }
        }
    }
}