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
    public class SubquadroRepository
    {
        public async Task<List<SubquadroModel>> Get(RhNetContext rhNetContext)
        {
            return await (from x in rhNetContext.Subquadros
                          from y in rhNetContext.Quadros.Where(e=> e.Id == x.Quadro_Id)
                          orderby x.Descricao
                          select new SubquadroModel()
                          {
                              Descricao = x.Descricao,
                              Id = x.Id,
                              Sigla = x.Sigla,
                              Quadro = new QuadroModel()
                              {
                                  Id = y.Id,
                                  Descricao = y.Descricao,
                                  Sigla = y.Sigla
                              }
                          }).ToListAsync();
        }

        public async Task<Object> Add(RhNetContext rhNetContext, SubquadroModel subquadroModel)
        {
            if (subquadroModel.Quadro == null)
            {
                return "Quadro não informado.";
            }


            Quadro quadro = await rhNetContext.Quadros.FindAsync(subquadroModel.Quadro.Id);

            if (quadro == null)
            {
                return "Quadro não encontrado.";
            }

            Subquadro subquadro = new Subquadro()
            {
                Sigla = subquadroModel.Sigla,
                Descricao = subquadroModel.Descricao,
                Quadro = quadro
            };

            try
            {
                rhNetContext.Entry(subquadro).State = EntityState.Added;
                await rhNetContext.SaveChangesAsync();
                await rhNetContext.Entry(subquadro).ReloadAsync();

                subquadroModel.Id = subquadro.Id;
                
                return subquadroModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }
        }

        public async Task<Object> Update(RhNetContext rhNetContext, SubquadroModel subquadroModel)
        {

            if (subquadroModel.Quadro == null)
            {
                return "Quadro não informado.";
            }


            Quadro quadro = await rhNetContext.Quadros.FindAsync(subquadroModel.Quadro.Id);

            if (quadro == null)
            {
                return "Quadro não encontrado.";
            }

            Subquadro subquadro = await rhNetContext.Subquadros.FindAsync(subquadroModel.Id);

            if (subquadro == null)
            {
                return "Subquadro não encontrado.";
            }
            subquadro.Descricao = subquadroModel.Descricao;
            subquadro.Sigla = subquadroModel.Sigla;
            subquadro.Quadro = quadro;

            try
            {
                rhNetContext.Entry(subquadro).State = EntityState.Modified;
                await rhNetContext.SaveChangesAsync();

                return subquadroModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }


        }

        public async Task<Object> Remove(RhNetContext rhNetContext, SubquadroModel subquadroModel)
        {
            Subquadro subquadro = await rhNetContext.Subquadros.FindAsync(subquadroModel.Id);

            if (subquadro == null)
            {
                return "Subquadro não encontrado.";
            }

            try
            {
                rhNetContext.Entry(subquadro).State = EntityState.Deleted;
                await rhNetContext.SaveChangesAsync();

                return subquadroModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }


        }
    }
}
