using API_Citel.Application;
using Dominio.Dto.Categoria;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Citel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private CategoriaApplication categoriaApplication;

        public CategoriaController(CategoriaApplication _categoriaApplication)
        {
            categoriaApplication = _categoriaApplication;
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult<List<Categoria>>> ObterCategorias()
        {
            try
            {
                return await categoriaApplication.ObterCategorias();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ObterPorId/{id}")]
        public async Task<ActionResult<Categoria>> ObterCategoriaPorId([FromRoute] int id)
        {
            try
            {
                return await categoriaApplication.ObterCategoriaPorId(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Editar")]
        public async Task<ActionResult> EditarCategoria(CategoriaPutDto dto)
        {
            try
            {
                var retorno = await categoriaApplication.EditarCategoria(dto);

                if (retorno) return StatusCode(200, "Categoria alterada com sucesso.");
                else return  StatusCode(500, "Não foi possível alterar o categoria.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Excluir/{id}")]
        public async Task<ActionResult> ExcluirCategoria([FromRoute] int id)
        {
            try
            {
                var retorno = await categoriaApplication.ExcluirCategoria(id);

                if (retorno) return StatusCode(200, "Categoria excluída com sucesso.");
                else return StatusCode(500, "Não foi possível excluir a categoria.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Adicionar/{descricao}")]
        public async Task<ActionResult> AdicionarCategoria([FromRoute] string descricao)
        {
            try
            {
                var retorno = await categoriaApplication.AdicionarCategoria(descricao);

                if (retorno) return StatusCode(200, "Categoria adicionada com sucesso.");
                else return StatusCode(500, "Não foi possível adicionar a categoria.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
