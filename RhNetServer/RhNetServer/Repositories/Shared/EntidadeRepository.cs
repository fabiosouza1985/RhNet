using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using RhNetServer.Contexts;
using RhNetServer.Entities.Shared;
using RhNetServer.Models.Shared;
using System.Data.Entity.Infrastructure;

namespace RhNetServer.Repositories.Shared
{
    public class EntidadeRepository
    {
        public async Task<List<EntidadeModel>> Get(RhNetContext rhNetContext)
        {
            return await (from x in rhNetContext.Entidades
                          from y in rhNetContext.Municipios.Where(e => e.Id == x.Municipio_Id).DefaultIfEmpty()
                          orderby x.Descricao
                          select new EntidadeModel()
                          {
                              Descricao = x.Descricao,
                              Codigo_Audesp = x.Codigo_Audesp,
                              Id = x.Id,
                              Municipio_Id = y.Id,
                              Municipio_Descricao = y.Descricao
                          }).ToListAsync();
        }

        public async Task<object> Add(RhNetContext rhNetContext, EntidadeModel entidadeModel)
        {
            Entidade entidade = new Entidade()
            {
                Codigo_Audesp = entidadeModel.Codigo_Audesp,
                Descricao = entidadeModel.Descricao
            };

            if (entidadeModel.Municipio_Id.HasValue)
            {
                Municipio municipio = await rhNetContext.Municipios.FindAsync(entidadeModel.Municipio_Id);

                if (municipio != null)
                {
                    entidade.Municipio = municipio;
                    entidadeModel.Municipio_Descricao = municipio.Descricao;
                }
            }
            try
            {
                rhNetContext.Entry(entidade).State = EntityState.Added;
                await rhNetContext.SaveChangesAsync();
                await rhNetContext.Entry(entidade).ReloadAsync();

                entidadeModel.Id = entidade.Id;

                return entidadeModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }
        }

        public async Task<object> Update(RhNetContext rhNetContext, EntidadeModel entidadeModel)
        {
            Entidade entidade = await rhNetContext.Entidades.FindAsync(entidadeModel.Id);

            if (entidade == null)
            {
                return "Entidade não encontrada.";
            }
            entidade.Descricao = entidadeModel.Descricao;
            entidade.Codigo_Audesp = entidadeModel.Codigo_Audesp;

            if (entidadeModel.Municipio_Id.HasValue)
            {
                Municipio municipio = await rhNetContext.Municipios.FindAsync(entidadeModel.Municipio_Id);

                if (municipio != null)
                {
                    entidade.Municipio = municipio;
                    entidadeModel.Municipio_Descricao = municipio.Descricao;
                }
            }
            else
            {
                entidade.Municipio = null;
                entidadeModel.Municipio_Descricao = "";
            }

            try
            {
                rhNetContext.Entry(entidade).State = EntityState.Modified;
                await rhNetContext.SaveChangesAsync();

                return entidadeModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }


        }

        public async Task<object> Remove(RhNetContext rhNetContext, EntidadeModel entidadeModel)
        {
            Entidade entidade = await rhNetContext.Entidades.FindAsync(entidadeModel.Id);

            if (entidade == null)
            {
                return "Entidade não encontrada.";
            }

            try
            {
                rhNetContext.Entry(entidade).State = EntityState.Deleted;
                await rhNetContext.SaveChangesAsync();

                return entidadeModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }


        }
    }
}
