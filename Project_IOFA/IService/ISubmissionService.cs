﻿using Microsoft.EntityFrameworkCore;
using ProjectSem3.Models;

namespace ProjectSem3.IService;

public interface ISubmissionService
{
    public bool StuSendTeacher(Submission sub);
    public List<Exhibition> GetEx();
    public List<Artwork> GetlistArt();
    public List<Submission> GetlistSub();
    public bool DropSub(int id);
    public Submission GetSub(int id);


}
