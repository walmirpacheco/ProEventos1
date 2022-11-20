using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos1.Domain;
using ProEventos1.Persistence.Contextos;
using ProEventos1.Persistence.Contratos;

namespace ProEventos1.Persistence
{
    public class LotePersist : ILotePersist
    {  
        private readonly ProEventos1Context _context;
        public LotePersist(ProEventos1Context context)
        {           
            _context = context;       
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int id)
        {
            System.Linq.IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId
                                     && lote.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId);

            return await query.ToArrayAsync();
        }
    }
}