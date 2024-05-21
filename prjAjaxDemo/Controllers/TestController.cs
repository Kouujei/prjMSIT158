using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using prjAjaxDemo.Models;
using System.Composition.Hosting.Core;

namespace prjAjaxDemo.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly MyDBContext _context;
        public TestController(ILogger<TestController> logger, MyDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Try()
        //{
        //    //function printSomething (data) {
        //    //    const promise = new Promise(function(resolve, reject){ setTimeout(function(){ resolve(data) }, 1000); })
        //    //return View(promise);
        //    //}

        //}
        public IActionResult show()
        {
            var show = _context.Categories;
            return View(show);

        }
        public IActionResult First()
        {
            
            return View();

        }
		public IActionResult Address()
		{

			return View();

		}
        public IActionResult Register()
        {

            return View();

        }
    }
}
