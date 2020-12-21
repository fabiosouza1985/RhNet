using System.Data.Entity;
using RhNetServer.Contexts;
using RhNetServer.Entities.Shared;
using RhNetServer.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace RhNetServer.Repositories.Shared
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
                              Id = x.Id
                          }).ToListAsync();
        }

        public async Task<QuadroModel> Get(RhNetContext rhNetContext, int id)
        {
            QuadroModel quadroModel = await (from x in rhNetContext.Quadros
                                             where x.Id == id
                                             orderby x.Descricao
                                             select new QuadroModel()
                                             {
                                                 Descricao = x.Descricao,
                                                 Id = x.Id
                                             }).FirstOrDefaultAsync();

            if (quadroModel == null) return null;

            quadroModel.Subquadros = await (from x in rhNetContext.Subquadros
                                            where x.Quadro_Id == quadroModel.Id
                                            select new SubquadroModel()
                                            {
                                                Id = x.Id,
                                                Descricao = x.Descricao,
                                                Sigla = x.Sigla
                                            }).ToListAsync();

            quadroModel.Atos_Normativos = await (from x in rhNetContext.Quadros_Atos_Normativos
                                                 from y in rhNetContext.Atos_Normativos.Where(e => e.Id == x.Ato_Normativo_Id)
                                                 from z in rhNetContext.Tipos_de_Ato_Normativo.Where(e => e.Id == y.Tipo_de_Ato_Normativo_Id)
                                            where x.Quadro_Id == quadroModel.Id
                                            select new Ato_NormativoModel()
                                            {
                                                Id = y.Id,
                                                Descricao = y.Descricao,
                                                Ano = y.Ano,
                                                Numero = y.Numero,
                                                Publicacao_Data = y.Publicacao_Data,
                                                Vigencia_Data = y.Vigencia_Data,
                                                Tipo_de_Ato_Normativo_Id = z.Id,
                                                Tipo_de_Ato_Normativo_Descricao = z.Descricao
                                            }).ToListAsync();
            return quadroModel;
        }

        public async Task<object> Add(RhNetContext rhNetContext, QuadroModel quadroModel)
        {
            Quadro quadro = new Quadro()
            {
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

        public async Task<object> Update(RhNetContext rhNetContext, QuadroModel quadroModel)
        {
            Quadro quadro = await rhNetContext.Quadros.FindAsync(quadroModel.Id);

            if (quadro == null)
            {
                return "Quadro não encontrado.";
            }
            quadro.Descricao = quadroModel.Descricao;

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

        public async Task<object> Remove(RhNetContext rhNetContext, QuadroModel quadroModel)
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
