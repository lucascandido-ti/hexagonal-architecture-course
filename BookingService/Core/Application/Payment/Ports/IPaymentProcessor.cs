
using Application.Payment.Responses;

namespace Application.Payment.Ports
{
    public interface IPaymentProcessor
    {
        Task<PaymentResponse> CapturePayment(string paymentIntention);
    }
}
