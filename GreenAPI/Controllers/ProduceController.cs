using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommonLib.Produce;
using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenAPI.Managers.Interfaces;

namespace GreenAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProduceController : ControllerBase, IProduceService
    {
        private readonly ILogger<ProduceController> _logger;
        private readonly IProduceManager _produceManager;

        public ProduceController(
            ILogger<ProduceController> logger,
            IProduceManager produceManager)
        {
            _logger = logger;
            _produceManager = produceManager;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<ProduceDTO>> GetAll()
        {
            return await Task.Run(() =>
            {
                return _produceManager.GetAll();
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ProduceDTO> Get(int id)
        {
            return await Task.Run(() =>
            {
                return _produceManager.Get(id);
            });
        }

        [HttpPost]
        public async Task<ProduceDTO> Save(ProduceDTO produce)
        {
            return await Task.Run(() =>
            {
                return _produceManager.Save(produce);
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<int> Delete(int id)
        {
            return await Task.Run(() =>
            {
                return _produceManager.Delete(id);
            });
        }
    }
}
