using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPesquisa.Models;
using ApiPesquisa.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiPesquisa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuscaController : ControllerBase
    {
        private readonly IBuscaService _buscaService;

        public BuscaController(IBuscaService buscaService)
        {
            _buscaService = buscaService;
        }

        [HttpGet("getBusca")]
        public IActionResult Get([FromQuery] string busca)
        {   
            try
            {
                var resultados =  _buscaService.RealizarPesquisa(busca);
                if(resultados == null) return NoContent();

                return Ok(resultados);
            }
            catch (Exception e)
            {               
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Não foi possível retornar os Títulos e Links da busca. Erro: {e.Message}");
            }            
        }
    }
}