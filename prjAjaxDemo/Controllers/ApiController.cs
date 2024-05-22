using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using prjAjaxDemo.Models;
using prjAjaxDemo.Models.DTO;
using System.Security.Cryptography;

namespace prjAjaxDemo.Controllers
{
    public class ApiController : Controller
    {

        private readonly MyDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment; //依賴注入Dependency Injection
        public ApiController(MyDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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

        public IActionResult City()
        {
            var ct = _context.Addresses.Select(x => x.City).Distinct();
            return Json(ct);
        }
        public IActionResult District(string city)
        {
            var ct = _context.Addresses.Where(x => x.City== city).Select(x => x.SiteId).Distinct();
            return Json(ct);
        }
        public IActionResult Road(string district)
        {
            var ct = _context.Addresses.Where(x => x.SiteId == district).Select(x => x.Road);;
            return Json(ct);
        }
        //public IActionResult Register(MemberDTO member)
        //{
        //    if (string.IsNullOrEmpty(member.userName))
        //    {
        //        member.userName = "guest";
        //    }
        //    return Content($"Hello {member.userName}，{member.Age} 歲了，電子郵件是 {member.Email}", "text/html", System.Text.Encoding.UTF8);
        //    //if (string.IsNullOrEmpty(name))
        //    //{
        //    //    name = "路人甲";
        //    //}
        //    //return Content($"{name}您好,{age}歲!!");
        //}
        //public IActionResult Avatar(int id = 1)
        //{
        //    Member member = _context.Members.Find(id);
        //    if (member != null)
        //    {
        //        byte[] img = member.FileData;
        //        if (img != null)
        //        {
        //            return File(img, "image/jpeg");
        //        }
        //    }
        //    return NotFound();
        //    //https://localhost:7298/api/Avatar?id=3 只有id才能用/
        //    //如果是
        //}
        public IActionResult Avatar(int id = 1)
        {

                Member? member = _context.Members.Find(id);
                if (member != null)
                {
                    byte[] img = member.FileData;
                    if (img != null)
                    {
                        return File(img, "image/jpeg");
                    }

                }
                return NotFound();
            
        }

        public IActionResult Register(Member member, IFormFile avatar)
        {
            if (string.IsNullOrEmpty(member.Name))
            {
                member.Name = "guest";
            }

            //取得上傳檔案的資訊
            //string info = $"{avatar.FileName} - {avatar.Length} - {avatar.ContentType}";
            //string info = _hostEnvironment.ContentRootPath;

            //檔案上傳寫進資料夾
            //todo1 判斷檔案是否存在
            //todo2 限制上傳檔案的大小跟類型 

            //實際路徑
            //string uploadPath = @"C:\Users\User\Documents\workspace\MSIT158Site\wwwroot\uploads\a.png";
            //WebRootPath：C: \Users\User\Documents\workspace\MSIT158Site\wwwroot
            //ContentRootPath：C:\Users\User\Documents\workspace\MSIT158Site
            string uploadPath = Path.Combine(_hostEnvironment.WebRootPath, "img", avatar.FileName);
            string info = uploadPath;
            using (var fileStream = new FileStream(uploadPath, FileMode.Create))
            {
                avatar.CopyTo(fileStream);
            }

            //檔案上傳轉成二進位
            byte[] imgByte = null;
            using (var memoryStream = new MemoryStream())
            {
                avatar.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }

            //寫進資料庫
            member.FileName = avatar.FileName;
            member.FileData = imgByte;
            _context.Members.Add(member);
            _context.SaveChanges();

            return Content(info, "text/plain", System.Text.Encoding.UTF8);
            // return Content($"Hello {member.Name}，{member.Age} 歲了，電子郵件是 {member.Email}", "text/html", System.Text.Encoding.UTF8);
        }
        public IActionResult Spots([FromBody] SearchDTO search)
        {

            return Json(search);
        }

    }
}
