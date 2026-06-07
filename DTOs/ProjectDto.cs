namespace Portfolio.Api.DTOs;

public record ProjectDto(
    int Id,
    string Title,
    string Slug,
    string ShortDescription,
    string? LongDescription,
    string? GithubUrl,
    string? DemoUrl,
    List<SkillDto> Skills,
    bool Featured
);