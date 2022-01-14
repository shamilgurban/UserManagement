using AutoMapper;
using Consumer.API.DataAccess.Users;
using Consumer.API.Mapper;
using Consumer.API.Models;
using Consumer.API.Models.Dtos.User;
using Consumer.API.Services.UserService.Commands;
using Consumer.API.Services.UserService.Queries;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer.Test.Services.UserServiceTests
{
    public class UserServiceTest
    {
        [Test]
        public async Task UserCreateCommandTest()
        {
            var userRepository = new Mock<IUserRepository>();
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger>();
            UserCreateCommandHandler userCreateCommandHandler = new UserCreateCommandHandler(userRepository.Object, mapper.Object, logger.Object);

            await userCreateCommandHandler.Handle(new UserCreateCommand
            {
                Name = "Capitan",
                Surname = "Price",
                Email = "capitanprice@test.com",
                PhoneNumber = "12345"
            }, It.IsAny<CancellationToken>());
        }

        [Test]
        public async Task UserOrganisationBindCommandTest()
        {
            var userRepository = new Mock<IUserRepository>();
            var logger = new Mock<ILogger>();

            userRepository.Setup(m => m.GetById(It.IsAny<int>()).Result).Returns(new User
            {
                Id = 1,
                Name = "Jack",
                Surname = "Williams",
                FatherName = "John",
                PhoneNumber = "123456789",
                Email = "jack.williams@email.com",
                OrganisationId = 1
            });

            UserOrganisationBindCommandHandler userCreateCommandHandler = new UserOrganisationBindCommandHandler(userRepository.Object, logger.Object);
            await userCreateCommandHandler.Handle(new UserOrganisationBindCommand
            {
                OrganisationId = 1,
                UserId = 1
            }, It.IsAny<CancellationToken>());
        }

        [Test]
        public async Task UserListByOrganisationIdQueryTest()
        {
            var userRepository = new Mock<IUserRepository>();
            var domainProfile = new DomainProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(domainProfile));
            IMapper mapper = new Mapper(configuration);
            var logger = new Mock<ILogger>();
            UserListByOrganisationIdQueryHandler userListByOrganisationIdQueryHandler = new UserListByOrganisationIdQueryHandler(userRepository.Object, mapper, logger.Object);

            userRepository.Setup(m => m.GetUsersByOrganisationId(1, 0, 2).Result).Returns(new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Jack",
                    Surname = "Williams",
                    FatherName = "John",
                    PhoneNumber = "123456789",
                    Email = "jack.williams@email.com",
                    OrganisationId = 1
                },
                new User
                {
                    Id = 2,
                    Name = "Tommy",
                    Surname = "Vercetty",
                    PhoneNumber = "123456789",
                    Email = "tommy.vercetti@vicecity.com",
                    OrganisationId = 1
                }
            });

            userRepository.Setup(m => m.GetUsersCountByOrganisationId(1)).Returns(2);
            userRepository.Setup(m => m.GetTotalCount()).Returns(10);


            PaginatedList<UserDto> paginatedList = await userListByOrganisationIdQueryHandler.Handle(new UserListByOrganisationIdQuery
            {
                OrganisationId = 1,
                Skip = 0,
                Take = 2
            }, It.IsAny<CancellationToken>());

            Assert.AreEqual(JsonSerializer.Serialize(paginatedList), JsonSerializer.Serialize(new PaginatedList<UserDto>
            {
                FilteredRowCount = 2,
                TotalRowCount = 10,
                Items = new List<UserDto>
                {
                    new UserDto
                    {
                        Id = 1,
                        Name = "Jack",
                        Surname = "Williams",
                        FatherName = "John",
                        PhoneNumber = "123456789",
                        Email = "jack.williams@email.com",
                        OrganisationId = 1
                    },
                    new UserDto
                    {
                        Id = 2,
                        Name = "Tommy",
                        Surname = "Vercetty",
                        PhoneNumber = "123456789",
                        Email = "tommy.vercetti@vicecity.com",
                        OrganisationId = 1
                    }
                }
            }));
        }
    }
}
