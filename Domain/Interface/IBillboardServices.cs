using Domain.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IBillboardServices
    {
        Task<List<Billboard>> GetBillboard();
        Task MapBillboardObject(BillboardDto billboardD);
        Task<string> DeleteBillboard(int id);
    }
}
