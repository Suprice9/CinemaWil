using System;
using  

namespace Domain.Interface.Web
{
    interface IActorControllerServices
    {
        Task<List<ActorViewModel>> GetActors();
    }
}
