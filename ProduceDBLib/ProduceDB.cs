using LiteDB;
using System.Linq;
using Microsoft.Extensions.Options;
using ProduceDBLib.Entities;
using System;
using System.Collections.Generic;

namespace ProduceDBLib
{
    public class ProduceDB : IProduceDB
    {
        private string _connection;

        public ProduceDB(IOptionsMonitor<ProduceDBOptions> options)
        {
            try
            {
                _connection = options.Get("ProduceDB").Connection;

                if (_connection == null)
                {
                    ThrowOptionsValidationException();
                }
            }
            catch (OptionsValidationException)
            {
                ThrowOptionsValidationException();
            }
        }

        private OptionsValidationException ThrowOptionsValidationException()
        {
            throw new OptionsValidationException("ProduceDB", typeof(ProduceDBOptions), new List<string>() { "Your Service lacks ProduceDBOptions" });
        }

        public IEnumerable<Produce> Read()
        {
            List<Produce> returnValue = new List<Produce>();

            using (var db = new LiteDatabase(_connection))
            {
                returnValue.AddRange(
                    db.GetCollection<Produce>("produce").FindAll()
                );
            }

            return returnValue;
        }

        public Produce Read(int id)
        {
            Produce returnValue = new Produce();

            using (var db = new LiteDatabase(_connection))
            {
                returnValue = db.GetCollection<Produce>("produce").FindOne(x => x.Id == id);
            }

            return returnValue;
        }

        public int Upsert(Produce produce)
        {
            int returnValue = 0;

            using (var db = new LiteDatabase(_connection))
            {
                ILiteCollection<Produce> col = db.GetCollection<Produce>("produce");

                col.Upsert(produce);

                returnValue = produce.Id;
            }

            return returnValue;
        }

        public int Delete(int id)
        {
            int returnValue = 0;

            using (var db = new LiteDatabase(_connection))
            {
                ILiteCollection<Produce> col = db.GetCollection<Produce>("produce");

                returnValue = col.Delete(id) ? 1 : 0;
            }

            return returnValue;
        }
    }
}
