using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos1.Domain.Identity;

namespace ProEventos1.Persistence.Contratos
{
    public interface IUserPersist : IGeralPersist
    {
        Task<IEnumerable<User>> GetUserAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserNameAsync(string userName);
    }
}