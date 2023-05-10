using Application.MercadoPago.Exceptions;
using Application.Payment.DTO;
using Application.Payment.Enums;
using Application.Payment.Ports;
using Application.Payment.Responses;

namespace Application.MercadoPago
{
    public class MercadoPagoAdapter : IMercadoPagoPaymentService
    {
        public Task<PaymentResponse> PayWithCreditCard(string paymentIntention)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(paymentIntention))
                {
                    throw new InvalidPaymentIntentionException();
                }

                paymentIntention = "/success";

                var dto = new PaymentStateDTO
                {
                    CreatedDate = DateTime.Now,
                    Message = $"Successfully paid {paymentIntention}",
                    paymentId = "123",
                    Status = PaymentStatus.Success
                };

                var response = new PaymentResponse
                {
                    Data = dto,
                    Success = true,
                    Message = "Payment successfully processed"
                };

                return Task.FromResult(response);
            }
            catch (InvalidPaymentIntentionException)
            {
                var response = new PaymentResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PAYMENT_INVALID_PEYMENT_INTENTION
                };

                return Task.FromResult(response);
            }
        }

        public Task<PaymentResponse> PayWithDebitCard(string paymentIntention)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentResponse> PayWithTransfer(string paymentIntention)
        {
            throw new NotImplementedException();
        }
    }
}
