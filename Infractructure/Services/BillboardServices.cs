using Domain.Dtos;
using Domain.Interface;
using Domain.Models;
using Infractructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public async Task AddBillboardObject(BillboardDto billboard)
        {
            //revisar
            var movieId = await _dbContext.Movie.FindAsync(billboard.MoviesId);

            var actorId = await _dbContext.Actor.FindAsync(billboard.ActorsId);

            _dbContext.Billboard.FromSqlRaw($"AddMoviesAndActors {movieId}, {actorId}");

        }

        public async Task<string> UpdateBillboard(int e, BillboardDto billboard)
        {
            var billboardDb = await _dbContext.Billboard.FindAsync(e);
            if (billboardDb != null)
            {
                billboardDb.ActorsId = billboard.ActorsId;
                billboardDb.MoviesId = billboard.MoviesId;

                _dbContext.Update(billboardDb);

                await _dbContext.SaveChangesAsync();

                return "Actualizado exitosamente";
            }
            else
            {
                return "Cartelera no encontrada";
            }
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
