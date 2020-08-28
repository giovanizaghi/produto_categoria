using Dominio.Context;
using Dominio.Dto.Produto;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Citel.Application
{
    public class ProdutoApplication
    {
        private readonly DataModel db;

        public ProdutoApplication(DataModel _db)
        {
            db = _db;
        }

        public async Task<List<Produto>> ObterProdutos()
        {
            return await db.Produto.ToListAsync();
        }

        public async Task<Produto> ObterProdutoPorId(int id)
        {
            return await db.Produto.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<bool> EditarProduto(ProdutoPutDto dto)
        {
            var obj = await db.Produto.Where(x => x.Id == dto.Id).FirstOrDefaultAsync();

            if (obj != null)
            {
                obj.Nome = dto.Nome;
                obj.Descricao = dto.Nome;
                obj.Preco = dto.Preco;
                obj.PrecoAnterior = dto.PrecoAnterior;
                obj.IdCategoria = dto.IdCategoria;

                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ExcluirProduto(int id)
        {
            var obj = await db.Produto.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (obj != null)
            {
                db.Remove(obj);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AdicionarProduto(ProdutoPostDto dto)
        {
            var objAdd = new Produto
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Preco = dto.Preco,
                PrecoAnterior = dto.PrecoAnterior,
                IdCategoria = dto.IdCategoria
            };

            var addedObj = await db.AddAsync(objAdd);

            if (addedObj != null)
            {
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
