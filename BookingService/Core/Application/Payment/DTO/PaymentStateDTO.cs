
using Application.Payment.Enums;

namespace Application.Payment.DTO
{
    public class PaymentStateDTO
    {
        public PaymentStatus Status { get; set; }

        public string paymentId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Message { get; set; }
    }
}
