using Microsoft.AspNetCore.Mvc;
using ProjectSem3.IService;

namespace ProjectSem3.Controllers;
public class CompetitionController : Controller
{
    private ICompetitionService _compeService;
    private IAccountStudentService _accountStudentService;
    public CompetitionController(ICompetitionService compeService, IAccountStudentService accountStudentService)
    {
        _compeService = compeService;
        _accountStudentService = accountStudentService;
    }
    public IActionResult ShowCompe()
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
        ViewBag.compe = _compeService.GetlistCompe();
        return View();
    }
    public IActionResult DetailCompe(int id)
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
        ViewBag.compe = _compeService.GetCompe(id);
        return View();
    }
}
