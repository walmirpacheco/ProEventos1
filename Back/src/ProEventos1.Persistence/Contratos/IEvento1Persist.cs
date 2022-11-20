using System.Threading.Tasks;
using ProEventos1.Domain;
using ProEventos1.Persistence.Models;

namespace ProEventos1.Persistence.Contratos
{
    public interface IEvento1Persist
    {
        Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false);
    }
}