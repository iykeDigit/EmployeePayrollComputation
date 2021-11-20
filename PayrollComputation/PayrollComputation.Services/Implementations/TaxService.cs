using PayrollComputation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollComputation.Services.Implementations
{
    public class TaxService : ITaxService
    {
        private decimal taxRate;
        private decimal tax;
        public decimal TaxAmount(decimal totalAmount)
        {
            if(totalAmount <= 1042) 
            {
                //Tax Free Rate
                taxRate = .0m;
            }
            else if(totalAmount > 1042 && totalAmount <= 3125) 
            {
                //Basic tax rate
                taxRate = 0.20m;
                //income tax
                tax = (1042 * .0m) + ((totalAmount - 1042) * taxRate);
            }
            else if(totalAmount > 3125 && totalAmount <= 12500)
            {
                //higher tax rate
                taxRate = .40m;
                //income tax
                tax = (1042 * .0m) + ((3125 - 1045 * .20m)) + ((totalAmount - 3125) * taxRate);
            }
            else if(totalAmount > 12500)
            {
                //Additional tax rate
                taxRate = .45m;
                //Income Tax
                tax = (1042 * .0m) + ((3125 - 1042) * .20m) + ((12500 - 3125) * .40m) + ((totalAmount - 12500) * taxRate);
            }
            return tax;
        }
    }
}
