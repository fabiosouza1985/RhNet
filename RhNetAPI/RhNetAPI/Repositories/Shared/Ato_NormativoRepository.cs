using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RhNetAPI.Contexts;
using RhNetAPI.Entities.Shared;
using RhNetAPI.Models.Shared;

namespace RhNetAPI.Repositories.Shared
{
    public class Ato_NormativoRepository
    {
        public async Task<List<Ato_NormativoModel>> Get(RhNetContext rhNetContext)
        {
            return await (from x in rhNetContext.Atos_Normativos
                          from y in rhNetContext.Tipos_de_Ato_Normativo.Where(e => e.Id == x.Tipo_de_Ato_Normativo_Id)
                          orderby x.Descricao
                          select new Ato_NormativoModel()
                          {
                              Descricao = x.Descricao,
                              Id = x.Id,
                              Tipo_de_Ato_Normativo_Id = y.Id,
                              Tipo_de_Ato_Normativo_Descricao = y.Descricao,
                              Ano = x.Ano,
                              Numero = x.Numero,
                              Publicacao_Data = x.Publicacao_Data,
                              Vigencia_Data = x.Vigencia_Data
                          }).ToListAsync();
        }

        public async Task<Object> Add(RhNetContext rhNetContext, Ato_NormativoModel ato_normativoModel)
        {
            Ato_Normativo ato_normativo = new Ato_Normativo()
            {                
                Descricao = ato_normativoModel.Descricao,
                Vigencia_Data = ato_normativoModel.Vigencia_Data,
                Publicacao_Data = ato_normativoModel.Publicacao_Data,
                Numero = ato_normativoModel.Numero,
                Ano = ato_normativoModel.Ano                
            };

            Tipo_de_Ato_Normativo tipo_de_ato_normativo = await rhNetContext.Tipos_de_Ato_Normativo.FindAsync(ato_normativoModel.Tipo_de_Ato_Normativo_Id);

            if (tipo_de_ato_normativo != null)
            {
                ato_normativo.Tipo_de_Ato_Normativo = tipo_de_ato_normativo;
                ato_normativoModel.Tipo_de_Ato_Normativo_Descricao = tipo_de_ato_normativo.Descricao;
            }
            else
            {
                return "Tipo de Ato Normativo não encontrado.";
            }
            try
            {
                rhNetContext.Entry(ato_normativo).State = EntityState.Added;
                await rhNetContext.SaveChangesAsync();
                await rhNetContext.Entry(ato_normativo).ReloadAsync();

                ato_normativoModel.Id = ato_normativo.Id;

                return ato_normativoModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }
        }

        public async Task<Object> Update(RhNetContext rhNetContext, Ato_NormativoModel ato_normativoModel)
        {
            Ato_Normativo ato_normativo = await rhNetContext.Atos_Normativos.FindAsync(ato_normativoModel.Id);

            if (ato_normativo == null)
            {
                return "Ato Normativo não encontrado.";
            }
            ato_normativo.Descricao = ato_normativoModel.Descricao;
            ato_normativo.Vigencia_Data = ato_normativoModel.Vigencia_Data;
            ato_normativo.Publicacao_Data = ato_normativoModel.Publicacao_Data;
            ato_normativo.Numero = ato_normativoModel.Numero;
            ato_normativo.Ano = ato_normativoModel.Ano;

            Tipo_de_Ato_Normativo tipo_de_ato_normativo = await rhNetContext.Tipos_de_Ato_Normativo.FindAsync(ato_normativoModel.Tipo_de_Ato_Normativo_Id);

            if (tipo_de_ato_normativo != null)
            {
                ato_normativo.Tipo_de_Ato_Normativo = tipo_de_ato_normativo;
                ato_normativoModel.Tipo_de_Ato_Normativo_Descricao = tipo_de_ato_normativo.Descricao;
            }
            else
            {
                return "Tipo de Ato Normativo não encontrado.";
            }

            try
            {
                rhNetContext.Entry(ato_normativo).State = EntityState.Modified;
                await rhNetContext.SaveChangesAsync();

                return ato_normativoModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }


        }

        public async Task<Object> Remove(RhNetContext rhNetContext, Ato_NormativoModel ato_normativoModel)
        {
            Ato_Normativo ato_normativo = await rhNetContext.Atos_Normativos.FindAsync(ato_normativoModel.Id);

            if (ato_normativo == null)
            {
                return "Ato Normativo não encontrado.";
            }

            try
            {
                rhNetContext.Entry(ato_normativo).State = EntityState.Deleted;
                await rhNetContext.SaveChangesAsync();

                return ato_normativoModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }


        }
    }
}
