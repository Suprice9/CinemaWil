using Domain.Dtos;
using Domain.Interface;
using Domain.Models;
using Infractructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infractructure.Services
{
    public class BillboardServices : IBillboardServices
    {

        private readonly DataBaseContext _dbContext;

        public BillboardServices(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Billboard>> GetBillboard()
        {
            var billboards = await _dbContext.Billboard.ToListAsync();

            return billboards;
        }

        public async Task MapBillboardObject(BillboardDto billboardD)
        {
            var result = billboardD.Adapt<Billboard>();
            await _dbContext.Billboard.AddAsync(result);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> DeleteBillboard(int id)
        {
            var billboard = await _dbContext.Billboard.FindAsync(id);

            if (billboard != null)
            {
                _dbContext.Remove(billboard);
                await _dbContext.SaveChangesAsync();
                return "Se elimino correctamente";
            }
            else
            {
                return "Hubo un error";
            }
        }

    }
}
