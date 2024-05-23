using ProjectSem3.IService;

namespace ProjectSem3.Models.Dao;

public class ExhibitionDao : IExhibitionService
{
    private readonly IofaContext _context;
    public ExhibitionDao(IofaContext context)
    {
        _context = context;
    }
    public List<Exhibition> GetExhi()
    {
        try
        {
            var ex = _context.Exhibitions
                .OrderByDescending(m => m.ExhibitionId)
                .ToList();
            return ex;
        }
        catch(Exception)
        {
            return [];
        }
    }
}
