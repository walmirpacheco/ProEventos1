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

    public class EventoService : IEventoService
    {    
        private readonly IGeralPersist _geralPersist;
        private readonly IEvento1Persist _eventoPersist;
        private readonly IMapper _mapper;
        public EventoService(IGeralPersist geralPersist, 
                             IEvento1Persist eventoPersist,
                             IMapper mapper)
        {           
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;
            _mapper = mapper;
            
        }
        public async Task<EventoDto> AddEventos(int userId, EventoDto model)
        {            
            try
            {

                var evento = _mapper.Map<Evento>(model);
                evento.UserId = userId;

                 _geralPersist.Add<Evento>(evento);

                 if (await _geralPersist.SaveChangesAsync())
                 {
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);

                    return _mapper.Map<EventoDto>(eventoRetorno);
                 }
                 return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
           
        }

        public async Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model)
        {            
           try
           {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;
                model.UserId = userId;
                
                _mapper.Map(model, evento);

                _geralPersist.Update<Evento>(evento);

               if (await _geralPersist.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);

                    return _mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;                 
           }
           catch (Exception ex)
           {
                
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteEvento(int userId, int eventoId)
        {            
            try
            {
                 var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);
                 if (evento == null) throw new Exception("[ERRO!] Evento para delete não encontrado!");

                 _geralPersist.Delete<Evento>(evento);
                 return await _geralPersist.SaveChangesAsync();
                                
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<EventoDto>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(userId, pageParams,  includePalestrantes); 
                if (eventos == null) return null;     

                 var resultado = _mapper.Map<PageList<EventoDto>>(eventos); 

                 resultado.CurrentPage = eventos.CurrentPage; 
                 resultado.TotalPages = eventos.TotalPages;
                 resultado.PageSize = eventos.PageSize;
                 resultado.TotalCount = eventos.TotalCount;

                return resultado;  
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, includePalestrantes); 
                if (evento == null) return null; 

                 var resultado = _mapper.Map<EventoDto>(evento);                                

                return resultado;  
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
    }
}