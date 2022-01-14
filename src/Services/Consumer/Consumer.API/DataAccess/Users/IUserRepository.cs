using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consumer.API.DataAccess.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUsersByOrganisationId(int organisationId, int skip, int take);
        int GetUsersCountByOrganisationId(int organisationId);
    }
}
