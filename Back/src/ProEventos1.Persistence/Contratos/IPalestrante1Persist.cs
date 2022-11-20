using System.Threading.Tasks;
using ProEventos1.Domain;
using ProEventos1.Persistence.Models;

namespace ProEventos1.Persistence.Contratos
{
    public interface IPalestrante1Persist : IGeralPersist
    {       
        Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false);
        Task<Palestrante> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false);
    }
}