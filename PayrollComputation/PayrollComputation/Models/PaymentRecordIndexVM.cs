using PayrollComputation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollComputation.UI.Models
{
    public class PaymentRecordIndexVM
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Display(Name = "Pay Date")]
        public DateTime PayDate { get; set; }
        [Display(Name = "Month")]
        public string PayMonth { get; set; }
        public string TaxCode { get; set; }
        public string TaxYearId { get; set; }
        public decimal HourlyRate { get; set; }
        public string Year { get; set; }
        [Display(Name = "Total Earnings")]
        public decimal TotalEarnings { get; set; }
        [Display(Name = "Total Deductions")]
        public decimal TotalDeduction { get; set; }
        [Display(Name = "Net")]
        public decimal NetPayment { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal ContractualHours { get; set; }
    }
}
