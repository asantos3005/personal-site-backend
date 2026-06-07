using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.DTOs;

namespace Portfolio.Api.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _db;

    public ProjectService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<ProjectDto>> GetProjectsAsync(string? skill, string? search)
    {
        var query = _db.Projects.AsQueryable();

        if (!string.IsNullOrWhiteSpace(skill))
        {
            query = query.Where(p =>
                p.ProjectSkills.Any(ps => ps.Skill.Slug == skill));
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var pattern = $"%{search}%";

            query = query.Where(p =>
                EF.Functions.ILike(p.Title, pattern) ||
                EF.Functions.ILike(p.ShortDescription, pattern) ||
                (p.LongDescription != null && EF.Functions.ILike(p.LongDescription, pattern))
            );
        }

        return await query
            .Select(p => new ProjectDto(
                p.Id,
                p.Title,
                p.Slug,
                p.ShortDescription,
                p.LongDescription,
                p.GithubUrl,
                p.DemoUrl,
                p.ProjectSkills
                    .Select(ps => new SkillDto(
                        ps.Skill.Name,
                        ps.Skill.Slug
                    ))
                    .ToList(),
                p.Featured
            ))
            .ToListAsync();
    }

    public async Task<ProjectDto?> GetProjectBySlugAsync(string slug)
    {
        return await _db.Projects
            .Where(p => p.Slug == slug)
            .Select(p => new ProjectDto(
                p.Id,
                p.Title,
                p.Slug,
                p.ShortDescription,
                p.LongDescription,
                p.GithubUrl,
                p.DemoUrl,
                p.ProjectSkills
                    .Select(ps => new SkillDto(
                        ps.Skill.Name,
                        ps.Skill.Slug
                    ))
                    .ToList(),
                p.Featured
            ))
            .FirstOrDefaultAsync();
    }
}