﻿using TechStudy.RazorPages.Data;

namespace TechStudy.RazorPages.Entities;

public class ApplicationForMembership
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; }
    public string TechStudyUserId { get; set; } = null!;
    public TechStudyUser? TechStudyUser { get; set; }
    public int? ApplicationStatusId { get; set; } = 1;
    public ApplicationStatus? ApplicationStatus { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
}
