﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollComputation.Model
{
    public class EmployeeDetailVM
    {
        public string Id { get; set; }
        
        public int EmployeeNo { get; set; }
       
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DOB { get; set; }
        public DateTime ResumptionDate { get; set; }
        public string Designation { get; set; }
        
        public string NationalInsuranceNo { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public StudentLoan  StudentLoan { get; set; }
        public string PhoneNumber { get; set; }
        public UnionMember UnionMember { get; set; }
        
        public string Address { get; set; }
        
        public string City { get; set; }
        
        public string Postcode { get; set; }
        

    }
}
