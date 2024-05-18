using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using ProjectSem3.IService;

namespace ProjectSem3.Models.Dao;

public class ArtworkDao : IArtworkService
{
    private readonly IofaContext _context;
    public ArtworkDao(IofaContext context)
    {
        _context = context;
    }
    public bool AddArt(Artwork art)
    {
        try
        {
            _context.Artworks.Add(art);
            return _context.SaveChanges() > 0;
        }catch(Exception)
        {
            return false;
        }
    }
    public List<Artwork> GetlistArt()
    {
        try
        {
            var Art = _context.Artworks
                .OrderByDescending(f => f.ArtworkId)
                .Include(f => f.Student)
                .ToList();

            return Art;
        }
        catch (Exception)
        {
            return new List<Artwork>();
        }
    }
    public List<Student> GetStu()
    {
        try
        {
            var res = _context.Students.ToList();
            if (res != null)
            {
                return res;
            }
            return [];
        }
        catch (Exception)
        {

            return [];
        }
    }
    public bool DropArt(int id)
    {
        try
        {
            var res = _context.Artworks.FirstOrDefault(p => p.ArtworkId.Equals(id));
            if (res != null)
            {
                _context.Artworks.Remove(res);
                return _context.SaveChanges() > 0;
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Student? GStuBE(string email)
    {
        try
        {
            var student = _context.Students.FirstOrDefault(m => m.Email == email);
            return student ?? new Student(); // Trả về một đối tượng Student mới nếu không tìm thấy sinh viên
        }
        catch (Exception)
        {
            return new Student(); // Trả về một đối tượng Student mới nếu có lỗi xảy ra
        }
    }

}
