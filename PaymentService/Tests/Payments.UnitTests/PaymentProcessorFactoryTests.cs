
using Application.MercadoPago;
using Application.Payment.Enums;
using Application.Payment;
using Application;
using PaymentsApplication;

namespace Payments.UnitTests
{
    public class PaymentProcessorFactoryTests
    {
        [Test]
        public void ShouldReturn_NotImplementedPaymentProvider_WhenAskingForStripeProvider()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.Stripe);

            Assert.AreEqual(provider.GetType(), typeof(NotImplementedPaymentProvider));
        }

        [Test]
        public void ShouldReturn_MercadoPagoAdapter_Provider()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);

            Assert.AreEqual(provider.GetType(), typeof(MercadoPagoAdapter));
        }

        [Test]
        public async Task ShouldReturnFalse_WhenCapturingPaymentFor_NotImplementedPaymentProvider()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.Stripe);

            var res = await provider.CapturePayment("https://myprovider.com/asdf");

            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.PAYMENT_PROVIDER_NOT_IMPLEMENTED);
            Assert.AreEqual(res.Message, "The selected payment provider is not available at the moment");
        }
    }
}
