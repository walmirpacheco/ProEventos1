using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos1.Domain.Identity;
using ProEventos1.Persistence.Contextos;
using ProEventos1.Persistence.Contratos;

namespace ProEventos1.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly ProEventos1Context _context;
        public UserPersist(ProEventos1Context context) : base(context)
        {
            _context = context;            
        }
        public async Task<IEnumerable<User>> GetUserAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }        
        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
                                 .SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
        }
    }
}