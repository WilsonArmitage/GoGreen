using CommonLib.Produce;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonLib.Interfaces
{
    public interface IProduceService
    {
        Task<ProduceDTO> Get(int id);
        Task<IEnumerable<ProduceDTO>> GetAll();
        Task<ProduceDTO> Save(ProduceDTO produce);
        Task<int> Delete(int id);
    }
}