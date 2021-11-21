using Microsoft.AspNetCore.Mvc;
using PayrollComputation.Model;
using PayrollComputation.Services.Interface;
using PayrollComputation.Services.Interfaces;
using PayrollComputation.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollComputation.UI.Controllers
{
    public class PayController : Controller
    {
        private readonly IPayComputation _payComputationService;
        private IEmployeeService _employeeService;
        private decimal overtimeHrs;
        private decimal contractualEarnings;
        private decimal overtimeEarnings;
        private ITaxService _taxService;
        private decimal totalEarnings;
        private INationalInsuranceContributionService _nationalInsuranceContributionService;
        private decimal tax;
        private decimal unionFee;
        private decimal studentLoan;
        private decimal nationalInsurance;
        private decimal totalDeduction;

        public PayController(IPayComputation payComputationService, IEmployeeService employeeService, 
            ITaxService taxService, INationalInsuranceContributionService nationalInsuranceContributionService)
        {
            _payComputationService = payComputationService;
            _employeeService = employeeService;
            _taxService = taxService;
            _nationalInsuranceContributionService = nationalInsuranceContributionService;
        }
        public IActionResult Index()
        {
            var payRecords = _payComputationService.GetAll().Select(pay => new PaymentRecordIndexVM
            {
                Id = pay.Id,
                EmployeeId = pay.EmployeeId,
                FullName = pay.FullName,
                PayDate = pay.PayDate,
                PayMonth = pay.PayMonth,
                TaxYearId = pay.TaxYearId,
                Year = _payComputationService.GetTaxYearById(pay.TaxYearId).YearofTax,
                TotalEarnings = pay.TotalEarnings,
                NetPayment = pay.NetPayment,
                Employee = pay.Employee
            });
            return View(payRecords);
        }

        public IActionResult Create() 
        {
            ViewBag.employees = _employeeService.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payComputationService.GetAllTaxYear();
            var model = new PaymentRecordIndexVM();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentRecordIndexVM model) 
        {
            if (ModelState.IsValid) 
            {
                var payRecord = new PaymentRecord()
                {
                    Id = model.Id,
                    EmployeeId = model.EmployeeId,
                    FullName = _employeeService.GetById(model.EmployeeId).FullName,
                    NiNo = _employeeService.GetById(model.EmployeeId).NationalInsuranceNo,
                    PayDate = model.PayDate,
                    PayMonth = model.PayMonth,
                    TaxYearId = model.TaxYearId,
                    TaxCode = model.TaxCode,
                    HourlyRate = model.HourlyRate,
                    HoursWorked = model.HoursWorked,
                    ContractualHours = model.ContractualHours,
                    OvertimeHours = overtimeHrs = _payComputationService.OvertimeHours(model.HoursWorked, model.ContractualHours),
                    ContractualEarnings = contractualEarnings = _payComputationService.ContractualEarnings(model.ContractualHours, model.HoursWorked, model.HourlyRate),
                    OvertimeEarnings = overtimeEarnings = _payComputationService.OvertimeEarnings(_payComputationService.OvertimeRate(model.HourlyRate), overtimeHrs),
                    TotalEarnings = totalEarnings = _payComputationService.TotalEarnings(overtimeEarnings, contractualEarnings),
                    Tax = tax = _taxService.TaxAmount(totalEarnings),
                    UnionFee = unionFee = _employeeService.UnionFees(model.EmployeeId),
                    SLC = studentLoan = _employeeService.StudentLoanRepaymentAmount(model.EmployeeId, totalEarnings),
                    NIC = nationalInsurance = _nationalInsuranceContributionService.NIContribution(totalEarnings),
                    TotalDeduction = totalDeduction = _payComputationService.TotalDeduction(tax, nationalInsurance, studentLoan, unionFee),
                    NetPayment = _payComputationService.NetPay(totalEarnings, totalDeduction)
                };
                _payComputationService.CreateAsync(payRecord);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.employees = _employeeService.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payComputationService.GetAllTaxYear();
            return View();
        }
    }
}
