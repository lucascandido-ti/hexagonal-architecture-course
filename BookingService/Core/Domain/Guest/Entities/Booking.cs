using Domain.Guest.Enums;
using Action = Domain.Guest.Enums.Action;

using RoomEntities = Domain.Room.Entities;

namespace Domain.Guest.Entities
{
    public class Booking
    {

        public Booking()
        {
            Status = Status.Created;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public RoomEntities.Room Room { get; set; }
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
    }
}
