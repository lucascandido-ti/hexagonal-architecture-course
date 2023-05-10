using Application.Payment.DTO;
using Application.Payment.Responses;

namespace Application.Payment.Ports
{
    public interface IMercadoPagoPaymentService
    {
        Task<PaymentResponse> PayWithCreditCard(string paymentIntention);

        Task<PaymentResponse> PayWithDebitCard(string paymentIntention);

        Task<PaymentResponse> PayWithTransfer(string paymentIntention);
    }
}
