using ProjectSem3.Models;

namespace ProjectSem3.IService;

public interface IArtworkService
{
    public bool AddArt(Artwork art);
    public List<Artwork> GetlistArt();
    public List<Student> GetStu();
    public bool DropArt(int id);
    public Student? GStuBE(string email);
}
