using Microsoft.EntityFrameworkCore;
using ProjectSem3.IService;

namespace ProjectSem3.Models.Dao;

public class SubmissionDao : ISubmissionService
{
    private readonly IofaContext _context;
    public SubmissionDao(IofaContext context)
    {
        _context = context;
    }
    public bool StuSendTeacher(Submission sub)
    {
        if (sub == null)
        {
            return false;
        }

        var artwork = _context.Artworks.SingleOrDefault(a => a.ArtworkId == sub.ArtworkId);
        if (artwork == null)
        {
            return false;
        }

        var newSubmission = new Submission
        {
            Award = null,
            CompetitionId = sub.CompetitionId,
            Status = "Waiting for grading",
            ArtworkId = sub.ArtworkId,
            Mark = null,
            SubmissionDate = sub.SubmissionDate,
            ExhibitionId = null,
        };

        _context.Submissions.Add(newSubmission);

        return _context.SaveChanges() > 0;
    }


    public List<Exhibition> GetEx()
    {
        try
        {
            var res = _context.Exhibitions.ToList();
            if(res != null)
            {
                return res;
            }
            return [];
        }catch (Exception)
        {
            return [];
        }
    }
    public List<Artwork> GetlistArt()
    {
        try
        {
            var Art = _context.Artworks
                .ToList();
            if (Art != null)
            {
                return Art;
            }
            return [];
        }
        catch (Exception)
        {
            return [];
        }
    }
    public List<Submission> GetlistSub()
    {
        try
        {
            var sub = _context.Submissions
                .OrderByDescending(f => f.SubmissionId)
                .Include(f => f.Artwork)
                .Include (f => f.Competition)
                .Include(f=> f.Exhibition)
                .ToList();

            return sub;
        }
        catch (Exception)
        {
            return [];
        }
    }
    public bool DropSub(int id)
    {
        try
        {
            var res = _context.Submissions.FirstOrDefault(p => p.SubmissionId.Equals(id));
            if (res != null)
            {
                _context.Submissions.Remove(res);
                return _context.SaveChanges() > 0;
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public Submission GetSub(int id)
    {
        try
        {
            var sub = _context.Submissions
                .Include(p => p.Artwork)
                .Include(p => p.Competition)
                .Include(p => p.Exhibition)
                .FirstOrDefault(p => p.SubmissionId == id);
            if (sub != null) return sub;
            return new Submission();
        }
        catch (Exception)
        {
            return new Submission();
        }
    }
}
