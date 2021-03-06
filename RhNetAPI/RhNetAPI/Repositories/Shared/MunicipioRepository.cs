﻿using Microsoft.EntityFrameworkCore;
using RhNetAPI.Contexts;
using RhNetAPI.Entities.Shared;
using RhNetAPI.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Repositories.Shared
{
    public class MunicipioRepository
    {        
        public async Task<List<MunicipioModel>> Get(RhNetContext rhNetContext)
        {
            return await (from x in rhNetContext.Municipios
                          orderby x.Descricao
                          select new MunicipioModel()
                          {
                              Descricao = x.Descricao,
                              Codigo_Audesp = x.Codigo_Audesp,
                              Id = x.Id
                          }).ToListAsync();
        }

        public async Task<Object> Add(RhNetContext rhNetContext, MunicipioModel municipioModel)
        {
            Municipio municipio = new Municipio()
            {
                Codigo_Audesp = municipioModel.Codigo_Audesp,
                Descricao = municipioModel.Descricao
            };

            try
            {
                rhNetContext.Entry(municipio).State = EntityState.Added;
                await rhNetContext.SaveChangesAsync();
                await rhNetContext.Entry(municipio).ReloadAsync();

                municipioModel.Id = municipio.Id;

                return municipioModel;
            }
            catch(DbUpdateException ex)           
            {
                return ex.InnerException.ToString();

            }
        }

        public async Task<Object> Update(RhNetContext rhNetContext, MunicipioModel municipioModel)
        {
            Municipio municipio = await rhNetContext.Municipios.FindAsync(municipioModel.Id);

            if (municipio == null)
            {
                return "Município não encontrado.";
            }
            municipio.Descricao = municipioModel.Descricao;
            municipio.Codigo_Audesp = municipioModel.Codigo_Audesp;

            try
            {
                rhNetContext.Entry(municipio).State = EntityState.Modified;
                await rhNetContext.SaveChangesAsync();

                return municipioModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }
          

        }

        public async Task<Object> Remove(RhNetContext rhNetContext, MunicipioModel municipioModel)
        {
            Municipio municipio = await rhNetContext.Municipios.FindAsync(municipioModel.Id);

            if (municipio == null)
            {
                return "Município não encontrado.";
            }
            
            try
            {
                rhNetContext.Entry(municipio).State = EntityState.Deleted;
                await rhNetContext.SaveChangesAsync();

                return municipioModel;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.ToString();

            }
           

        }
    }
}
