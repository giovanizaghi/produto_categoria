using API_Citel.Application;
using Dominio.Dto.Produto;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Citel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private ProdutoApplication produtoApplication;

        public ProdutoController(ProdutoApplication _produtoApplication)
        {
            produtoApplication = _produtoApplication;
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult<List<Produto>>> ObterProdutos()
        {
            try
            {
                return await produtoApplication.ObterProdutos();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ObterPorId/{id}")]
        public async Task<ActionResult<Produto>> ObterProdutoPorId([FromRoute] int id)
        {
            try
            {
                return await produtoApplication.ObterProdutoPorId(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<bool>> EditarProduto([FromBody] ProdutoPutDto dto)
        {
            try
            {
                var retorno = await produtoApplication.EditarProduto(dto);

                if (retorno) return StatusCode(200, "Produto alterado com sucesso.");
                else return StatusCode(500, "Não foi possível alterar o produto");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Excluir/{id}")]
        public async Task<ActionResult<bool>> ExcluirProduto([FromRoute] int id)
        {
            try
            {
                var retorno = await produtoApplication.ExcluirProduto(id);

                if (retorno) return StatusCode(200, "Produto excluído com sucesso.");
                else return StatusCode(500, "Não foi possível excluir o produto");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Adicionar")]
        public async Task<ActionResult<bool>> AdicionarProduto([FromBody] ProdutoPostDto produto)
        {
            try
            {
                var retorno = await produtoApplication.AdicionarProduto(produto);

                if (retorno) return StatusCode(200, "Produto adicionado com sucesso.");
                else return StatusCode(500, "Não foi possível adicionar o produto");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
