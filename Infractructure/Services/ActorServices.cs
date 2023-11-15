﻿using Domain.Dtos;
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
    public class ActorServices : IActorServices
    {
        private readonly DataBaseContext _dbContext;

        public ActorServices(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Actor>> GetActors()
        {
            var actors = await _dbContext.Actor.ToListAsync();

            return actors;
        }

        public async Task MapActorObject(ActorDto actorD)
        {
            var result = actorD.Adapt<Actor>();
            await _dbContext.Actor.AddAsync(result);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> DeleteActor(int id)
        {
            var actor = await _dbContext.Actor.FindAsync(id);

            if (actor != null)
            {
                _dbContext.Remove(actor);
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
