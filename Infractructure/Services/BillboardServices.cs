using Domain.Dtos;
using Domain.Interface;
using Domain.Models;
using Infractructure.Data;
using Mapster;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        public async Task<int>AddBillboardObject(BillboardDto billboard)
        {
            //revisar
            var movieId = await _dbContext.Movie.FindAsync(billboard.MoviesId);

            var actorId = await  _dbContext.Actor.FindAsync(billboard.ActorsId);

            var movie = new SqlParameter("@movieId", movieId.Id);

            var actor = new SqlParameter("@actorId", actorId.Id);
            
            var data=await _dbContext.Database.ExecuteSqlRawAsync("AddMoviesAndActors @movieId,@actorId",movie,actor);

            return data;

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
