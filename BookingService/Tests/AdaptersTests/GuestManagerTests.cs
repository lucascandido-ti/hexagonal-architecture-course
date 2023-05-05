using Application;
using Application.Guest.DTO;
using Application.Guest.Requests;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace AdaptersTests
{

    class FakeRepo : IGuestRepository
    {
        public Task<int> Create(Guest guest)
        {
            return Task.FromResult(111);
        }

        public Task<Guest> Get(int id)
        {
            throw new System.NotImplementedException();
        }
    }
    public class Tests
    {

        GuestManager guestManager;

        [SetUp]
        public void Setup()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>()))
                    .Returns(Task.FromResult(222));

            guestManager = new GuestManager(fakeRepo.Object);
        }

        [Test]
        public async Task ShouldBeAbleToValidateCreateGuest()
        {
            var guestDTO = new GuestDTO
            {
                Name = "Antonio",
                Surname = "Silva",
                Email = "abcd@email.com",
                IdNumber = "abcd",
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDTO
            };

            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.True(res.Success);
        }
    }
}