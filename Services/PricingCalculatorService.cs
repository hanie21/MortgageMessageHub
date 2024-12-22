using System;
using Services.MortgagePricing.Models;

namespace MortgagePricingService.Services
{
    public class PricingCalculatorService
    {
        public PricingUpdate CalculatePricing(PricingUpdate update)
        {
            update.InterestRate = update.LoanAmount > 500000 ? 3.5m : 2.5m; // Example logic
            return update;
        }
    }
}

