using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using PayrollComputation.Model;
using PayrollComputation.Services.Interface;
using PayrollComputation.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollComputation.UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment hostingEnvironment)
        {
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(int? pageNumber)
        {
            var employees = _employeeService.GetAll();
            List<EmployeeIndexVM> list = new List<EmployeeIndexVM>();

            foreach(var employee in employees) 
            {
                var obj = new EmployeeIndexVM();
                obj.Id = employee.Id;
                obj.EmployeeNo = employee.EmployeeNo;
                obj.ImageUrl = employee.ImageUrl;
                obj.FullName = employee.FullName;
                obj.Gender = employee.Gender;
                obj.Designation = employee.Designation;
                obj.City = employee.City;
                obj.ResumptionDate = employee.ResumptionDate;

                list.Add(obj);
            }



            //var employees = _employeeService.GetAll().Select(employee => new EmployeeDetailVM 
            //{
            //    Id = employee.Id,
            //    EmployeeNo = employee.EmployeeNo,
            //    ImageUrl = employee.ImageUrl,
            //    FullName = employee.FullName,
            //    Gender = employee.Gender,
            //    Designation = employee.Designation,
            //    City = employee.City,
            //    ResumptionDate = employee.ResumptionDate

            //}).ToList();

            int pageSize = 4;

            return View(EmployeeListPagination<EmployeeIndexVM>.Create(list, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public IActionResult Create() 
        {
            var model = new CreateEmployeeVM();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create(CreateEmployeeVM model) 
        {
            if (ModelState.IsValid) 
            {
                var employee = new Employee
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Email = model.Email,
                    DOB = model.DOB,
                    Designation = model.Designation,
                    ResumptionDate = model.ResumptionDate,
                    NationalInsuranceNo = model.NationalInsuranceNo,
                    PaymentMethod = model.PaymentMethod,
                    StudentLoan = model.StudentLoan,
                    UnionMember = model.UnionMember,
                    Address = model.Address,
                    City = model.City,
                    PhoneNumber = model.PhoneNumber,
                    Postcode = model.Postcode,
                };

                if(model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDirectory = @"images/employee/";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = Guid.NewGuid() + fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDirectory, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDirectory + "" + fileName;
                }

                await _employeeService.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
            
        }

        [HttpGet]
        public IActionResult Edit(string id) 
        {
            var employee = _employeeService.GetById(id);
            if(employee == null) 
            {
                return NotFound();
            }

            var model = new EditEmployeeVM()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                Email = employee.Email,
                DOB = employee.DOB,
                ResumptionDate = employee.ResumptionDate,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.Address,
                City = employee.City,
                PhoneNumber = employee.PhoneNumber,
                Postcode = employee.Postcode,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEmployeeVM model) 
        {
            if (ModelState.IsValid) 
            {
                var employee = _employeeService.GetById(model.Id);
                if (employee == null) 
                {
                    return NotFound();
                }
                employee.EmployeeNo = model.EmployeeNo;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.MiddleName = model.MiddleName;
                employee.NationalInsuranceNo = model.NationalInsuranceNo;
                employee.Gender = model.Gender;
                employee.Email = model.Email;
                employee.DOB = model.DOB;
                employee.ResumptionDate = model.ResumptionDate;
                employee.Designation = model.Designation;
                employee.PaymentMethod = model.PaymentMethod;
                employee.StudentLoan = model.StudentLoan;
                employee.UnionMember = model.UnionMember;
                employee.Address = model.Address;
                employee.City = model.City;
                employee.Postcode = model.Postcode;

                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDirectory = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = Guid.NewGuid() + fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDirectory, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDirectory + "" + fileName;
                }

                await _employeeService.UpdateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult EmployeeDetail(string id) 
        {
            var employee = _employeeService.GetById(id);
            if(employee == null) 
            {
                return NotFound();
            }
            EmployeeDetailVM model = new EmployeeDetailVM() 
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                Gender = employee.Gender,
                DOB = employee.DOB,
                ResumptionDate = employee.ResumptionDate,
                Designation = employee.Designation,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.City,
                ImageUrl = employee.ImageUrl,
                Postcode = employee.Postcode
                };
            return View(model);
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteEmployee(string id) 
        {
            var employee = _employeeService.GetById(id);
            if(employee == null) 
            {
                return NotFound();
            }

            var model = new DeleteEmployeeVM()
            {
                Id = employee.Id,
                FullName = employee.FullName
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployee(DeleteEmployeeVM model) 
        {
            await _employeeService.Delete(model.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
