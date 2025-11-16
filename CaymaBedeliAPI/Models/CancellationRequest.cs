namespace CaymaBedeliAPI.Models
{
    public class CancellationRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal InstallationFee { get; set; } = 0;
        public decimal ActivationFee { get; set; } = 0;
        public decimal ModemFee { get; set; } = 0;
    }
}
