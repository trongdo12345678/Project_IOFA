using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.DependencyResolver;
using ProjectSem3.IService;
using System.Security.Cryptography;
using System.Text;

namespace ProjectSem3.Models.Dao;

public class AccountStudentDao : IAccountStudentService
{
    private readonly IofaContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AccountStudentDao(IofaContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    public Student? getmemberbyusername(string? email)
    {
        try
        {
            var stulogin = _context.Students.FirstOrDefault(m => m.Email == email);
            if (stulogin != null) return stulogin;

            return new Student();
        }
        catch (Exception)
        {

            return new Student();

        }
    }
    //login cho student
    public bool LoginStudent(string email, string password)
    {
        var stu = _context.Students.FirstOrDefault(m => m.Email == email);
        if (stu != null)
        {
            if(BCrypt.Net.BCrypt.Verify(password, stu.Password))
            {
                _httpContextAccessor.HttpContext.Session.SetString("LoggedInUser", email);
            }

           
            return true;
        }
        return false;
    }
    public bool AddArt(Student model)
    {
        try
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password, salt);

            _context.Students.Add(model);
            return _context.SaveChanges() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
    //public void SendEmail(string fromEmail, string toEmail, string subject, string body)
    //{
    //    var emailMessage = new MimeMessage();
    //    emailMessage.From.Add(new MailboxAddress("", fromEmail));
    //    emailMessage.To.Add(new MailboxAddress("", toEmail));
    //    emailMessage.Subject = subject;
    //    emailMessage.Body = new TextPart("html") { Text = body };

    //    using (var client = new SmtpClient())
    //    {
    //        client.Connect(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]), true);
    //        client.Authenticate(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderPassword"]);
    //        client.Send(emailMessage);
    //        client.Disconnect(true);
    //    }
    //}

}
