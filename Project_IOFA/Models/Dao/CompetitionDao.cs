using Microsoft.EntityFrameworkCore;
using ProjectSem3.IService;

namespace ProjectSem3.Models.Dao;

public class CompetitionDao : ICompetitionService
{
    private readonly IofaContext _context;
    public CompetitionDao(IofaContext context)
    {
        _context = context;
    }
    public Competition GetCompe(int id)
    {
        try
        {
            var compe = _context.Competitions
                .Include(p => p.Teacher)
                .FirstOrDefault(p => p.CompetitionId == id);
            if (compe != null) return compe;
            return new Competition();
        }
        catch (Exception)
        {
            return new Competition();
        }
    }
    public List<Competition> GetlistCompe()
    {
        try
        {
            var compe = _context.Competitions
                .OrderByDescending(f => f.CompetitionId)
                .Include(f => f.Teacher)
                .ToList();

            return compe;
        }
        catch (Exception)
        {
            return [];
        }
    }
}
