using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayrollComputation.Data;
using PayrollComputation.Model;
using PayrollComputation.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollComputation.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _db;
        private decimal studentLoanAmount;
       

        public EmployeeService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Employee newEmployee)
        {
            await _db.Employees.AddAsync(newEmployee);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAll() => _db.Employees.AsNoTracking().
            OrderBy(emp => emp.FullName);

        public Employee GetById(string employeeId) => _db.Employees.Where(e => e.Id == employeeId).FirstOrDefault();

        public async Task Delete(string employeeId) 
        {
            var employee = GetById(employeeId);
            _db.Remove(employee);
            await _db.SaveChangesAsync();
        }

        public decimal StudentLoanRepaymentAmount(string id, decimal totalAmount)
        {
            var employee = GetById(id);
            if(employee.StudentLoan == StudentLoan.Yes && totalAmount > 1750 && totalAmount < 2000)
            {
                studentLoanAmount = 15m;
            }
            else if(employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2000 && totalAmount < 2250)
            {
                studentLoanAmount = 38m;
            }
            else if(employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2250 && totalAmount < 2500)
            {
                studentLoanAmount = 60m;
            }
            else if(employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2500) 
            {
                studentLoanAmount = 83m;
            }
            else 
            {
                studentLoanAmount = 0m;
            }
            return studentLoanAmount;
        }

        public decimal UnionFees(string id)
        {
            var employee = GetById(id);
            var fee = employee.UnionMember == UnionMember.Yes ? 10m : 0m;
            return fee;
            
        }

        public async Task UpdateAsync(Employee employee)
        {
            _db.Update(employee);
           await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(string employeeId)
        {
            var employee = GetById(employeeId);
            _db.Update(employee);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> GetAllEmployeesForPayroll() 
        {
            return GetAll().Select(emp => new SelectListItem()
            {
                Text = emp.FullName,
                Value = emp.Id.ToString()
            });
        }
    }
}
