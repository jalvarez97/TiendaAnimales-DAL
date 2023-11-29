using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TiendaAnimales.Models;

namespace TuProyecto.DAL
{
    public class AnimalDAL
    {
        private readonly string connectionString; // Cadena de conexión a la base de datos

        public AnimalDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Animal> ObtenerTodosLosAnimales()
        {
            List<Animal> animales = new List<Animal>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IdAnimal, NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento FROM Animal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Animal animal = new Animal
                            {
                                IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                                NombreAnimal = reader["NombreAnimal"].ToString(),
                                Raza = reader["Raza"].ToString(),
                                RIdTipoAnimal = Convert.ToInt32(reader["RIdTipoAnimal"]),
                                FechaNacimiento = (reader["FechaNacimiento"] != DBNull.Value) 
                                                    ? Convert.ToDateTime(reader["FechaNacimiento"]) : (DateTime?)null
                            };
                            animales.Add(animal);
                        }
                    }
                }
            }

            return animales;
        }

        public Animal ObtenerAnimalPorId(int idAnimal)
        {
            Animal animal = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IdAnimal, NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento FROM Animal WHERE IdAnimal = @IdAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAnimal", idAnimal);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            animal = new Animal
                            {
                                IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                                NombreAnimal = reader["NombreAnimal"].ToString(),
                                Raza = reader["Raza"].ToString(),
                                RIdTipoAnimal = Convert.ToInt32(reader["RIdTipoAnimal"]),
                                FechaNacimiento = (reader["FechaNacimiento"] != DBNull.Value) ? Convert.ToDateTime(reader["FechaNacimiento"]) : (DateTime?)null
                            };
                        }
                    }
                }
            }

            return animal;
        }

        public void InsertarAnimal(Animal animal)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Animal (NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento) " +
                               "VALUES (@NombreAnimal, @Raza, @RIdTipoAnimal, @FechaNacimiento)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                    command.Parameters.AddWithValue("@Raza", animal.Raza);
                    command.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                    command.Parameters.AddWithValue("@FechaNacimiento", (object)animal.FechaNacimiento ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarAnimal(Animal animal)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Animal " +
                               "SET NombreAnimal = @NombreAnimal, Raza = @Raza, RIdTipoAnimal = @RIdTipoAnimal, FechaNacimiento = @FechaNacimiento " +
                               "WHERE IdAnimal = @IdAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
                    command.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                    command.Parameters.AddWithValue("@Raza", animal.Raza);
                    command.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                    command.Parameters.AddWithValue("@FechaNacimiento", (object)animal.FechaNacimiento ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EliminarAnimal(int idAnimal)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAnimal", idAnimal);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
