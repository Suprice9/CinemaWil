using Domain.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IActorServices
    {
        Task<List<Actor>> GetActors();
        Task MapActorObject(ActorDto actorD);

        Task<string> DeleteActor(int id);

    }
}
