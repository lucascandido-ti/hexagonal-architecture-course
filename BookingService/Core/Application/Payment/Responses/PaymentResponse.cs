using Application.Payment.DTO;

namespace Application.Payment.Responses
{
    public class PaymentResponse: Response
    {
        public PaymentStateDTO Data { get; set; }
    }
}
