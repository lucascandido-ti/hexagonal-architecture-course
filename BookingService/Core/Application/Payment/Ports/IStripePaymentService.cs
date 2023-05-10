﻿using Application.Payment.DTO;

namespace Application.Payment.Ports
{
    public interface IStripePaymentService
    {
        Task<PaymentStateDTO> PayWithCreditCard(string paymentIntention);

        Task<PaymentStateDTO> PayWithDebitCard(string paymentIntention);

        Task<PaymentStateDTO> PayWithTransfer(string paymentIntention);
    }
}
