using Microsoft.AspNetCore.Mvc;
using ProjectSem3.IService;
using ProjectSem3.Models;

namespace ProjectSem3.Controllers;
public class ArtWorkController : Controller
{
    private IArtworkService _artService;
    private IAccountStudentService _accountStudentService;
    public ArtWorkController(IArtworkService artService, IAccountStudentService accountStudentService)
    {
        _artService = artService;
        _accountStudentService = accountStudentService;
    }
    public IActionResult ShowArtWork(Student model)
    {

        if (HttpContext.Session.GetString("LoggedInUser") == null)
        {
            ViewBag.checklogin = "";
        }
        else
        {
            string? emaillogined = HttpContext.Session.GetString("LoggedInUser");
            ViewBag.checklogin = emaillogined;
            if (emaillogined != null)
            {
                ViewBag.stulogined = _accountStudentService.getmemberbyusername(emaillogined);
            }
        }
        ViewBag.stulogin = HttpContext.Session.GetString("LoggedInUser");
        ViewBag.art = _artService.GetlistArt();
        
        return View();
    }
    //[Route("~/")]
    public IActionResult ShowAddArt()
    {
        var mess = TempData["Mess"] as string;
        if (mess == "")
        {
            ViewBag.Mess = "";

        }
        else
        {
            ViewBag.Mess = mess;
        }

        var email = HttpContext.Session.GetString("LoggedInUser");
        ViewBag.stulogin = _artService.GStuBE(email);
        return View();
    }
    [HttpPost]
    public IActionResult AddArt(Artwork art)
    {
        if (!ModelState.IsValid)
        {
            // Kiểm tra từng trường input và chỉ cảnh báo khi trường đó null
            if (string.IsNullOrEmpty(art.ArtworkName))
                ModelState.AddModelError("Name", "Please enter a name.");

            if (string.IsNullOrEmpty(art.FileUrl))
                ModelState.AddModelError("Images", "Please enter a Images.");


            if (string.IsNullOrEmpty(art.Descritption))
                ModelState.AddModelError("Descritption", "Please enter a Descritption.");

            // Thực hiện kiểm tra các trường khác nếu cần

            TempData["Mess"] = "Please do not leave blank boxes";
            return RedirectToAction("ShowAddArt");
        }
        else
        {
            art.DayPost = DateTime.Now;
            _artService.AddArt(art);
            return RedirectToAction("ShowArtWork");
        }
    }
    public IActionResult DeleteArt(int id)
    {
        try
        {
            _artService.DropArt(id);
            return RedirectToAction("ShowArtWork");
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "An error occurred while deleting data." });
        }
       
    }
}
