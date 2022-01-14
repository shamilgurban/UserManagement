using AutoMapper;
using Consumer.API.DataAccess.Users;
using Consumer.API.Models;
using Consumer.API.Models.Dtos.User;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer.API.Services.UserService.Queries
{
    public class UserListByOrganisationIdQueryHandler : IRequestHandler<UserListByOrganisationIdQuery, PaginatedList<UserDto>>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public UserListByOrganisationIdQueryHandler(IUserRepository userRepository, IMapper mapper, ILogger logger)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<PaginatedList<UserDto>> Handle(UserListByOrganisationIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<User> users = await userRepository.GetUsersByOrganisationId(request.OrganisationId, request.Skip, request.Take);
                int filteredRowCount = userRepository.GetUsersCountByOrganisationId(request.OrganisationId);
                int totalRowCount = userRepository.GetTotalCount();

                PaginatedList<UserDto> paginatedList = new PaginatedList<UserDto>
                {
                    Items = mapper.Map<List<UserDto>>(users),
                    FilteredRowCount = filteredRowCount,
                    TotalRowCount = totalRowCount,
                };

                return paginatedList;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception("Error happened while getting users by organisation");
            }
        }
    }
}
