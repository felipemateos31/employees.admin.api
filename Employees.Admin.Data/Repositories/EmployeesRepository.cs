using Dapper;
using Employees.Admin.Data.Interfaces;
using Employees.Admin.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Admin.Data.Repositories
{
    public class EmployeesRepository : BaseRepository, IEmployeesRepository
    {
        private readonly ILogger<EmployeesRepository> _logger;

        public EmployeesRepository(IConfiguration config, ILogger<EmployeesRepository> logger) : base(config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            var storedProcedure = "spInsEmployee";
            using (var connection = GetOpenConnection())
            {
                var dp = new DynamicParameters();
                dp.Add("@pId", Guid.NewGuid());
                dp.Add("@pFirstName", employee.FirstName);
                dp.Add("@pLastName", employee.LastName);
                dp.Add("@pMiddleName", employee.MiddleName);
                dp.Add("@pAge", employee.Age);
                dp.Add("@pBirthDate", employee.BirthDate);
                dp.Add("@pGender", employee.Gender);
                dp.Add("@pMaritalStatus", employee.MaritalStatus);
                dp.Add("@pRfc", employee.Rfc);
                dp.Add("@pAddress", employee.Address);
                dp.Add("@pEmail", employee.Email);
                dp.Add("@pPhone", employee.Phone);
                dp.Add("@pPosition", employee.Position);
                dp.Add("@pStartDate", employee.StartDate);
                dp.Add("@pEndDate", employee.EndDate is null ? null : employee.EndDate);
                return connection.Query<Employee>(storedProcedure, dp, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var storedProcedure = "spUpdEmployee";
            using (var connection = GetOpenConnection())
            {
                var dp = new DynamicParameters();
                dp.Add("@pId", employee.Id);
                dp.Add("@pFirstName", employee.FirstName);
                dp.Add("@pLastName", employee.LastName);
                dp.Add("@pMiddleName", employee.MiddleName);
                dp.Add("@pAge", employee.Age);
                dp.Add("@pBirthDate", employee.BirthDate);
                dp.Add("@pGender", employee.Gender);
                dp.Add("@pMaritalStatus", employee.MaritalStatus);
                dp.Add("@pRfc", employee.Rfc);
                dp.Add("@pAddress", employee.Address);
                dp.Add("@pEmail", employee.Email);
                dp.Add("@pPhone", employee.Phone);
                dp.Add("@pPosition", employee.Position);
                dp.Add("@pStartDate", employee.StartDate);
                dp.Add("@pEndDate", employee.EndDate is null ? null : employee.EndDate);
                return connection.Query<Employee>(storedProcedure, dp, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public async Task<Employee> DeleteEmployee(Guid Id)
        {
            var storedProcedure = "spDelEmployee";
            using (var connection = GetOpenConnection())
            {
                var dp = new DynamicParameters();
                dp.Add("@pId", Id);
                return connection.Query<Employee>(storedProcedure, dp, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public async Task<Employee> GetEmployee(Guid Id)
        {
            var storedProcedure = "spSelEmployee";
            using (var connection = GetOpenConnection())
            {
                var dp = new DynamicParameters();
                dp.Add("@pId", Id);
                return connection.Query<Employee>(storedProcedure, dp, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public async Task<List<Employee>> GetEmployees(int typeConsult, int status, string name = "", string rfc = "")
        {
            var storedProcedure = "spSelEmployeeList";
            using (var connection = GetOpenConnection())
            {
                var dp = new DynamicParameters();
                dp.Add("@pTypeConsult", typeConsult);
                dp.Add("@pName", name);
                dp.Add("@pRfc", rfc);
                dp.Add("@pStatus", status);
                return connection.Query<Employee>(storedProcedure, dp, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }
    }
}
