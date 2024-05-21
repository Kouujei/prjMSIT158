using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using prjAjaxDemo.Models;
using System.Security.Cryptography;

namespace prjAjaxDemo.Controllers
{
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;
        private readonly MyDBContext _context;
        public ApiController(ILogger<ApiController> logger, MyDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return Content("Hi", "text/html", System.Text.Encoding.UTF8);
        }
        public IActionResult reContent()
        {
            System.Threading.Thread.Sleep(3000);
            return Content("Hi");
            //return Content("Hi", "text/html");
            //return Content("Hi", "text/plain");
        }
        public IActionResult reJSON()
        {
            var ct = _context.Addresses.Select(x => x.City).Distinct();
            return Json(ct);

        }
        public IActionResult Avatar(int id = 1)
        {
            Member member = _context.Members.Find(id);
            if (member != null)
            {
                byte[] img = member.FileData;
                if (img != null)
                {
                    return File(img, "image/jpeg");
                }
            }
            return NotFound();
        //https://localhost:7298/api/Avatar?id=3 只有id才能用/
        //如果是
        }
        public IActionResult Register(string name, int age)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "路人甲";
            }
            return Content($"{name}您好,{age}歲!!");
        }

        public IActionResult cityone()
        {
            var ct = _context.Addresses.Select(x => x.City).Distinct();
            return Json(ct);
        }
        public IActionResult citytwo(string pick)
        {
            var ct = _context.Addresses.Where(x => x.City==pick);
            var tt = ct.Select(x => x.SiteId).Distinct();
            return Json(tt);
        }
        public IActionResult citythree(string pick)
        {
            var ct = _context.Addresses.Where(x => x.SiteId ==pick);
            var zt = ct.Select(x => x.Road);
            return Json(zt);
        }
    }
}
