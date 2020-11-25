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
    public class MunicipioRepository
    {
        public List<Property> GetProperties()
        {
            return  Property.GetProperties(typeof(MunicipioModel));
        }
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
    }
}
