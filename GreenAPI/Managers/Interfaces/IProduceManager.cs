using CommonLib.Produce;
using System.Collections.Generic;

namespace GreenAPI.Managers.Interfaces
{
    public interface IProduceManager
    {
        ProduceDTO Get(int id);
        IEnumerable<ProduceDTO> GetAll();
        ProduceDTO Save(ProduceDTO produce);
        int Delete(int id);
    }
}