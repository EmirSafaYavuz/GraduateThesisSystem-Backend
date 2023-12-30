using System;
using System.Collections.Generic;
using Core.Entities.Concrete;
using DataAccess.Entities;
using DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataAccess.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ThesisType>();
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    { }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Institute> Institutes { get; set; }

    public virtual DbSet<Keyword> Keywords { get; set; }

    public virtual DbSet<KeywordsThesis> KeywordsTheses { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<SubjectTopic> SubjectTopics { get; set; }

    public virtual DbSet<SubjectTopicsThesis> SubjectTopicsTheses { get; set; }

    public virtual DbSet<Supervisor> Supervisors { get; set; }

    public virtual DbSet<SupervisorsThesis> SupervisorsTheses { get; set; }

    public virtual DbSet<Thesis> Theses { get; set; }

    public virtual DbSet<University> Universities { get; set; }
    public virtual DbSet<OperationClaim> OperationClaims { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=GraduateThesisSystem;Username=emirsafayavuz;Password=12345", npgsqlOptions =>
        {
            
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("thesis_type", new[] { "Master", "Doctorate", "Specialization in Medicine", "Proficiency in Art" });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authors_pk");

            entity.ToTable("authors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Institute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("institutes_pk");

            entity.ToTable("institutes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.UniversityId).HasColumnName("university_id");

            entity.HasOne(d => d.University).WithMany(p => p.Institutes)
                .HasForeignKey(d => d.UniversityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("institutes_universities_id_fk");
        });

        modelBuilder.Entity<Keyword>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("keywords_pk");

            entity.ToTable("keywords");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<KeywordsThesis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("keywords_theses_pk");

            entity.ToTable("keywords_theses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.KeywordId).HasColumnName("keyword_id");
            entity.Property(e => e.ThesisId).HasColumnName("thesis_id");

            entity.HasOne(d => d.Keyword).WithMany(p => p.KeywordsTheses)
                .HasForeignKey(d => d.KeywordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("keywords_theses_keywords_id_fk");

            entity.HasOne(d => d.Thesis).WithMany(p => p.KeywordsTheses)
                .HasForeignKey(d => d.ThesisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("keywords_theses_theses_id_fk");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("languages_pk");

            entity.ToTable("languages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("locations_pk");

            entity.ToTable("locations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(25)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(25)
                .HasColumnName("country");
        });

        modelBuilder.Entity<SubjectTopic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subject_topics_pk");

            entity.ToTable("subject_topics");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<SubjectTopicsThesis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subject_topics_theses_pk");

            entity.ToTable("subject_topics_theses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SubjectTopicId).HasColumnName("subject_topic_id");
            entity.Property(e => e.ThesisId).HasColumnName("thesis_id");

            entity.HasOne(d => d.SubjectTopic).WithMany(p => p.SubjectTopicsTheses)
                .HasForeignKey(d => d.SubjectTopicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subject_topics_theses_subject_topics_id_fk");

            entity.HasOne(d => d.Thesis).WithMany(p => p.SubjectTopicsTheses)
                .HasForeignKey(d => d.ThesisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subject_topics_theses_theses_id_fk");
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("supervisors_pk");

            entity.ToTable("supervisors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<SupervisorsThesis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("supervisors_theses_pk");

            entity.ToTable("supervisors_theses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SupervisorId).HasColumnName("supervisor_id");
            entity.Property(e => e.ThesisId).HasColumnName("thesis_id");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.SupervisorsTheses)
                .HasForeignKey(d => d.SupervisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supervisors_theses_supervisors_id_fk");

            entity.HasOne(d => d.Thesis).WithMany(p => p.SupervisorsTheses)
                .HasForeignKey(d => d.ThesisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supervisors_theses_theses_id_fk");
        });

        modelBuilder.Entity<Thesis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("id");

            entity.ToTable("theses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abstract)
                .HasMaxLength(5000)
                .HasColumnName("abstract");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.CoSupervisorId).HasColumnName("co_supervisor_id");
            entity.Property(e => e.InstituteId).HasColumnName("institute_id");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.NumOfPages).HasColumnName("num_of_pages");
            entity.Property(e => e.SubmissionDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("submission_date");
            entity.Property(e => e.SupervisorId).HasColumnName("supervisor_id");
            entity.Property(e => e.ThesisNo).HasColumnName("thesis_no");
            entity.Property(e => e.Title)
                .HasMaxLength(500)
                .HasColumnName("title");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Author).WithMany(p => p.Theses)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("theses_authors_id_fk");

            entity.HasOne(d => d.CoSupervisor).WithMany(p => p.ThesisCoSupervisors)
                .HasForeignKey(d => d.CoSupervisorId)
                .HasConstraintName("theses_co_supervisor_id_fk");

            entity.HasOne(d => d.Institute).WithMany(p => p.Theses)
                .HasForeignKey(d => d.InstituteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("theses_institutes_id_fk");

            entity.HasOne(d => d.Language).WithMany(p => p.Theses)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("theses_languages_id_fk");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.ThesisSupervisors)
                .HasForeignKey(d => d.SupervisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("theses_supervisors_id_fk");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("universities_pk");

            entity.ToTable("universities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Location).WithMany(p => p.Universities)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("universities_locations_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
