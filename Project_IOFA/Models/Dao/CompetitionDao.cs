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
