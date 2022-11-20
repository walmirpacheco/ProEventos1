using System.Threading.Tasks;
using ProEventos1.Application.Dtos;
using ProEventos1.Persistence.Models;

namespace ProEventos1.Application.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(int userId, EventoDto model);
        Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model);
        Task<bool> DeleteEvento(int userId, int eventoId);

        Task<PageList<EventoDto>> GetAllEventosAsync(int userId, PageParams pageParams,bool includePalestrantes = false);
        Task<EventoDto> GetEventoByIdAsync(int userId, int EventoId, bool includePalestrantes = false);  
    }
}