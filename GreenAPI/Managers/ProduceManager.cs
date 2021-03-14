using CommonLib.Produce;
using GreenAPI.Managers.Interfaces;
using ProduceDBLib;
using ProduceDBLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenAPI.Managers
{
    public class ProduceManager : IProduceManager
    {
        private IProduceDB _produceDB;

        public ProduceManager(IProduceDB produceDB)
        {
            _produceDB = produceDB;
        }

        public IEnumerable<ProduceDTO> GetAll()
        {
            return _produceDB.Read()
                .Select(x => EntityToDto(x));
        }

        public ProduceDTO Get(int id)
        {
            ProduceDTO returnValue = new ProduceDTO();

            Produce produce = _produceDB.Read(id);

            if (produce != null)
            {
                returnValue.Id = produce.Id;
                returnValue.Name = produce.Name;
                returnValue.Price = produce.Price;
                returnValue.Stock = produce.Stock;
            }

            return returnValue;
        }

        public ProduceDTO Save(ProduceDTO produce)
        {
            int saved = _produceDB.Upsert(DtoToEntity(produce));

            produce.Id = saved;

            return produce;
        }

        public int Delete(int id)
        {
            return _produceDB.Delete(id);
        }

        private Produce DtoToEntity(ProduceDTO dto)
        {
            return new Produce()
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
            };
        }

        private ProduceDTO EntityToDto(Produce entity)
        {
            return new ProduceDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Stock = entity.Stock,
            };
        }
    }
}
