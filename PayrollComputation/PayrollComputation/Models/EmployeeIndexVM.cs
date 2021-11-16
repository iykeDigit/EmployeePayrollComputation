using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollComputation.UI.Models
{
    public class EmployeeIndexVM
    {
        public string Id { get; set; }
        public int EmployeeNo { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ResumptionDate { get; set; }
        public string Designation { get; set; }
        public string City { get; set; }
    }
}
