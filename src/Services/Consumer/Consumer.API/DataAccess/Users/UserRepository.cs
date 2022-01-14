using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.API.DataAccess.Users
{
    public class UserRepository : IUserRepository
    {
        private DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<User> GetById(int id)
        {
            return await dataContext.Users.FindAsync(id);
        }

        public int GetTotalCount()
        {
            return dataContext.Users.Count();
        }

        public async Task<IEnumerable<User>> GetUsersByOrganisationId(int organisationId, int skip, int take)
        {
            return await dataContext.Users.Where(user => user.OrganisationId == organisationId)
                .Include(property => property.Organisation).Skip(skip).Take(take).ToListAsync();
        }

        public int GetUsersCountByOrganisationId(int organisationId)
        {
            return dataContext.Users.Count(user => user.OrganisationId == organisationId);
        }

        public async Task Insert(User user)
        {
            await dataContext.Users.AddAsync(user);
        }

        public async Task Update(User user)
        {
            dataContext.Users.Attach(user);
            dataContext.Entry(user).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await dataContext.SaveChangesAsync();
        }
    }
}
