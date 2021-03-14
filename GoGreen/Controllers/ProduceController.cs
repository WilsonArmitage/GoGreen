using CommonLib.Produce;
using CommonLib.Services;
using GoGreen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GoGreen.Controllers
{
    public class ProduceController : Controller
    {
        private readonly ILogger<ProduceController> _logger;

        private readonly GreenAPIService _greenApiService;

        public ProduceController(
            ILogger<ProduceController> logger,
            GreenAPIService greenApiService
        )
        {
            _logger = logger;
            _greenApiService = greenApiService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ProduceDTO> produce = await _greenApiService.GetAll();

            return View("Index", produce);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("ManageProduce", new ProduceViewModel());
        }

        public async Task<IActionResult> Edit(int id)
        {
            ProduceDTO produce = await _greenApiService.Get(id);

            return View("ManageProduce", new ProduceViewModel()
            {
                Id = produce.Id,
                Name = produce.Name,
                Price = produce.Price,
                Stock = produce.Stock,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProduceViewModel model)
        {
            if(ModelState.IsValid)
            {
                ProduceDTO response = await _greenApiService.Save(new ProduceDTO()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Price = model.Price.GetValueOrDefault(0),
                    Stock = model.Stock.GetValueOrDefault(0),
                });

                model.Id = response.Id;

                if (model.Id == 0)
                {
                    AddNotification("danger", "Save Failed!");
                }
                else
                {
                    AddNotification("success", "Save Succeeded!");
                }
            }

            return View("ManageProduce", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            int delete = await _greenApiService.Delete(id);

            if (delete == 0)
            {
                AddNotification("danger", "Delete Failed!");
            }
            else
            {
                AddNotification("success", "Delete Succeeded!");
            }

            return await Index();
        }

        private void AddNotification(string status, string message)
        {
            if(ViewBag.Notifications == null)
            {
                ViewBag.Notifications = new List<NotificationModel>();
            }

            ViewBag.Notifications.Add(new NotificationModel()
            {
                Status = status,
                Message = message,
            });
        }
    }
}
