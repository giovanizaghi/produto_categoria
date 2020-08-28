using Dominio.Context;
using Dominio.Dto.Categoria;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Citel.Application
{
    public class CategoriaApplication
    {
        private readonly DataModel db;

        public CategoriaApplication(DataModel _db)
        {
            db = _db;
        }

        public async Task<List<Categoria>> ObterCategorias()
        {
            return await db.Categoria.ToListAsync();
        }

        public async Task<Categoria> ObterCategoriaPorId(int id)
        {
            return await db.Categoria.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<bool> EditarCategoria(CategoriaPutDto dto)
        {
            var obj = await db.Categoria.Where(x => x.Id == dto.Id).FirstOrDefaultAsync();

            if (obj != null)
            {
                obj.Descricao = dto.Descricao;
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ExcluirCategoria(int id)
        {
            var obj = await db.Categoria.Where(x => x.Id == id).FirstOrDefaultAsync();

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

        public async Task<bool> AdicionarCategoria(string descricao)
        {
            var objAdd = new Categoria();

            objAdd.Descricao = descricao;
            await db.AddAsync(objAdd);

            if (objAdd != null)
            {
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            };
        }
    }
}
