
using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Guest.Enums;
using Action = Domain.Guest.Enums.Action;

namespace Domain.Entities
{
    public class Booking
    {

        public Booking()
        {
            Status = Status.Created;
            PlacedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Room Room { get; set; }
        public Guest Guest { get; set; }
        private Status Status { get; set; }
        public Status CurrentStatus { get { return Status; } }

        public void ChangeState(Action action)
        {
            Status = (Status, action) switch
            {
                (Status.Created, Action.Pay) => Status.Paid,
                (Status.Created, Action.Cancel) => Status.Canceled,
                (Status.Paid, Action.Finish) => Status.Finished,
                (Status.Paid, Action.Refound) => Status.Refounded,
                (Status.Canceled, Action.Reopen) => Status.Created,
                _ => Status
            };
        }

        public bool IsValid()
        {
            try
            {
                this.ValidateState();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        private void ValidateState()
        {
            if(this.PlacedAt == null)
            {
                throw new PlaceAtRequiredInformationException();
            }
            
            if(this.Start == null)
            {
                throw new StartDateRequiredException();
            }

            if(this.End == null)
            {
                throw new EndDateRequiredException();
            }

            if(this.Room == null)
            {
                throw new RoomRequiredException();
            }

            if(this.Guest == null)
            {
                throw new GuestRequiredException();
            }

            this.Room.IsValid();
            this.Guest.IsValid();
        }

        public async Task Save(IBookingRepository bookingRepository)
        {
            this.ValidateState();

            if(this.Id == 0) {
                var resp = await bookingRepository.CreateBooking(this);
                this.Id = resp.Id;
            }
        }
    }
}
