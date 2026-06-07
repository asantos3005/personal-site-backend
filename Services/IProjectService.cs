using Portfolio.Api.DTOs;

namespace Portfolio.Api.Services;

public interface IProjectService
{
    Task<List<ProjectDto>> GetProjectsAsync(string? skill, string? search);
    Task<ProjectDto?> GetProjectBySlugAsync(string slug);
}