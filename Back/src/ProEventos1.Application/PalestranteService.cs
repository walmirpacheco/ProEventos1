using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos1.Application.Contratos;
using ProEventos1.Application.Dtos;
using ProEventos1.Domain;
using ProEventos1.Persistence.Contratos;
using ProEventos1.Persistence.Models;

namespace ProEventos1.Application
{

    public class PalestranteService : IPalestranteService
    {    
        private readonly IPalestrante1Persist _palestrantePersist;
        private readonly IMapper _mapper;
        public PalestranteService(IPalestrante1Persist palestrantePersist,
                             IMapper mapper)
        {  
            _palestrantePersist = palestrantePersist;
            _mapper = mapper;
            
        }
        public async Task<PalestranteDto> AddPalestrantes(int userId, PalestranteAddDto model)
        {            
            try
            {

                var Palestrante = _mapper.Map<Palestrante>(model);
                Palestrante.UserId = userId;

                 _palestrantePersist.Add<Palestrante>(Palestrante);

                 if (await _palestrantePersist.SaveChangesAsync())
                 {
                    var PalestranteRetorno = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, false);

                    return _mapper.Map<PalestranteDto>(PalestranteRetorno);
                 }
                 return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
           
        }

        public async Task<PalestranteDto> UpdatePalestrante(int userId, PalestranteUpdateDto model)
        {            
           try
           {
                var Palestrante = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, false);
                if (Palestrante == null) return null;

                model.Id = Palestrante.Id;
                model.UserId = userId;
                
                _mapper.Map(model, Palestrante);

                _palestrantePersist.Update<Palestrante>(Palestrante);

               if (await _palestrantePersist.SaveChangesAsync())
                {
                    var PalestranteRetorno = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, false);

                    return _mapper.Map<PalestranteDto>(PalestranteRetorno);
                }
                return null;                 
           }
           catch (Exception ex)
           {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<PalestranteDto>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false)
        {
            try
            {
                var Palestrantes = await _palestrantePersist.GetAllPalestrantesAsync(pageParams,  includeEventos); 
                if (Palestrantes == null) return null;     

                 var resultado = _mapper.Map<PageList<PalestranteDto>>(Palestrantes); 

                 resultado.CurrentPage = Palestrantes.CurrentPage; 
                 resultado.TotalPages = Palestrantes.TotalPages;
                 resultado.PageSize = Palestrantes.PageSize;
                 resultado.TotalCount = Palestrantes.TotalCount;

                return resultado;  
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<PalestranteDto> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false)
        {
            try
            {
                var Palestrante = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, includeEventos); 
                if (Palestrante == null) return null; 

                 var resultado = _mapper.Map<PalestranteDto>(Palestrante);                                

                return resultado;  
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
    }
}