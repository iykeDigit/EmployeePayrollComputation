using Microsoft.AspNetCore.Mvc.Rendering;
using PayrollComputation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollComputation.Services.Interface
{
    public interface IEmployeeService
    {
        Task CreateAsync(Employee newEmployee);
        Employee GetById(string employeeId);
        Task UpdateAsync(Employee employee);
        Task Delete(string employeeId);
        Task UpdateAsync(string employeeId);
        decimal UnionFees(string id);
        decimal StudentLoanRepaymentAmount(string id, decimal totalAmount);
        IEnumerable<Employee> GetAll();
        IEnumerable<SelectListItem> GetAllEmployeesForPayroll();

    }
}
