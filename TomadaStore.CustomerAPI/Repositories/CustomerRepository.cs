using Dapper;
using Microsoft.Data.SqlClient;
using TomadaStore.CustomerAPI.Data;
using TomadaStore.CustomerAPI.Repositories.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly SqlConnection _connection;

        public CustomerRepository(ILogger<CustomerRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection.GetConnection();
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            try
            {
                var sqlSelect = @"SELECT Id, FirstName, LastName, Email, PhoneNumber, Status
                                FROM Customers";

                var customers = await _connection.QueryAsync<CustomerResponseDTO>(sqlSelect);

                return customers.ToList();

            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error retrieving customer: {sqlEx.Message}");
                throw new Exception(sqlEx.StackTrace);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving customer: {ex.Message}");
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
                var insertSql = @"INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber, Status)
                                    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Status)";

                await _connection.ExecuteAsync(insertSql, new
                {
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.PhoneNumber,
                    Status = true
                });
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error inserting customer: {sqlEx.Message}");
                throw new Exception(sqlEx.StackTrace);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.Message}");
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            try
            {
                var sqlSelect = @"SELECT Id, FirstName, LastName, Email, PhoneNumber, Status
                                FROM Customers 
                                WHERE Id = @Id";

                return await _connection.QueryFirstOrDefaultAsync<CustomerResponseDTO>(sqlSelect, new { id });
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error retrieving customer: {sqlEx.Message}");
                throw new Exception(sqlEx.StackTrace);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving customer: {ex.Message}");
                throw new Exception(ex.StackTrace);
            }
        }
    }
}
