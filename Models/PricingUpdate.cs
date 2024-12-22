namespace Services.MortgagePricing.Models
{
    public class PricingUpdate
    {
        public required string ApplicationId { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}