namespace Portfolio.Api.Models;

public class Project
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string? LongDescription { get; set; }

    public string? GithubUrl { get; set; }
    public string? DemoUrl { get; set; }

    public bool Featured { get; set; }

    public ICollection<ProjectSkill> ProjectSkills { get; set; } = new List<ProjectSkill>();
}