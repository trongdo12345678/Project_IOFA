using Microsoft.AspNetCore.Mvc;
using ProjectSem3.IService;

namespace ProjectSem3.Controllers;
public class StudentUserController : Controller
{
    private IArtworkService _artService;
    public StudentUserController(IArtworkService artService)
    {
        _artService = artService;
    }
    public IActionResult Index()
    {
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
    public IActionResult Feature()
    {
        return View();
    }
    public IActionResult Project()
    {
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
