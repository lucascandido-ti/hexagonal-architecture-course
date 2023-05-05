using Domain.Exceptions;
using Domain.Ports;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string email { get; set; }
        public PersonId DocumentId { get; set; }
        private void ValidateState()
        {
            if(
                DocumentId == null ||
                string.IsNullOrEmpty(DocumentId.IdNumber) ||
                DocumentId.IdNumber.Length <= 3 ||
                DocumentId.DocumentType == 0
              )
            {
                throw new InvalidPersonDocumentIdException();
            }

            if(
                string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(Surname) ||
                string.IsNullOrEmpty(email))
            {
                throw new MissingRequiredInformation();
            }

            if (Utils.Utils.ValidateEmail(this.email) == false)
            {
                throw new InvalidEmailException();
            }
        }

        public async Task Save(IGuestRepository guestRepository)
        {
            this.ValidateState();
            if(this.Id == 0)
            {
                this.Id = await guestRepository.Create(this);
            }
        }
    }
}
