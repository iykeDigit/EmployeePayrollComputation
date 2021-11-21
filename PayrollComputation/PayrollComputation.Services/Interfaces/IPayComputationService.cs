using Microsoft.AspNetCore.Mvc.Rendering;
using PayrollComputation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollComputation.Services.Interfaces
{
    public interface IPayComputationService
    {
        Task CreateAsync(PaymentRecord paymentRecord);
        PaymentRecord GetById(string id);
        IEnumerable<PaymentRecord> GetAll();
        IEnumerable<SelectListItem> GetAllTaxYear();
        decimal OvertimeHours(decimal hoursWorked, decimal contractualHours);
        decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate);
        decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours);
        decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings);
        decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFees);
        decimal NetPay(decimal totalEarnings, decimal totalDeduction);
        decimal OvertimeRate(decimal hourlyRate);
        TaxYear GetTaxYearById(string id);
    }
}
