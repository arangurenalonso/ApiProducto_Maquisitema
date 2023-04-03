using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.ApisExternas
{
    public interface IMockapiRepository
    {
        Task<Discont?> GetDiscontByProductIdAsync(int ProductId);
        Task<Discont?> SaveDiscontByProductIdAsync(Discont discont);
        Task<Discont?> UpdateDiscontByProductIdAsync(Discont discont,int id);
    }
}
