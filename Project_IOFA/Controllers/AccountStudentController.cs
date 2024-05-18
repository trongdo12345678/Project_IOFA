using Microsoft.AspNetCore.Mvc;
using ProjectSem3.IService;
using ProjectSem3.Models;

namespace ProjectSem3.Controllers;
public class AccountStudentController : Controller
{

    private IAccountStudentService _accountstuService;
    public AccountStudentController(IAccountStudentService accountstuService)
    {
        _accountstuService = accountstuService;
    }
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("LoggedInUser") != null)
        {


            return RedirectToAction("ShowArtWork", "ArtWork");
        }
        else
        {
            return RedirectToAction("LoginUser");
        }
    }
    [HttpPost]
    public IActionResult LoginStu(string email, string password)
    {
        bool isLoggedIn = _accountstuService.LoginStudent(email, password);
        if (isLoggedIn)
        {
            HttpContext.Session.SetString("LoggedInUser", email);
            return RedirectToAction("ShowArtWork", "ArtWork");
        }
        else
        {
            TempData["ErrorMessage"] = "Account or password is incorrect.";
            return RedirectToAction("LoginStuWeb");
        }
    }
    //[Route("~/")]
    public IActionResult LoginStuWeb()
    {
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string ?? "";
        ViewBag.UsernameErrorMessage = TempData["UsernameErrorMessage"] as string ?? "";

        bool redirectToRegister = TempData["RedirectToRegister"] != null ? (bool)TempData["RedirectToRegister"] : false;
        if (redirectToRegister)
        {
            return View("LoginStuWeb");
        }

        return View();
    }
    [HttpPost] // hàm Logout
    public IActionResult LogOut()
    {
        HttpContext.Session.SetString("LoggedInUser", "");
        return RedirectToAction("index", "Layoutmain");
    }

}
