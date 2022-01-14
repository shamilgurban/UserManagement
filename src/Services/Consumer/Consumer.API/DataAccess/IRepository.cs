using System.Threading.Tasks;

namespace Consumer.API.DataAccess
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);
        int GetTotalCount();
        Task Insert(T obj);
        Task Update(T obj);
        Task Save();
    }
}
