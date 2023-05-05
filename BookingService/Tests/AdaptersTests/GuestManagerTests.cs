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


        [TestCase("")]
        [TestCase(null)]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        public async Task ShouldReturnInvalidPersonExceptionWhenDocsAreInvalid(string docNumber)
        {
            var guestDTO = new GuestDTO
            {
                Name = "Antonio",
                Surname = "Silva",
                Email = "abcd@email.com",
                IdNumber = docNumber,
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDTO
            };

            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.False(res.Success);

            Assert.AreEqual(res.ErrorCode, ErrorCodes.INVALID_PERSON_ID);
            Assert.AreEqual(res.Message, "The ID passed is not valid");
        }

        [TestCase("","surname","abc@email.com")]
        [TestCase(null,"surname","abc@email.com")]
        
        [TestCase("name","","abc@email.com")]
        [TestCase("name",null,"abc@email.com")]
        
        [TestCase("name","surname","")]
        [TestCase("name","surname",null)]
        public async Task ShouldReturnMissingRequiredInformation(string name, string surname, string email)
        {
            var guestDTO = new GuestDTO
            {
                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDTO
            };

            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.False(res.Success);

            Assert.AreEqual(res.ErrorCode, ErrorCodes.MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "Missing required information passed");
        }

        
        [TestCase("invalid")]
        public async Task ShouldReturnInvalidEmailPassed(string email)
        {
            var guestDTO = new GuestDTO
            {
                Name = "Fulano",
                Surname = "Ciclano",
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDTO
            };

            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.False(res.Success);

            Assert.AreEqual(res.ErrorCode, ErrorCodes.INVALID_EMAIL);
            Assert.AreEqual(res.Message, "The given email is not valid");
        }
    }
}