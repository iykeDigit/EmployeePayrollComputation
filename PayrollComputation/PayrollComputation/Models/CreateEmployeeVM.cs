using Microsoft.AspNetCore.Http;
using PayrollComputation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollComputation.UI.Models
{
    public class CreateEmployeeVM
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Employee Number is requred"),
            RegularExpression("^[A-Z]{3,3}[0-9]{3}$")]
        public int EmployeeNo { get; set; }
        [Required(ErrorMessage ="First Name is required"), StringLength(50, MinimumLength =2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(50), Display(Name ="Middle Name")]
        public string MiddleName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + (string.IsNullOrEmpty(MiddleName) ? " " : (" " + (char?)MiddleName[0] + ".").ToUpper())
                    + LastName;
            }
        }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Gender { get; set; }

        [Display(Name ="Photo")]
        public IFormFile ImageUrl { get; set; }

        [DataType(DataType.Date), Display(Name="Date of Birth")]
        public DateTime DOB { get; set; }
        [DataType(DataType.Date), Display(Name = "Resumption Date")]
        public DateTime ResumptionDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage ="Job Role is required"), StringLength(100)]
        public string Designation { get; set; }
        [Required, StringLength(50), Display(Name = "NI No.")]
        [RegularExpression(@"^[A-CEGHJ-PR-TW-Z]{1}[A-CEHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$")]
        public string NationalInsuranceNo { get; set; }
        [Display(Name ="Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name ="Student Loan")]
        public StudentLoan StudentLoan { get; set; }
        [Display(Name = "Union Member")]
        public UnionMember UnionMember { get; set; }
        [Required, MaxLength(150)]
        public string Address { get; set; }
        [Required, StringLength(50)]
        public string City { get; set; }
        [Required, StringLength(50)]
        public string Postcode { get; set; }
        public string PhoneNumber { get; set; }

    }
}
