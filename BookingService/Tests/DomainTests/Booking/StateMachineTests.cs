using Domain.Entities;
using Domain.Guest.Enums;
using Action = Domain.Guest.Enums.Action;

namespace DomainTests.Bookings
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldAlwaysStartWithCreatedStatus()
        {
            var booking = new Booking();
            Assert.AreEqual(booking.Status, Status.Created);
        }

        [Test]
        public void ShouldSetStatusToPaidWhenPaingForABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            Assert.AreEqual(booking.Status, Status.Paid);
        }

        [Test]
        public void ShouldSetStatusToCancelWhenCancelingABokkingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Cancel);
            Assert.AreEqual(booking.Status, Status.Canceled);
        }

        [Test]
        public void ShouldSetStatusToReopenWhenCancel()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Reopen);
            Assert.AreEqual(booking.Status, Status.Created);
        }

        [Test]
        public void ShouldSetStatusToFinishedWhenFinishingAPaidBooking()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            Assert.AreEqual(booking.Status, Status.Paid);

            booking.ChangeState(Action.Finish);
            Assert.AreEqual(booking.Status, Status.Finished);
        }

        [Test]
        public void ShouldNotChangeStatusWhenRefoundingAFinishedBookin()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);
            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.Status, Status.Finished);

        }
    }
}