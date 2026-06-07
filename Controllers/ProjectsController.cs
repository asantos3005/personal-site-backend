using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.DTOs;
using Portfolio.Api.Services;

namespace Portfolio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectDto>>> GetProjects(
        [FromQuery] string? skill,
        [FromQuery] string? search)
    {
        var projects = await _projectService.GetProjectsAsync(skill, search);

        return Ok(projects);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<ProjectDto>> GetProjectBySlug(string slug)
    {
        var project = await _projectService.GetProjectBySlugAsync(slug);

        if (project is null)
        {
            return NotFound();
        }

        return Ok(project);
    }
}