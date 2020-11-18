using RhNetAPI.Contexts;
using RhNetAPI.Models.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RhNetAPI.Entities.Adm;

namespace RhNetAPI.Repositories.Adm
{
    public class ClientRepository
    {

        public async Task<List<ClientModel>> GetAllClients(RhNetContext context)
        {

            return await (from x in context.Clients
                          select new ClientModel()
                          {
                             Situation = x.Situation,
                             Cnpj = x.Cnpj,
                             Description = x.Description,
                             Id = x.Id
                          }).ToListAsync();
            ;

        }

        public async Task<ClientModel> AddClient(ClientModel clientModel, RhNetContext context)
        {

            Client client = new Client()
            {
                Description = clientModel.Description,
                Cnpj = clientModel.Cnpj,
                Situation = clientModel.Situation
            };

            context.Entry(client).State = EntityState.Added;
            await context.SaveChangesAsync();
            await context.Entry(client).ReloadAsync();

            clientModel.Id = client.Id;

            return clientModel;

        }

        public async Task<ClientModel> UpdateClient(ClientModel clientModel, RhNetContext context)
        {
            Client client = await context.Clients.FindAsync(clientModel.Id);

            client.Description = clientModel.Description;
            client.Cnpj = clientModel.Cnpj;
            client.Situation = clientModel.Situation;

            context.Entry(client).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return clientModel;

        }

        public async Task<ClientModel> RemoveClient(ClientModel clientModel, RhNetContext context)
        {
            Client client = await context.Clients.FindAsync(clientModel.Id);
            
            context.Entry(client).State = EntityState.Deleted;
            await context.SaveChangesAsync();

            return clientModel;

        }
    }
}
