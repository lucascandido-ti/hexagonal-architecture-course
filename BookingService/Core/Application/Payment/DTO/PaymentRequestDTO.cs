
using Application.Payment.Enums;

namespace Application.Payment.DTO
{
    
    public class PaymentRequestDTO
    {
        public int BookingId { get; set; }
        public string PaymentIntention { get; set; }
        public SupportedPaymentProviders SelectedPaymentProvider { get; set; }
        public SupportedPaymentMethods SelectedPaymentMethod { get; set; }
    }
}
