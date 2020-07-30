using Application.Models.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class BrochureController : Controller
    {
        private readonly Service _service;

        public BrochureController(Service service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.Get());
        }
    }
}
