using Microsoft.AspNetCore.Mvc;
using ProjectSem3.IService;
using ProjectSem3.Models;

namespace ProjectSem3.Controllers;
public class SubmissionController : Controller
{
    private ISubmissionService _subService;
    public SubmissionController(ISubmissionService subService)
    {
        _subService = subService;
    }
    public IActionResult ShowUploadExamk(int id)
    {
        ViewBag.compe = id;
        ViewBag.Art = _subService.GetlistArt();
        ViewBag.Ex = _subService.GetEx();
        return View();
    }
    [HttpPost]
    public IActionResult UploadExamk(Submission sub, string compeid)
    {
        TempData["projectiddonate"] = compeid;
        sub.CompetitionId = Convert.ToInt32(TempData["projectiddonate"]);
        sub.SubmissionDate = DateTime.Now;
        bool result = _subService.StuSendTeacher(sub);
        return RedirectToAction("ShowSub");
       
    }

    public IActionResult ShowSub()
    {
       ViewBag.Sub = _subService.GetlistSub();
        return View();
    }
    public IActionResult success()
    {
        return View();
    }
    public IActionResult DropSub(int id)
    {
        try
        {
            _subService.DropSub(id);
            return RedirectToAction("ShowSub");
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "An error occurred while deleting data." });
        }

    }
}
