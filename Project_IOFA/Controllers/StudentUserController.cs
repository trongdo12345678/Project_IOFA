using Microsoft.AspNetCore.Mvc;
using ProjectSem3.IService;

namespace ProjectSem3.Controllers;
public class StudentUserController : Controller
{
    private IArtworkService _artService;
    private IExhibitionService _exhibitionService;
    private ISubmissionService _subService;
    private ICompetitionService _compeService;
    private IAccountStudentService _accountStudentService;
    public StudentUserController(IArtworkService artService, IExhibitionService exhibitionService, ISubmissionService subService, ICompetitionService compeService, IAccountStudentService accountStudentService)
    {
        _artService = artService;
        _exhibitionService = exhibitionService;
        _subService = subService;
        _compeService = compeService;
        _accountStudentService = accountStudentService;
    }
    public IActionResult Index()
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
        ViewBag.Sub = _subService.GetlistSub();
        ViewBag.Ex = _exhibitionService.GetExhi();
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }
    public IActionResult Exhibition()
    {
        ViewBag.Sub = _subService.GetlistSub();
        ViewBag.Ex = _exhibitionService.GetExhi();
        return View();
    }
    //[Route("~/")]
    public IActionResult Product()
    {
        ViewBag.Sub = _subService.GetlistSub();
        return View();
    }
    public IActionResult DetailPro(int id)
    {
        ViewBag.ProD = _subService.GetSub(id);
        return View();
    }
    public IActionResult Teacher()
    {
        return View();
    }
    public IActionResult Evaluate()
    {
        return View();
    }


}
