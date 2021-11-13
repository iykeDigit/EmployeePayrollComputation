using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollComputation.Model
{
    public class Employee
    {
        public string Id { get; set; }
        [Required]
        public int EmployeeNo { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DOB { get; set; }
        public DateTime ResumptionDate { get; set; }
        public string Designation { get; set; }
        [Required, MaxLength(50)]
        public string NationalInsuranceNo { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public StudentLoan  StudentLoan { get; set; }
        public UnionMember UnionMember { get; set; }
        [Required, MaxLength(150)]
        public string Address { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(50)]
        public string Postcode { get; set; }
        public IEnumerable<PaymentRecord> PaymentRecords { get; set; }

    }
}
