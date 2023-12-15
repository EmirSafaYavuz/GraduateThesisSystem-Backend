using DataAccess.Concrete.Configurations;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public class GtsContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=GraduateThesisSystem;Username=emirsafayavuz;Password=12345");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Institute> Institutes { get; set; }
    public DbSet<Keyword> Keywords { get; set; }
    public DbSet<KeywordThesis> KeywordsTheses { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<SubjectTopic> SubjectTopics { get; set; }
    public DbSet<SubjectTopicThesis> SubjectTopicsTheses { get; set; }
    public DbSet<Supervisor> Supervisors { get; set; }
    public DbSet<SupervisorThesis> SupervisorsTheses { get; set; }
    public DbSet<Thesis> Theses { get; set; }
    public DbSet<University> Universities { get; set; }
}