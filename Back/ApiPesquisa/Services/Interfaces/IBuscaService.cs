using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPesquisa.Models;

namespace ApiPesquisa.Services.Interfaces
{
    public interface IBuscaService
    {
        List<Resultado> RealizarPesquisa(string busca);
    }
}