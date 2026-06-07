using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Portfolio.Api.Models;

namespace Portfolio.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<ProjectSkill> ProjectSkills => Set<ProjectSkill>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.HasIndex(p => p.Slug)
                .IsUnique();

            entity.Property(p => p.Title)
                .IsRequired();

            entity.Property(p => p.Slug)
                .IsRequired();

            entity.Property(p => p.ShortDescription)
                .IsRequired();
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.HasIndex(s => s.Slug)
                .IsUnique();

            entity.Property(s => s.Name)
                .IsRequired();

            entity.Property(s => s.Slug)
                .IsRequired();
        });

        modelBuilder.Entity<ProjectSkill>(entity =>
        {
            entity.HasKey(ps => new { ps.ProjectId, ps.SkillId });

            entity.HasOne(ps => ps.Project)
                .WithMany(p => p.ProjectSkills)
                .HasForeignKey(ps => ps.ProjectId);

            entity.HasOne(ps => ps.Skill)
                .WithMany(s => s.ProjectSkills)
                .HasForeignKey(ps => ps.SkillId);
        });
    }
}