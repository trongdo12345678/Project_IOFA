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
    public bool StuSendTeacher(Submission sub, out string errorMessage)
    {
        errorMessage = string.Empty;

        try
        {
            if (sub == null)
            {
                errorMessage = "Submission is null.";
                return false;
            }

            var artwork = _context.Artworks.SingleOrDefault(a => a.ArtworkId == sub.ArtworkId);
            if (artwork == null)
            {
                errorMessage = "Artwork not found.";
                return false;
            }

            if (artwork.Status == "Submitted")
            {
                errorMessage = "This exam has been submitted.";
                return false;
            }

            // Cập nhật trạng thái của artwork thành "Submitted"
            artwork.Status = "Submitted";

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

            bool submissionAdded = _context.SaveChanges() > 0;

            return submissionAdded;
        }
        catch (Exception ex)
        {
            errorMessage = $"Error in StuSendTeacher: {ex.Message}";
            return false;
        }
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
}
