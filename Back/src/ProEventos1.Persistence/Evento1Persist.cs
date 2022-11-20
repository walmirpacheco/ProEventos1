using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos1.Domain;
using ProEventos1.Persistence.Contextos;
using ProEventos1.Persistence.Contratos;
using ProEventos1.Persistence.Models;

namespace ProEventos1.Persistence
{
    public class Evento1Persist : IEvento1Persist
    {  
        private readonly ProEventos1Context _context;
        public Evento1Persist(ProEventos1Context context)
        {           
            _context = context;  
           // _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;      
        } 

        public async Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (includePalestrantes) 
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }             

            query = query.AsNoTracking()
                         .Where(e => (e.Tema.ToLower().Contains(pageParams.Term.ToLower()) || e.Local.ToLower().Contains(pageParams.Term.ToLower())) && e.UserId == userId)
                         .OrderBy(e => e.Id);

            return await PageList<Evento>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (includePalestrantes) 
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }             

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == eventoId && 
                                     e.UserId == userId);

            return await query.FirstOrDefaultAsync();
        } 
    }
}