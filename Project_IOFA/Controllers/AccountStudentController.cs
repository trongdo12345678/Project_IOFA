using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProjectSem3.IService;
using ProjectSem3.Models;
using System.Net.Mail;
using Google.Authenticator;

namespace ProjectSem3.Controllers;
public class AccountStudentController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private IAccountStudentService _accountstuService;
    public AccountStudentController(IAccountStudentService accountstuService, IHttpContextAccessor httpContextAccessor)
    {
        _accountstuService = accountstuService;
        _httpContextAccessor = httpContextAccessor;
    }
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("LoggedInUser") != null)
        {


            return RedirectToAction("Index", "StudentUser");
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
            return RedirectToAction("Index", "StudentUser");
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
    public IActionResult showadd()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Addstudent(Student model)
    {
        _accountstuService.AddArt(model);
        return RedirectToAction("showadd");
    }
    // Phương thức để gửi email
    //public IActionResult ShowRequestOTP()
    //{
    //    return View();
    //}
    //    [HttpPost]
    //    public IActionResult RequestOTP(string fromEmail, string userEmail)
    //    {
    //        string otp = GenerateOTP();

    //        // Lưu trữ OTP và email vào session
    //        HttpContext.Session.SetString("OTP", otp);
    //        HttpContext.Session.SetString("OTPEmail", userEmail);

    //        string subject = "Your OTP Code";
    //        string body = $"Your OTP code is {otp}";

    //        try
    //        {
    //        _accountstuService.SendEmail(fromEmail, userEmail, subject, body);
    //            ViewBag.Message = "OTP has been sent successfully.";
    //        }
    //        catch (Exception ex)
    //        {
    //            ViewBag.Error = "Error sending OTP: " + ex.Message;
    //        }

    //        return View();
    //    }
    //    public IActionResult ShowConfirmOTP()
    //    {
    //        return View();
    //    }
    //    [HttpPost]
    //    public IActionResult ConfirmOTP(string email, string otp)
    //    {
    //        string sessionOtp = HttpContext.Session.GetString("OTP");
    //        string sessionEmail = HttpContext.Session.GetString("OTPEmail");

    //        if (sessionOtp == otp && sessionEmail == email)
    //        {
    //            // OTP đúng, thực hiện các bước tiếp theo như cho phép đổi mật khẩu
    //            ViewBag.Message = "OTP verified successfully.";
    //        }
    //        else
    //        {
    //            // OTP sai
    //            ViewBag.Error = "Invalid OTP.";
    //        }

    //        return View();
    //    }
    //    private string GenerateOTP()
    //    {
    //        var random = new Random();
    //        return random.Next(100000, 999999).ToString();
    //    }

        [HttpPost] // hàm Logout
        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("LoggedInUser", "");
            return RedirectToAction("index", "Layoutmain");
        }
}
