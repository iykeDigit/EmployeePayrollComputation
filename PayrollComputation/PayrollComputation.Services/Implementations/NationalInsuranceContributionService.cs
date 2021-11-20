using PayrollComputation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollComputation.Services.Implementations
{
    public class NationalInsuranceContributionService : INationalInsuranceContributionService
    {
        private decimal NIRate;
        private decimal NIC;

        public decimal NIContribution(decimal totalAmount)
        {
            if(totalAmount < 719)
            {
                //Lower earning limit rate 
                //& below primary threshold
                NIRate = .0m;
                NIC = 0m;
            }
            else if(totalAmount >= 719 && totalAmount <= 4167) 
            {
                //Between Primary threshold and upper earnings limit
                NIRate = .12m;
                NIC = ((totalAmount - 719) * NIRate);
            }
            else if(totalAmount > 4167) 
            {
                //Above upper earnings limit
                NIRate = .02m;
                NIC = ((4167 - 719) * .12m) + ((totalAmount - 4167) * NIRate);
            }

            return NIC;
        }
    }
}
