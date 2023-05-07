using Application;
using Application.Guest;
using Application.Guest.DTO;
using Application.Guest.Requests;
using Domain.Guest.Entities;
using Domain.Guest.Enums;
using Domain.Guest.Ports;
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

            int expectdId = 222;

            var request = new CreateGuestRequest()
            {
                Data = guestDTO
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>()))
                    .Returns(Task.FromResult(expectdId));

            guestManager = new GuestManager(fakeRepo.Object);

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

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>()))
                    .Returns(Task.FromResult(222));

            guestManager = new GuestManager(fakeRepo.Object);

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

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>()))
                    .Returns(Task.FromResult(222));

            guestManager = new GuestManager(fakeRepo.Object);

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

        [Test]
        public async Task ShouldReturnGuestNotFoundWhenGuestDoesntExist()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            var fakeGuest = new Guest
            {
                Id = 333,
                Name = "Test"
            };

            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Guest?>(null));

            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.GetGuest(333);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.GUEST_NOT_FOUND);
            Assert.AreEqual(res.Message, "No guest record was found with the given Id");

        }

        [Test]
        public async Task ShouldReturnGuestSuccess()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            var fakeGuest = new Guest
            {
                Id = 333,
                Name = "Test",
                DocumentId = new Domain.ValueObjects.PersonId
                {
                    DocumentType = DocumentType.DriveLicence,
                    IdNumber = "123"
                }
            };

            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult((Guest?)fakeGuest));
            
            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.GetGuest(333);

            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.AreEqual(res.Data.Id, fakeGuest.Id);
            Assert.AreEqual(res.Data.Name, fakeGuest.Name);
        }
    }
}