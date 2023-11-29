using AnimalesMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TuProyecto.DAL
{
    public class TipoAnimalDAL
    {
        private readonly string connectionString; // Cadena de conexión a la base de datos

        public TipoAnimalDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<TipoAnimal> ObtenerTodosLosTiposAnimales()
        {
            List<TipoAnimal> tiposAnimales = new List<TipoAnimal>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IdTipoAnimal, TipoDescripcion FROM TipoAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TipoAnimal tipoAnimal = new TipoAnimal
                            {
                                IdTipoAnimal = Convert.ToInt32(reader["IdTipoAnimal"]),
                                TipoDescripcion = reader["TipoDescripcion"].ToString()
                            };
                            tiposAnimales.Add(tipoAnimal);
                        }
                    }
                }
            }

            return tiposAnimales;
        }

        public TipoAnimal ObtenerTipoAnimalPorId(int idTipoAnimal)
        {
            TipoAnimal tipoAnimal = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IdTipoAnimal, TipoDescripcion FROM TipoAnimal WHERE IdTipoAnimal = @IdTipoAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTipoAnimal", idTipoAnimal);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tipoAnimal = new TipoAnimal
                            {
                                IdTipoAnimal = Convert.ToInt32(reader["IdTipoAnimal"]),
                                TipoDescripcion = reader["TipoDescripcion"].ToString()
                            };
                        }
                    }
                }
            }

            return tipoAnimal;
        }

        public void InsertarTipoAnimal(TipoAnimal tipoAnimal)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TipoAnimal (TipoDescripcion) VALUES (@TipoDescripcion)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TipoDescripcion", tipoAnimal.TipoDescripcion);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarTipoAnimal(TipoAnimal tipoAnimal)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE TipoAnimal SET TipoDescripcion = @TipoDescripcion WHERE IdTipoAnimal = @IdTipoAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTipoAnimal", tipoAnimal.IdTipoAnimal);
                    command.Parameters.AddWithValue("@TipoDescripcion", tipoAnimal.TipoDescripcion);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EliminarTipoAnimal(int idTipoAnimal)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM TipoAnimal WHERE IdTipoAnimal = @IdTipoAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTipoAnimal", idTipoAnimal);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}