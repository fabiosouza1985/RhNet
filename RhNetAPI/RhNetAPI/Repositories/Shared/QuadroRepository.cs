using Microsoft.EntityFrameworkCore;
using RhNetAPI.Contexts;
using RhNetAPI.Entities.Shared;
using RhNetAPI.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Repositories.Shared
{
    public class QuadroRepository
    {
        public async Task<List<QuadroModel>> Get(RhNetContext rhNetContext)
        {
            return await (from x in rhNetContext.Quadros
                          orderby x.Descricao
                          select new QuadroModel()
                          {
                              Descricao = x.Descricao,
                              Id = x.Id,
                              Sigla = x.Sigla
                          }).ToListAsync();
        }

        public async Task<Object> Add(RhNetContext rhNetContext, QuadroModel quadroModel)
        {
            Quadro quadro = new Quadro()
            {
                Sigla = quadroModel.Sigla,
                Descricao = quadroModel.Descricao
            };

            try
            {
                rhNetContext.Entry(quadro).State = EntityState.Added;
                await rhNetContext.SaveChangesAsync();
                await rhNetContext.Entry(quadro).ReloadAsync();

                quadroModel.Id = quadro.Id;

                return quadroModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }
        }

        public async Task<Object> Update(RhNetContext rhNetContext, QuadroModel quadroModel)
        {
            Quadro quadro = await rhNetContext.Quadros.FindAsync(quadroModel.Id);

            if (quadro == null)
            {
                return "Quadro não encontrado.";
            }
            quadro.Descricao = quadroModel.Descricao;
            quadro.Sigla = quadroModel.Sigla;

            try
            {
                rhNetContext.Entry(quadro).State = EntityState.Modified;
                await rhNetContext.SaveChangesAsync();

                return quadroModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }


        }

        public async Task<Object> Remove(RhNetContext rhNetContext, QuadroModel quadroModel)
        {
            Quadro quadro = await rhNetContext.Quadros.FindAsync(quadroModel.Id);

            if (quadro == null)
            {
                return "Quadro não encontrado.";
            }

            try
            {
                rhNetContext.Entry(quadro).State = EntityState.Deleted;
                await rhNetContext.SaveChangesAsync();

                return quadroModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }


        }
    }
}
