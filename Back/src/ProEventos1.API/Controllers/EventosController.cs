using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos1.API.Extensions;
using ProEventos1.Application.Contratos;
using ProEventos1.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using ProEventos1.Persistence.Models;
using ProEventos1.API.Helpers;

namespace ProEventos1.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {  
        private readonly IEventoService _eventoService;
        private readonly IUtil _util;
        private readonly IAccountService _accountService;
        private readonly string _destino = "images";

        public EventosController(IEventoService eventoService, 
                                 IUtil util,
                                 IAccountService accountService)
        {                             
            _util = util;
            _accountService = accountService;
            _eventoService = eventoService;            
        } 

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {            
            try
            {
                 var eventos = await _eventoService.GetAllEventosAsync(User.GetUserId(), pageParams, true);
                 if (eventos == null) return NoContent();

                 Response.AddPagination(eventos.CurrentPage, eventos.PageSize, eventos.TotalCount, eventos.TotalPages);


                 return Ok(eventos);
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"[ERRO!] Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }            
        }
        
        [HttpGet("{id}")]        
        public async Task<IActionResult> GetById(int id)
        {            
            try
            {
                 var evento = await _eventoService.GetEventoByIdAsync(User.GetUserId(), id, true);
                 if (evento == null) return NoContent();

                 return Ok(evento);
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"[ERRO!] Evento por Id não encontrado. Erro: {ex.Message}");
            }
            
        }

        [HttpPost("upload-image/{eventoId}")]
        public async Task<IActionResult> UploadImage(int eventoId)
        {
            try
            {
                 var evento = await _eventoService.GetEventoByIdAsync(User.GetUserId(), eventoId, true);
                 if (evento == null) return NoContent();

                 var file = Request.Form.Files[0];
                 if (file.Length > 0) 
                 {
                    _util.DeleteImage(evento.ImagemURL, _destino);
                    evento.ImagemURL = await _util.SaveImage(file, _destino);
                 }
                 var EventoRetorno = await _eventoService.UpdateEvento(User.GetUserId(), eventoId, evento);

                 return Ok(EventoRetorno);
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"[ERRO!] Erro ao tentar realizar Upload de foto do evento. Erro: {ex.Message}");
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                 var evento = await _eventoService.AddEventos(User.GetUserId(), model);
                 if (evento == null) return NoContent();

                 return Ok(evento);
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"[ERRO!] Erro ao tentar adicionar eventos. Erro: {ex.Message}");
            }            
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDto model)
        {
            try
            {
                 var evento = await _eventoService.UpdateEvento(User.GetUserId(), id, model);
                 if (evento == null) return NoContent();

                 return Ok(evento);
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"[ERRO!] Erro ao tentar atualizar eventos. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                 var evento = await _eventoService.GetEventoByIdAsync(User.GetUserId(), id, true);
                 if (evento == null) return NoContent();

                if( await _eventoService.DeleteEvento(User.GetUserId(), id)) {
                    _util.DeleteImage(evento.ImagemURL, _destino);
                    return Ok(new { message = "Deletado"});                    
                }
                else
                {
                    throw new Exception("[ERRO!] Ocorreu um problema não especificado ao tentar deletar Evento!");                      
                }
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"[ERRO!] Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }
    }
}
