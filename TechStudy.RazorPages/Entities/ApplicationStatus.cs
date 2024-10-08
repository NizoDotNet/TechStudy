﻿namespace TechStudy.RazorPages.Entities;

public enum ApplicationStatusId
{
    Review = 1,
    Accepted,
    Rejected
}

public class ApplicationStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}