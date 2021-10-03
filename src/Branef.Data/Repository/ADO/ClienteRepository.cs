using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Branef.Negocio.Models;
using Branef.Negocio.Models.Enum;
using Branef.Negocio.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Branef.Data.Repository.ADO
{
    public class ClienteRepository : IClienteRepository
    {
        private string connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");       
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task Adicionar(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> ObterPorId(Guid id)
        {
            var cliente = new Cliente();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var comandoSQL = "SELECT Id, Nome, Porte, CreatedAt, UpdatedAt FROM Clientes WHERE Id = @Id";
                var cmd = new SqlCommand(comandoSQL, connection);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", id);

                connection.Open();

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    cliente = PreencherObjeto(reader);
                }
                connection.Close();
            }

            return cliente;
        }

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            var clientes = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var comandoSQL = "SELECT Id, Nome, Porte, CreatedAt, UpdatedAt FROM Clientes";
                var cmd = new SqlCommand(comandoSQL, connection);
                cmd.CommandType = CommandType.Text;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var cliente = PreencherObjeto(reader);
                    clientes.Add(cliente);
                }
                connection.Close();
            }

            return clientes;
        }

        public Task Atualizar(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public async Task Remover(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var comandoSQL = "DELETE FROM CLIENTES WHERE Id = @Id";
                var cmd = new SqlCommand(comandoSQL, connection);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", id);

                connection.Open();
                await cmd.ExecuteNonQueryAsync();
                connection.Close();
            }
        }

        public async Task<bool> Existe(Guid id)
        {
            var exists = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var comandoSQL = "SELECT Id FROM Clientes WHERE Id = @Id";
                var cmd = new SqlCommand(comandoSQL, connection);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                exists = reader.HasRows;
                connection.Close();
            }

            return exists;
        }
        private Cliente PreencherObjeto(SqlDataReader reader)
        {
            var cliente = new Cliente
            {
                Id = new Guid(reader["Id"].ToString()),
                Nome = reader["Nome"].ToString(),
                Porte = (EPorteEmpresa)Enum.Parse(typeof(EPorteEmpresa), reader["Porte"].ToString()),
                CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
            };
            return cliente;
        }
    }
}