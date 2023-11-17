using Domain.Dtos;
using Domain.Interface;
using Domain.Models;
using Domain.Dtos;
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

            

            foreach(var b in billboards){
                var movie = await _dbContext.Movie.FindAsync(b.MoviesId);

                var actor = await _dbContext.Actor.FindAsync(b.ActorsId);

                b.Movie = movie;
                b.Actor = actor;
            }

            return billboards;
        }

        public async Task<string> AddBillboardObject(BillboardDto billboard)
        {

            var movie = await _dbContext.Movie.FindAsync(billboard.MoviesId);

            var actor = await _dbContext.Actor.FindAsync(billboard.ActorsId);

            if (movie != null && actor != null)
            {
                var movieId = new SqlParameter("@movieId", movie.Id);

                var actorId = new SqlParameter("@actorId", actor.Id);
                
                var query = await _dbContext.Database.ExecuteSqlRawAsync("AddMoviesAndActors @movieId,@actorId", movieId, actorId);
                if(query != -1 ) {
                    return "Query no se hizo satisfactoriamente";
                }
                return  "Agregado exitosamente";              
            }
            else
            {
                return "Hubo un error";
            }
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
