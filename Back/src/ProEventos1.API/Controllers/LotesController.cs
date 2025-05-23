﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos1.Application.Contratos;
using ProEventos1.Application.Dtos;


namespace ProEventos1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {  
        private readonly ILoteService _loteService;

        public LotesController(ILoteService LoteService)
        {                             
            _loteService = LoteService;            
        } 

        [HttpGet ("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {            
            try
            {
                 var lotes = await _loteService.GetLotesByEventoIdAsync(eventoId);
                 if (lotes == null) return NoContent();


                 return Ok(lotes);
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"[ERRO!] Erro ao tentar recuperar lotes. Erro: {ex.Message}");
            }            
        } 

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> SaveLotes(int eventoId, LoteDto[] models)
        {
            try
            {
                 var lotes = await _loteService.SaveLotes(eventoId, models);
                 if (lotes == null) return NoContent();

                 return Ok(lotes);
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"[ERRO!] Erro ao tentar salvar lotes. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> Delete(int eventoId, int loteId)
        {
            try
            {
                 var lote = await _loteService.GetLoteByIdsAsync(eventoId, loteId);
                 if (lote == null) return NoContent();

                return await _loteService.DeleteLote(lote.EventoId, lote.Id) 
                    ? Ok(new { message = "Lote Deletado!"}) 
                    : throw new Exception("[ERRO!] Ocorreu um problema não especificado ao tentar deletar Lote!");                
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"[ERRO!] Erro ao tentar deletar lotes. Erro: {ex.Message}");
            }
        }
    }
}
