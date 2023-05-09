using Domain.Room.Exceptions;
using Domain.Room.Ports;
using Domain.Room.ValueObjects;

namespace Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public Price Price { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public bool isAvalible
        {
            get
            {
                if (InMaintenance || HasGuest)
                {
                    return false;
                }
                return true;
            }
        }

        public bool HasGuest
        {
            get
            {
                return true;
            }
        }

        public bool IsValid()
        {
            this.ValidateState();
            return true;
        }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                throw new InvalidRoomDataException();
            }

            if (this.Price == null || this.Price.Value < 10)
            {
                throw new InvalidRoomPriceException();
            }
        }

        public bool CanBeBooked()
        {
            try
            {
                ValidateState();
            }
            catch (Exception)
            {

                return false;
            }

            if (!isAvalible)
            {
                return false;
            }

            return true;
        }

        public async Task Save(IRoomRepository roomRepository)
        {
            ValidateState();
            if (Id == 0)
            {
                Id = await roomRepository.Create(this);
            }
        }

    }
}
