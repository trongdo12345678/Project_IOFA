using ProjectSem3.Models;

namespace ProjectSem3.IService;

public interface IAccountStudentService
{
    public bool LoginStudent(string email, string password);
    public bool AddArt(Student model);
    public Student? getmemberbyusername(string? email);

}
