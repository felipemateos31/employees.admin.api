using Employees.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Admin.Data.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetEmployees(int typeConsult, int status, string name = "", string rfc = "");
        Task<Employee> GetEmployee(Guid Id);
        Task<Employee> CreateEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(Guid Id);

    }
}
