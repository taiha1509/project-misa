using Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class PresidentController : Controller
    {
        public IActionResult Index()
        {
            ViewData.Model = ("Donald Trump", new DateTime(1980, 10, 10), "fuck boy");
            var view = new ViewResult();

            view.ViewData = ViewData;

            return view;
        }

        [Route("sayHello")]
        public IActionResult AttributeRouting()
        {
            ViewData.Model = ("Donald Trump", new DateTime(1980, 10, 10), "fuck boy");
            var view = new ViewResult();

            view.ViewData = ViewData;

            return view;
        }
    }

    public  class AppController : Controller
    {
        public IActionResult CreateNewBook(Book book)
        {
            return Content($"{book.Title} by {book.Author}, {book.Publisher} in {book.Year}");
        }

        public IActionResult Form()
        {
            return View("/Views/President/Form.cshtml");
        }

    }
}


