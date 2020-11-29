using Microsoft.EntityFrameworkCore;
using RhNetAPI.Contexts;
using RhNetAPI.Entities.Shared;
using RhNetAPI.Models.Shared;
using RhNetAPI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Repositories.Shared
{
    public class Tipo_de_Ato_NormativoRepository
    {
        public async Task<List<Tipo_de_Ato_NormativoModel>> Get(RhNetContext rhNetContext)
        {
            return await (from x in rhNetContext.Tipos_de_Ato_Normativo
                          orderby x.Descricao
                          select new Tipo_de_Ato_NormativoModel()
                          {
                              Descricao = x.Descricao,
                              Id = x.Id
                          }).ToListAsync();
        }

        public async Task<Object> Add(RhNetContext rhNetContext, Tipo_de_Ato_NormativoModel tipo_de_ato_normativoModel)
        {
            Tipo_de_Ato_Normativo tipo_de_ato_normativo = new Tipo_de_Ato_Normativo()
            {
                Descricao = tipo_de_ato_normativoModel.Descricao
            };

            try
            {
                rhNetContext.Entry(tipo_de_ato_normativo).State = EntityState.Added;
                await rhNetContext.SaveChangesAsync();
                await rhNetContext.Entry(tipo_de_ato_normativo).ReloadAsync();

                tipo_de_ato_normativoModel.Id = tipo_de_ato_normativo.Id;

                return tipo_de_ato_normativoModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }
        }

        public async Task<Object> Update(RhNetContext rhNetContext, Tipo_de_Ato_NormativoModel tipo_de_ato_normativoModel)
        {
            Tipo_de_Ato_Normativo tipo_de_ato_normativo = await rhNetContext.Tipos_de_Ato_Normativo.FindAsync(tipo_de_ato_normativoModel.Id);

            if (tipo_de_ato_normativo == null)
            {
                return "Tipo de Ato Normativo não encontrado.";
            }
            tipo_de_ato_normativo.Descricao = tipo_de_ato_normativoModel.Descricao;

            try
            {
                rhNetContext.Entry(tipo_de_ato_normativo).State = EntityState.Modified;
                await rhNetContext.SaveChangesAsync();

                return tipo_de_ato_normativoModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }


        }

        public async Task<Object> Remove(RhNetContext rhNetContext, Tipo_de_Ato_NormativoModel tipo_de_ato_normativoModel)
        {
            Tipo_de_Ato_Normativo tipo_de_ato_normativo = await rhNetContext.Tipos_de_Ato_Normativo.FindAsync(tipo_de_ato_normativoModel.Id);

            if (tipo_de_ato_normativo == null)
            {
                return "Tipo de Ato Normativo não encontrado.";
            }

            try
            {
                rhNetContext.Entry(tipo_de_ato_normativo).State = EntityState.Deleted;
                await rhNetContext.SaveChangesAsync();

                return tipo_de_ato_normativoModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }


        }
    }
}
