using ProduceDBLib.Entities;
using System.Collections.Generic;

namespace ProduceDBLib
{
    public interface IProduceDB
    {
        IEnumerable<Produce> Read();
        Produce Read(int id);
        int Upsert(Produce produce);
        int Delete(int id);
    }
}